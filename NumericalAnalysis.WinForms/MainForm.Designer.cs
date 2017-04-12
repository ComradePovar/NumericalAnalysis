namespace NumericalAnalysis.WinForms
{
    partial class MainForm
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
            this.plot1 = new OxyPlot.WindowsForms.PlotView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbN = new System.Windows.Forms.TextBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.plot2 = new OxyPlot.WindowsForms.PlotView();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.plot1.Location = new System.Drawing.Point(12, 12);
            this.plot1.Name = "plot1";
            this.plot1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot1.Size = new System.Drawing.Size(620, 383);
            this.plot1.TabIndex = 0;
            this.plot1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Число частичных отрезков разбиения N:";
            // 
            // tbN
            // 
            this.tbN.Location = new System.Drawing.Point(235, 414);
            this.tbN.Name = "tbN";
            this.tbN.Size = new System.Drawing.Size(100, 20);
            this.tbN.TabIndex = 2;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(1184, 414);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 3;
            this.btnDraw.Text = "Начертить";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // plot2
            // 
            this.plot2.Location = new System.Drawing.Point(644, 12);
            this.plot2.Name = "plot2";
            this.plot2.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot2.Size = new System.Drawing.Size(620, 383);
            this.plot2.TabIndex = 4;
            this.plot2.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot2.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot2.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(632, -2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 400);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 441);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.plot2);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.tbN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.plot1);
            this.Name = "MainForm";
            this.Text = "Implicit Adams";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private OxyPlot.WindowsForms.PlotView plot1;
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbN;
        private System.Windows.Forms.Button btnDraw;
        private OxyPlot.WindowsForms.PlotView plot2;
        private System.Windows.Forms.Label label2;
    }
}

