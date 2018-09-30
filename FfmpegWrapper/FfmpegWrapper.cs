using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegWrapper
{

    public class Ffmpeg
    {
        public string version = "1.0";

        /// <summary>
        /// ffmpeg.exeへのパス
        /// </summary>
        public string FfmpegExePath = "";

        public string StdOutput;
        public string StdError;
        public string Arguments;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="FfmpegExePath">ffmpeg.exeへのパス.デフォルトは同ディレクトリを指定</param>
        public Ffmpeg(string FfmpegExePath = @"ffmpeg.exe")
        {
            if (System.IO.File.Exists(FfmpegExePath))
            {
                this.FfmpegExePath = FfmpegExePath;
            }
            else
            {
                throw new FileNotFoundException(FfmpegExePath + "が見つかりません.");
            }
        }

        /// <summary>
        /// 1. 時間切り取りする
        /// </summary>
        /// <param name="inputFilePath">入力ファイル</param>
        /// <param name="outputFilePath">出力パス</param>
        /// <param name="start">開始する[s]</param>
        /// <param name="duration">切り取る長さ[s]</param>
        public void Triming(string inputFilePath, string outputFilePath, int start, int duration)
        {
            string argument = $" -ss {start} -i \"{inputFilePath}\" -ss 0 -t {duration} -c:v copy -c:a copy -async 1 -strict -2 \"{outputFilePath}\" -y";
            // ※ -y : 上書き. 上書き確認[yes/no]を避けるため。
            this.Execute(argument);
        }

        /// <summary>
        /// プロセスの実行
        /// (TODO) 結果の文字列を戻す必要があるよね。
        /// </summary>
        /// <param name="argument">ffmpegの引数</param>
        public void Execute(string argument)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = this.FfmpegExePath;
            proc.StartInfo.Arguments = argument;

            proc.Exited += new EventHandler(p_Exited);
            proc.EnableRaisingEvents = true;

            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;


            proc.Start();

            string output = proc.StandardOutput.ReadToEnd(); // 標準出力の読み取り
            string error = proc.StandardError.ReadToEnd();

            proc.WaitForExit();
            proc.Close();

            Console.WriteLine(":argument:" + argument + Environment.NewLine);
            Console.WriteLine(output);
            Console.WriteLine(error);

            this.Arguments = argument;
            this.StdOutput = output;
            this.StdError = error;

        }

        private void p_Exited(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }


        public int GetVideoDuration(string inputFilePath)
        {

            string basePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
            string argument = string.Format("-v error -select_streams v:0 -show_entries stream=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1  \"{0}\" ", inputFilePath);
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Path.Combine(basePath, @"ffprobe.exe");
            proc.StartInfo.Arguments = argument;

            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;

            if (!proc.Start())
            {
                this.StdOutput = "[ERROR] Ffmpeg.GetVideoDuration: Error Starting!";
                this.StdError = "[ERROR] Ffmpeg.GetVideoDuration: Error Starting!";
                return -1;
            }


            this.Arguments = argument;
            this.StdOutput = proc.StandardOutput.ReadToEnd(); // 標準出力の読み取り
            this.StdError = proc.StandardError.ReadToEnd();

            // Remove the milliseconds
            string duration = this.StdOutput.Split('.')[0];
            if (duration.Length <= 0) duration = "0:00:00";

            proc.WaitForExit();
            proc.Close();

            // Stdの保存

            // 以下, durationの計算

            string HH = duration.Split(':')[0];
            string mm = duration.Split(':')[1];
            string ss = duration.Split(':')[2];

            int duration_sec = 60 * 60 * int.Parse(HH) + 60 * int.Parse(mm) + int.Parse(ss);

            return duration_sec;
        }


        /// <summary>
        /// ファイル名を整形する
        /// @"C:\\root\\test.wmv", 10 =>  @"C:\\root\\test_010.wmv"
        /// </summary>
        public static string SubFileName(string OriginFilePath, int SubId)
        {
            string base_dirpath = Path.GetDirectoryName(OriginFilePath);
            string base_name = Path.GetFileNameWithoutExtension(OriginFilePath);
            string ext = Path.GetExtension(OriginFilePath);
            string new_filename = $"{base_name}_{SubId.ToString("D3")}{ext}";
            string new_filepath = Path.Combine(base_dirpath, new_filename);
            return new_filepath;
        }

        public static void KillAllFfmpegExeProcess()
        {
            //notepadのプロセスを取得
            System.Diagnostics.Process[] ps =
                System.Diagnostics.Process.GetProcessesByName("ffmpeg");


            Console.WriteLine($"ffmpeg.exe kll. count is {ps.Count()}");

            foreach (System.Diagnostics.Process p in ps)
            {
                //プロセスを強制的に終了させる
                p.Kill();
            }
        }
    }
}
