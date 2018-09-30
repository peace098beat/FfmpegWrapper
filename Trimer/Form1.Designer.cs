namespace Trimer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox_Console = new System.Windows.Forms.RichTextBox();
            this.button_RunCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.numericUpDown_TrimingDuration = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TrimingDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox_Console
            // 
            this.richTextBox_Console.BackColor = System.Drawing.Color.Black;
            this.richTextBox_Console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Console.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_Console.Name = "richTextBox_Console";
            this.richTextBox_Console.Size = new System.Drawing.Size(514, 265);
            this.richTextBox_Console.TabIndex = 0;
            this.richTextBox_Console.Text = "";
            // 
            // button_RunCancel
            // 
            this.button_RunCancel.Location = new System.Drawing.Point(12, 12);
            this.button_RunCancel.Name = "button_RunCancel";
            this.button_RunCancel.Size = new System.Drawing.Size(75, 23);
            this.button_RunCancel.TabIndex = 1;
            this.button_RunCancel.Text = "RUN";
            this.button_RunCancel.UseVisualStyleBackColor = true;
            this.button_RunCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDown_TrimingDuration);
            this.splitContainer1.Panel1.Controls.Add(this.button_RunCancel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox_Console);
            this.splitContainer1.Panel2MinSize = 50;
            this.splitContainer1.Size = new System.Drawing.Size(514, 369);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 2;
            // 
            // numericUpDown_TrimingDuration
            // 
            this.numericUpDown_TrimingDuration.Location = new System.Drawing.Point(373, 12);
            this.numericUpDown_TrimingDuration.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_TrimingDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_TrimingDuration.Name = "numericUpDown_TrimingDuration";
            this.numericUpDown_TrimingDuration.Size = new System.Drawing.Size(120, 19);
            this.numericUpDown_TrimingDuration.TabIndex = 2;
            this.numericUpDown_TrimingDuration.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 369);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TrimingDuration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_Console;
        private System.Windows.Forms.Button button_RunCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.NumericUpDown numericUpDown_TrimingDuration;
    }
}

