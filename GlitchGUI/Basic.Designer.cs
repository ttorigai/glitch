namespace GlitchGUI
{
    partial class Basic
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblProcessedResult = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.numberOfPasses = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtMaxBytes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMinBytes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPasses)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(32, 45);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(62, 100);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "Select File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSelectedFile);
            this.groupBox1.Location = new System.Drawing.Point(100, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected file:";
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.Location = new System.Drawing.Point(18, 28);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(233, 51);
            this.lblSelectedFile.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblProcessedResult);
            this.groupBox2.Location = new System.Drawing.Point(100, 377);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Process results:";
            // 
            // lblProcessedResult
            // 
            this.lblProcessedResult.Location = new System.Drawing.Point(18, 32);
            this.lblProcessedResult.Name = "lblProcessedResult";
            this.lblProcessedResult.Size = new System.Drawing.Size(233, 51);
            this.lblProcessedResult.TabIndex = 3;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(32, 377);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(62, 100);
            this.btnProcess.TabIndex = 5;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // numberOfPasses
            // 
            this.numberOfPasses.Location = new System.Drawing.Point(122, 24);
            this.numberOfPasses.Name = "numberOfPasses";
            this.numberOfPasses.Size = new System.Drawing.Size(38, 20);
            this.numberOfPasses.TabIndex = 6;
            this.numberOfPasses.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numberOfPasses);
            this.groupBox3.Location = new System.Drawing.Point(32, 175);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(330, 171);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Processing Parameters";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtMaxBytes);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtMinBytes);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(24, 59);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(269, 80);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Affected Byte Count (value in KB)";
            // 
            // txtMaxBytes
            // 
            this.txtMaxBytes.Location = new System.Drawing.Point(75, 49);
            this.txtMaxBytes.Name = "txtMaxBytes";
            this.txtMaxBytes.Size = new System.Drawing.Size(100, 20);
            this.txtMaxBytes.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Maximum:";
            // 
            // txtMinBytes
            // 
            this.txtMinBytes.Location = new System.Drawing.Point(75, 22);
            this.txtMinBytes.Name = "txtMinBytes";
            this.txtMinBytes.Size = new System.Drawing.Size(100, 20);
            this.txtMinBytes.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Minimum:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Number of passes:";
            // 
            // Basic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 517);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSelectFile);
            this.Name = "Basic";
            this.Padding = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.Text = "Glitch";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numberOfPasses)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSelectedFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblProcessedResult;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.NumericUpDown numberOfPasses;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtMaxBytes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMinBytes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}