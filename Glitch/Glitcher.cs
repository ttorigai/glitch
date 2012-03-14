using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Glitch.Lib
{
    public class Glitcher
    {
        private const int FILE_MOD_COUNT = 4; // number of corruption loops to run
        private const long MIN_SIZE = 1024; // 1 kilobytes
        //private const long MAX_SIZE = 3145728; // 3 megabytes
        //private const long MAX_SIZE = 2048; // 2 kilobytes
        private const long MAX_SIZE = 4096; // 4 kilobytes

        String inputFilename;
        bool valid;
        byte[] file;
        long imageDataOffset;
        long origFileSize;

        public String Message { get; set; }
        public String ResultFile { get; set; }

        enum CorruptionType { RANDOM, SWAP, REMOVE, COPY }

        public Glitcher(String filename)
        {
            valid = true;
            inputFilename = filename;
            imageDataOffset = 0;
            Message = "initialized...";
            Validate();
        }

        public bool Process()
        {
            if (!valid) 
                return false;
            Console.WriteLine("process...");
            try
            {
                TestHeader();
                Corruption();
                Output();
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
            return true;
        }

        private void Corruption()
        {
            Console.WriteLine("Image data offset is {0} of file length {1}", imageDataOffset, file.Length);

            Random r = new Random((int)DateTime.UtcNow.Ticks);
            for (int i = 0; i < FILE_MOD_COUNT; i++)
            {
                CorruptionType t = (CorruptionType)r.Next(0, 4);
                Console.WriteLine("... applying type {0}", t);
                Apply(t);
            }
        }

        private void Apply(CorruptionType t)
        {
            switch (t)
            {
                case CorruptionType.RANDOM:
                    RandomCorrupt();
                    break;
                case CorruptionType.COPY:
                    CopyCorrupt();
                    break;
                case CorruptionType.REMOVE:
                    RemoveCorrupt();
                    break;
                case CorruptionType.SWAP:
                    SwapCorrupt();
                    break;
            }
        }

        #region PSD File Verification

        private void Validate()
        {
            try
            {
                FileInfo fi = new FileInfo(inputFilename);
                // currently only operates on psd filetype, random bit manipulation doesn't seem to have desired effect on other filetypes
                if (!fi.Extension.Equals(".psd"))
                {
                    Message = "Invalid file type: " + fi.Extension;
                    valid = false;
                }
                file = File.ReadAllBytes(inputFilename);
                origFileSize = file.Length;
                valid = TestHeader();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private bool TestHeader()
        {
            // FILE HEADER
            // ----
            // bytes 0-3 - signature
            // bytes 4-5 - version
            // bytes 6-11 - zeroes
            // bytes 12-13 - channels
            // bytes 14-17 - height
            // bytes 18-21 - width
            // bytes 22-23 - depth
            // bytes 24-25 - color mode
            if (file[0] != '8' || file[1] != 'B' || file[2] != 'P' || file[3] != 'S')
            {
                Console.WriteLine("FAILED PSD file format verification!");
                return false;
            }
            
            Console.WriteLine("PSD file format verified...");            
            int ver = BitConverter.ToInt16(new byte[] { file[5], file[4] }, 0);  
            // note that the bytes must be swapped
            // see http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/PhotoshopFileFormats.htm under the Windows section
            if (ver != 1)
            {
                Console.WriteLine("FAILED PSD file version verification!");
                return false;
            }
            Console.WriteLine("PSD file version is verified {0}", ver);

            int height = BitConverter.ToInt32(new byte[] { file[17], file[16], file[15], file[14] }, 0);
            int width = BitConverter.ToInt32(new byte[] { file[21], file[20], file[19], file[18] }, 0);
            Console.WriteLine("Image size is WxH of {0}x{1}", width, height);

            // COLOR MODE DATA
            // ---
            int dataLength = BitConverter.ToInt32(new byte[] { file[29], file[28], file[27], file[26] }, 0);
            Console.WriteLine("Color mode data length is {0}", dataLength);
            // 26 bytes of header info plus 4 for color mode data length gives start offset of 30
            imageDataOffset = 30 + dataLength;

            // IMAGE RESOURCES
            // ---
            dataLength = BitConverter.ToInt32(new byte[] { file[imageDataOffset + 3], file[imageDataOffset + 2], file[imageDataOffset + 1], file[imageDataOffset] }, 0);
            Console.WriteLine("Image resources data length is {0}", dataLength);
            // start offset plus 4 for image resource data length bytes
            imageDataOffset = (imageDataOffset + 4) + dataLength;

            // LAYER & MASK INFO
            // ---
            dataLength = BitConverter.ToInt32(new byte[] { file[imageDataOffset + 3], file[imageDataOffset + 2], file[imageDataOffset + 1], file[imageDataOffset] }, 0);
            Console.WriteLine("Layer and mask info length is {0}", dataLength);
            imageDataOffset = (imageDataOffset + 4) + dataLength;

            return true;
        }

        #endregion PSD File Verification        

        #region Utility functions
        
        private long LongRandom(long min, long max, Random r)
        {
            if (max <= min)
            {
                Console.WriteLine("MAX MUST BE GREATER THAN MIN");
                return 0;
            }

            byte[] buf = new byte[8];
            r.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        private Tuple<long, long> generateRange(Random r, long start, long end, long minSize, long maxSize)
        {
            long a = LongRandom(start, end - maxSize, r);  // start is a random value between start (imageDataOffset) and end (size of array - maxSize)
            long b = LongRandom(a + minSize, (maxSize < end - a) ? (a + maxSize) : end, r);  // end is a random value between a + minSize and either a + maxSize or the end
            return new Tuple<long, long>(a, b);
        }

        private void Output()
        {
            string outName = MakeOutputFilename();

            using (FileStream fs = new FileStream(outName, FileMode.CreateNew))
            {
                fs.Write(file, 0, file.Length);
                ResultFile = fs.Name;
            }
        }

        private String MakeOutputFilename()
        {
            int idx = 0;
            string retval = "g" + idx.ToString("00") + "_" + Path.GetFileName(inputFilename);
            while (File.Exists(retval))
                retval = "g" + (++idx).ToString("00") + "_" + Path.GetFileName(inputFilename);
            return retval;
        }

        private byte[] InsertRange(byte[] target, byte[] data, long startIndex)
        {
            byte[] tmp = new byte[target.Length + data.Length];

            int resultIdx = 0;
            for (int i = 0; i < target.Length; i++)
            {
                tmp[resultIdx++] = target[i];
                if (i == startIndex)
                {
                    int dataIdx = 0;
                    while (dataIdx < data.Length)
                        tmp[resultIdx++] = data[dataIdx++];
                }                
            }
            return tmp;
        }

        private byte[] RemoveRange(byte[] target, long startIndex, long size)
        {
            byte[] tmp = new byte[target.Length - size];

            int resultIdx = 0;
            for (int i = 0; i < target.Length; i++)
            {
                if (i < startIndex || i > (startIndex + size))
                    tmp[resultIdx++] = target[i];
            }
            return tmp;
        }

        private byte[] WriteRange(byte[] target, byte[] data, long startIndex)
        {
            if (data.Length + startIndex > target.Length)
            {
                Console.WriteLine("WriteRange FAILED: Invalid data length for given start index");
                return data;
            }
            long dataIdx = 0;
            for (long i = startIndex; i < (startIndex + data.Length); i++)
                target[i] = data[dataIdx++];
            return target;
        }

        #endregion Utility functions

        #region Corruption methods

        private void RandomCorrupt()
        {
            Random r = new Random((int)DateTime.UtcNow.Ticks);

            Tuple<long, long> range = generateRange(r, imageDataOffset, file.Length, MIN_SIZE, MAX_SIZE);

            byte[] randomBytes = new byte[range.Item2 - range.Item1];
            r.NextBytes(randomBytes);
            randomBytes.CopyTo(file, range.Item1);
        }

        private void CopyCorrupt()
        {
            Random r = new Random((int)DateTime.UtcNow.Ticks);

            Tuple<long, long> range = generateRange(r, imageDataOffset, file.Length, MIN_SIZE, MAX_SIZE);

            byte[] copyData = new byte[range.Item2 - range.Item1];
            for (long i = range.Item1, j = 0; i < range.Item2; i++, j++)
                copyData[j] = file[i];

            int copyCount = r.Next(2, 8);

            for (int i = 0; i < copyCount; i++)
            {
                long copyTo = LongRandom(imageDataOffset, file.Length - MAX_SIZE, r);
                file = InsertRange(file, copyData, copyTo);
            }
        }

        private void RemoveCorrupt()
        {
            // limit the removal of data to half of the filesize
            if (file.Length < (origFileSize / 2))
            {
                // add some data
                CopyCorrupt();
                return;
            }

            Random r = new Random((int)DateTime.UtcNow.Ticks);

            Tuple<long, long> range = generateRange(r, imageDataOffset, file.Length, MIN_SIZE, MAX_SIZE);

            file = RemoveRange(file, range.Item1, range.Item2 - range.Item1);
        }

        private void SwapCorrupt()
        {
            Random r = new Random((int)DateTime.UtcNow.Ticks);
            Tuple<long, long> range1 = generateRange(r, imageDataOffset, file.Length, MIN_SIZE, MAX_SIZE);
            // second range starts between same start and end of file minus size of first range
            long range2start = LongRandom(imageDataOffset, file.Length - (range1.Item2 - range1.Item1), r);
            long range2end = range2start + (range1.Item2 - range1.Item1);
            // validate whether the end of the second range is valid
            if (range2end > file.Length)
            {
                Console.WriteLine("FAIL, generated invalid swap range");
                return;
            }
            Tuple<long, long> range2 = new Tuple<long, long>(range2start, range2end);

            byte[] copyDataA = new byte[range1.Item2 - range1.Item1];
            for (long i = range1.Item1, j = 0; i < range1.Item2; i++, j++)
            {
                copyDataA[j] = file[i];
            }
            
            byte[] copyDataB = new byte[range2.Item2 - range2.Item1];
            
            for (long i = range2.Item1, j = 0; i < range2.Item2; i++, j++)
            {
                copyDataB[j] = file[i];
            }

            file = WriteRange(file, copyDataA, range2.Item1);
            file = WriteRange(file, copyDataB, range1.Item1);
        }

        #endregion Corruption methods        
    }    
}
