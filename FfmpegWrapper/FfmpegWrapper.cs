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
        /// <summary>
        /// ffmpeg.exeへのパス
        /// </summary>
        public string FfmpegExePath = "";

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
            proc.StartInfo.CreateNoWindow = false;


            proc.Start();

            string output = proc.StandardOutput.ReadToEnd(); // 標準出力の読み取り
            string error = proc.StandardError.ReadToEnd();

            proc.WaitForExit();
            proc.Close();

            Console.WriteLine(":argument:"+ argument+Environment.NewLine);
            Console.WriteLine(output);
            Console.WriteLine(error);

        }

        private void p_Exited(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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
