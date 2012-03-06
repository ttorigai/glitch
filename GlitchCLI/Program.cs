using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Glitch.Lib;

namespace Glitch.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // first: take in a file argument
            if (args.Length < 1)
            {
                ShowUsage();
            }
            else
            {
                String file = args[0];
                if (File.Exists(file))
                {
                    Glitcher g = new Glitcher(file);
                    if (!g.Process())
                        Console.WriteLine("Failed processing - " + g.Message);
                    else
                        Console.WriteLine("Success - " + g.Message);
                }
                else
                    Console.WriteLine("Error: Invalid argument - File {0} does not exist.");
            }

            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
        }

        static void ShowUsage()
        {
            Console.WriteLine("GlitchCLI Usage: GlitchCLI <filename>");
            Console.WriteLine("GlitchCLI only works against the psd filetype");
        }
    }
}
