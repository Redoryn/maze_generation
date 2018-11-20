namespace Unity_ScratchPad
{
    partial class Form1
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnStepForward = new System.Windows.Forms.Button();
            this.btnRunToCompletion = new System.Windows.Forms.Button();
            this.picBoxGrid = new System.Windows.Forms.PictureBox();
            this.listBoxAlgorithms = new System.Windows.Forms.ListBox();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(685, 358);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStepForward
            // 
            this.btnStepForward.Location = new System.Drawing.Point(685, 387);
            this.btnStepForward.Name = "btnStepForward";
            this.btnStepForward.Size = new System.Drawing.Size(87, 23);
            this.btnStepForward.TabIndex = 1;
            this.btnStepForward.Text = "Step Forward";
            this.btnStepForward.UseVisualStyleBackColor = true;
            this.btnStepForward.Click += new System.EventHandler(this.btnStepForward_Click);
            // 
            // btnRunToCompletion
            // 
            this.btnRunToCompletion.Location = new System.Drawing.Point(685, 416);
            this.btnRunToCompletion.Name = "btnRunToCompletion";
            this.btnRunToCompletion.Size = new System.Drawing.Size(75, 23);
            this.btnRunToCompletion.TabIndex = 2;
            this.btnRunToCompletion.Text = "Complete";
            this.btnRunToCompletion.UseVisualStyleBackColor = true;
            this.btnRunToCompletion.Click += new System.EventHandler(this.btnRunToCompletion_Click);
            // 
            // picBoxGrid
            // 
            this.picBoxGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxGrid.Location = new System.Drawing.Point(12, 12);
            this.picBoxGrid.Name = "picBoxGrid";
            this.picBoxGrid.Size = new System.Drawing.Size(667, 537);
            this.picBoxGrid.TabIndex = 3;
            this.picBoxGrid.TabStop = false;
            // 
            // listBoxAlgorithms
            // 
            this.listBoxAlgorithms.FormattingEnabled = true;
            this.listBoxAlgorithms.Location = new System.Drawing.Point(690, 208);
            this.listBoxAlgorithms.Name = "listBoxAlgorithms";
            this.listBoxAlgorithms.Size = new System.Drawing.Size(82, 134);
            this.listBoxAlgorithms.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(697, 526);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 559);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.listBoxAlgorithms);
            this.Controls.Add(this.picBoxGrid);
            this.Controls.Add(this.btnRunToCompletion);
            this.Controls.Add(this.btnStepForward);
            this.Controls.Add(this.btnGenerate);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnStepForward;
        private System.Windows.Forms.Button btnRunToCompletion;
        private System.Windows.Forms.PictureBox picBoxGrid;
        private System.Windows.Forms.ListBox listBoxAlgorithms;
        private System.Windows.Forms.Button btnClear;
    }
}

