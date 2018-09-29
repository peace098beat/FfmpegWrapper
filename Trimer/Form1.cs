using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FfmpegWrapper;

namespace Trimer
{
    public partial class Form1 : Form
    {
        public FfmpegWrapper.Ffmpeg ffmpeg;

        public Form1()
        {
            InitializeComponent();

            this.ffmpeg = new Ffmpeg();

            this.Text = "FFmpegCSWrapper Trimer ver " + ffmpeg.version;

            this.richTextBox_Console.AllowDrop = true;
            this.richTextBox_Console.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.richTextBox_Console.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WriteConsole("Please D&D Movie Files");
        }

        //****************************************************//


        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップされたすべてのファイル名を取得する
            string[] filePathes = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string file in filePathes)
            {
                WriteConsole(file);
            }

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        //****************************************************//

        // triming
        void RunTriming(string filename)
        {

            string InputFilePath;
            string OutFilePath;
            int start;
            int duration;
            //ffmpeg.Triming()

        }

        //****************************************************//

        enum FontColors
        {
            RED,
            BLUE,
            YELLOW,
            GREEN,
            WHITE,
            NONE
        }

        /// <summary>
        /// コンソールへ書き込み
        /// </summary>
        /// <param name="message"></param>
        private void WriteConsole(string message, FontColors font_color = FontColors.NONE)
        {
            switch (font_color)
            {
                case FontColors.RED:
                    richTextBox_Console.ForeColor = Color.Red;
                    break;
                case FontColors.BLUE:
                    richTextBox_Console.ForeColor = Color.Blue;
                    break;
                case FontColors.YELLOW:
                    richTextBox_Console.ForeColor = Color.Yellow;
                    break;
                case FontColors.GREEN:
                    richTextBox_Console.ForeColor = Color.Green;
                    break;
                case FontColors.WHITE:
                    richTextBox_Console.ForeColor = Color.White;
                    break;
                default:
                    richTextBox_Console.ForeColor = Color.Lime;
                    break;
            }
            richTextBox_Console.Text += message + Environment.NewLine;
        }

        //****************************************************//

    }
}
