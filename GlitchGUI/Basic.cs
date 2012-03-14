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
        }

    }
}
