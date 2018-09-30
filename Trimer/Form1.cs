using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public Queue<string> Ques;

        public bool Cancel = false;

        public Form1()
        {
            InitializeComponent();

            this.ffmpeg = new Ffmpeg();
            this.Ques = new Queue<string>();

            this.Text = "FFmpegCSWrapper Trimer ver " + ffmpeg.version;

            this.richTextBox_Console.AllowDrop = true;
            this.richTextBox_Console.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.richTextBox_Console.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WriteConsole("Please D&D Movie Files.");
            WriteConsole("Run Convert Start ....");

            Task.Run(() => RunMainLoopAsync());


        }

        //****************************************************//

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップされたすべてのファイル名を取得する
            string[] filePathes = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string file in filePathes)
            {
                this.Ques.Enqueue(file);
                WriteConsole($"[ADD]{file}");
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

        private void button_Canel_Click(object sender, EventArgs e)
        {
            this.Cancel = true;
        }


        /// <summary>
        /// メインループ(非同期)
        /// </summary>
        async Task RunMainLoopAsync()
        {
            WriteConsole($"[INFO] TestMain");

            while (true)
            {

                // キャンセル処理
                if (this.Cancel == true)
                {
                    WriteConsole($"[CANCEL] メインループが、キャンセルされました", FontColors.RED);
                    this.Cancel = false;
                    return;
                }


                // キューが無いときは、ちょっと待つ
                if (Ques.Count == 0)
                {
                    //WriteConsole($".", NewlineFlg: false);

                    await Task.Delay(300);
                    continue;
                }

                // トリミング時間を取得する
                int triming_duration = (int)numericUpDown_TrimingDuration.Value;
                bool overwriteFlg = checkBox_OverWrite.Checked;

                // 処理するファイルを取得
                if (Ques.Count != 0)
                {
                    string QuesProgressBar = "";
                    for (int i = 1; i <= Ques.Count; i++)
                    {
                        QuesProgressBar += "|";
                    }

                    WriteConsole($"[QUES] {QuesProgressBar}", FontColors.BLUE);

                    // キューから取り出しトリミングを実行
                    string target_filepath = Ques.Dequeue();

                    WriteConsole($"[TARGET] {Path.GetFileName(target_filepath)}", FontColors.YELLOW);

                    await Task.Run(new Action(() =>
                     {
                         RunTriming(target_filepath, triming_duration, overwriteFlg);
                     }));
                }
            }

        }

        //****************************************************//

        /// <summary>
        /// (非同期) トリミング処理
        /// </summary>
        /// <param name="TargetFilePath"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public void RunTriming(string TargetFilePath, int duration, bool OverwriteFlg)
        {
            string InputFilePath = TargetFilePath;
            int total_duration = ffmpeg.GetVideoDuration(TargetFilePath);

            // 例外処理
            if (duration > total_duration)
            {
                WriteConsole("ファイルの時間が指定した切り取り時間よりも短いです。", FontColors.RED);
                return;
            }

            // Make Sub dir
            string SubDir = Path.Combine(Path.GetDirectoryName(InputFilePath), Path.GetFileNameWithoutExtension(InputFilePath));
            Directory.CreateDirectory(SubDir);

            // 切り出す動画の数
            int N = Convert.ToInt32(Math.Ceiling((double)total_duration / duration));

            for (int i = 1; i <= N; i++)
            {
                // キャンセル処理
                if (this.Cancel == true)
                {
                    WriteConsole($"[CANCEL] サブジョブが、キャンセルされました", FontColors.RED);
                    this.Cancel = false;
                    return;
                }

                // プログレスバーの表示
                string QuesProgressBar = "";
                for (int j = 1; j <= (N - i); j++)
                {
                    QuesProgressBar += "|";
                }
                WriteConsole($"[TRIMING] {QuesProgressBar}", FontColors.GREEN);

                // 入出力の設定
                string _OutFilePath = Ffmpeg.SubFileName(InputFilePath, i + 1);
                string _OutFileName = Path.GetFileName(_OutFilePath);
                string NewOutFilePath = Path.Combine(SubDir, _OutFileName);
                int start = i;
                WriteConsole($"[TRIMING] Start:{start}s, Duration:{duration}s, Total:{total_duration}s");

                // 上書きしない場合はなにもしない
                if (OverwriteFlg)
                {
                    // トリミング処理
                    ffmpeg.Triming(InputFilePath, NewOutFilePath, start, duration);
                }
                else
                {
                    WriteConsole($"[TRIMING] ファイルが存在します. 処理を回避します.");
                }



            }
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
        private void WriteConsole(string message, FontColors font_color = FontColors.NONE, bool NewlineFlg = true)
        {
            if (this.InvokeRequired)
            {
                // もし別スレッドの場合は、Invokeし、自分を呼び出す
                richTextBox_Console.BeginInvoke(new Action(() =>
                {
                    // 自分を呼び出し
                    WriteConsole(message, font_color, NewlineFlg);
                }));
                return;
            }
            else
            {
                // 本当の処理
                switch (font_color)
                {
                    case FontColors.RED:
                        richTextBox_Console.SelectionColor = Color.HotPink;
                        break;
                    case FontColors.BLUE:
                        richTextBox_Console.SelectionColor = Color.Aqua;
                        break;
                    case FontColors.YELLOW:
                        richTextBox_Console.SelectionColor = Color.Yellow;
                        break;
                    case FontColors.GREEN:
                        richTextBox_Console.SelectionColor = Color.Green;
                        break;
                    case FontColors.WHITE:
                        richTextBox_Console.SelectionColor = Color.White;
                        break;
                    default:
                        richTextBox_Console.SelectionColor = Color.Gray;
                        break;
                }


                string newline = "";
                if (NewlineFlg)
                {
                    newline = Environment.NewLine;
                }

                //リッチテキストボックスにフォーカスを移動
                richTextBox_Console.Focus();
                richTextBox_Console.SelectedText += (message + newline);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Cancel = true;
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cancel = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cancel = true;
            Application.Exit();
        }




        //****************************************************//

    }
}
