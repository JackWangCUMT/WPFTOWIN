﻿namespace ManiXWPFtoWIN
{
    partial class Preview
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
            this.Gradientpanels = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Gradientpanels
            // 
            this.Gradientpanels.AutoScroll = true;
            this.Gradientpanels.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Gradientpanels.Location = new System.Drawing.Point(0, 0);
            this.Gradientpanels.Name = "Gradientpanels";
            this.Gradientpanels.Size = new System.Drawing.Size(386, 466);
            this.Gradientpanels.TabIndex = 0;
            // 
            // Preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 462);
            this.Controls.Add(this.Gradientpanels);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 800);
            this.MinimizeBox = false;
            this.Name = "Preview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview";
            this.Load += new System.EventHandler(this.Preview_Load);
            this.SizeChanged += new System.EventHandler(this.Preview_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Gradientpanels;



    }
}