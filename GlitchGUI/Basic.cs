using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Glitch.Lib;

namespace GlitchGUI
{
    public partial class Basic : Form
    {
        public Basic()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "*.psd|*.psd";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    lblSelectedFile.Text = dlg.FileName;
                    lblSelectedFile.AutoEllipsis = true;
                    ToolTip t = new ToolTip();
                    t.SetToolTip(lblSelectedFile, lblSelectedFile.Text);
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;
                // set the argument values
                int numPasses = (int)numberOfPasses.Value;
                int minBytes = int.Parse(txtMinBytes.Text);
                int maxBytes = int.Parse(txtMaxBytes.Text);

                lblProcessedResult.Text = "Processing...";
                btnProcess.Enabled = false;

                Glitcher g = new Glitcher(lblSelectedFile.Text);
                if (!g.Process()) throw new Exception(g.Message);
                else lblProcessedResult.Text = "Success: " + g.ResultFile;
            }
            catch (Exception ex)
            {
                lblProcessedResult.Text = "Error processing file: " + ex.Message;
                lblProcessedResult.AutoEllipsis = true;
                ToolTip t = new ToolTip();
                t.SetToolTip(lblProcessedResult, lblProcessedResult.Text);
            }
            finally
            {
                btnProcess.Enabled = true;
            }
        }

        private bool ValidateInputs()
        {
            StringBuilder sb = new StringBuilder();
            // test number of passes
            if (numberOfPasses.Value < 1)
                sb.Append("Number of passes must be greater than zero!\n");
            // value in kb, ensure greater than zero
            int minTmp = 0;
            if (!int.TryParse(txtMinBytes.Text, out minTmp))
                sb.Append("Minimum byte count must be an integer value!\n");
            if (minTmp < 0)
                sb.Append("Minimum byte count must be equal to or greater than zero!\n");
            int maxTmp = 0;
            if (!int.TryParse(txtMaxBytes.Text, out maxTmp))
                sb.Append("Maximum byte count must be an integer value!\n");
            if (maxTmp < 1)
                sb.Append("Maximum byte count must be equal to or greater than one!\n");
            // NOTE: enforced minimum range is 0 to 1 kb
            if (minTmp >= maxTmp)
                sb.Append("Maximum byte count must be greater than the minimum byte count!\n");
            if (sb.Length > 0)
                MessageBox.Show(sb.ToString() + "\nPlease correct the above errors.", "ERROR", MessageBoxButtons.OK);
            return sb.Length == 0;
        }
    }
}
