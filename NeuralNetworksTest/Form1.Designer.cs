namespace NeuralNetworksTest
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
            this.components = new System.ComponentModel.Container();
            this.plot1 = new OxyPlot.WindowsForms.PlotView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.resetButton = new System.Windows.Forms.ToolStripMenuItem();
            this.trainButton = new System.Windows.Forms.ToolStripMenuItem();
            this.epochCountTextbox = new System.Windows.Forms.ToolStripTextBox();
            this.clearDataButton = new System.Windows.Forms.ToolStripMenuItem();
            this.epochsCompletedLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.errorLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.sampleFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.plot1.BackColor = System.Drawing.Color.White;
            this.plot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot1.Location = new System.Drawing.Point(0, 0);
            this.plot1.Name = "plot1";
            this.plot1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot1.Size = new System.Drawing.Size(584, 334);
            this.plot1.TabIndex = 0;
            this.plot1.Text = "plot1";
            this.plot1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetButton,
            this.trainButton,
            this.epochCountTextbox,
            this.clearDataButton,
            this.epochsCompletedLabel,
            this.errorLabel,
            this.sampleFunctionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 334);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // resetButton
            // 
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(47, 23);
            this.resetButton.Text = "Reset";
            this.resetButton.Click += new System.EventHandler(this.ResetToolStripMenuItem_Click);
            // 
            // trainButton
            // 
            this.trainButton.Checked = true;
            this.trainButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.trainButton.Name = "trainButton";
            this.trainButton.Size = new System.Drawing.Size(45, 23);
            this.trainButton.Text = "Train";
            this.trainButton.Click += new System.EventHandler(this.TrainToolStripMenuItem_Click);
            // 
            // epochCountTextbox
            // 
            this.epochCountTextbox.Name = "epochCountTextbox";
            this.epochCountTextbox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.epochCountTextbox.Size = new System.Drawing.Size(100, 23);
            this.epochCountTextbox.Text = "10000000";
            // 
            // clearDataButton
            // 
            this.clearDataButton.Name = "clearDataButton";
            this.clearDataButton.Size = new System.Drawing.Size(72, 23);
            this.clearDataButton.Text = "Clear data";
            this.clearDataButton.Click += new System.EventHandler(this.ClearDataToolStripMenuItem_Click);
            // 
            // epochsCompletedLabel
            // 
            this.epochsCompletedLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.epochsCompletedLabel.Name = "epochsCompletedLabel";
            this.epochsCompletedLabel.Size = new System.Drawing.Size(12, 23);
            // 
            // errorLabel
            // 
            this.errorLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(12, 23);
            // 
            // sampleFunctionToolStripMenuItem
            // 
            this.sampleFunctionToolStripMenuItem.Name = "sampleFunctionToolStripMenuItem";
            this.sampleFunctionToolStripMenuItem.Size = new System.Drawing.Size(105, 23);
            this.sampleFunctionToolStripMenuItem.Text = "SampleFunction";
            this.sampleFunctionToolStripMenuItem.Click += new System.EventHandler(this.SampleFunctionToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.plot1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ANN test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plot1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetButton;
        private System.Windows.Forms.ToolStripMenuItem trainButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripTextBox epochCountTextbox;
        private System.Windows.Forms.ToolStripMenuItem clearDataButton;
        private System.Windows.Forms.ToolStripMenuItem errorLabel;
        private System.Windows.Forms.ToolStripMenuItem epochsCompletedLabel;
        private System.Windows.Forms.ToolStripMenuItem sampleFunctionToolStripMenuItem;
    }
}
