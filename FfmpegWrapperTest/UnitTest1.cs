using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FfmpegWrapper;


namespace FfmpegWrapperTest
{
    [TestClass]
    public class コンストラクタ
    {
        [TestMethod, TestCategory("コンストラクタ")]
        [ExpectedException(typeof(FileNotFoundException))]
        public void 異常系_ffmpeg_exeが見つからない()
        {
            FfmpegWrapper.Ffmpeg ffmpeg = new FfmpegWrapper.Ffmpeg(@"C:\\error\\notfoundfile\\ffmpeg.exe");
        }

        [TestMethod, TestCategory("コンストラクタ")]
        public void 正常系_ffmpeg_exeが見つかる()
        {
            string appdir = AppDomain.CurrentDomain.BaseDirectory;
            string exe_path = Path.Combine(appdir, "ffmpeg.exe");

            FfmpegWrapper.Ffmpeg ffmpeg = new FfmpegWrapper.Ffmpeg(exe_path);
        }


        [TestMethod, TestCategory("コンストラクタ")]
        public void 正常系_ffmpeg_exeが見つかる2()
        {
            FfmpegWrapper.Ffmpeg ffmpeg = new FfmpegWrapper.Ffmpeg("ffmpeg.exe");
        }


        [TestMethod, TestCategory("コンストラクタ")]
        public void 正常系_ffmpeg_exeが見つかる3()
        {
            FfmpegWrapper.Ffmpeg ffmpeg = new FfmpegWrapper.Ffmpeg();
        }



    }

    [TestClass]
    public class メイン部
    {
        [TestMethod, TestCategory("トリミング部")]
        public void 正常系_0秒から10秒分だけトリミングして保存()
        {

            FfmpegWrapper.Ffmpeg ffmpeg = new Ffmpeg();

            string appdir = AppDomain.CurrentDomain.BaseDirectory;

            string InputFilePath = Path.Combine(appdir, "test.wmv");
            string OutputFilePath = Path.Combine(appdir, Ffmpeg.SubFileName(InputFilePath,2) );

            int start = 0;
            int duration = 5;

            ffmpeg.Triming(InputFilePath, OutputFilePath, start, duration);

            Assert.IsTrue(File.Exists(InputFilePath));
            Assert.IsTrue(File.Exists(OutputFilePath));

        }

        [TestMethod, TestCategory("でゅレーション")]
        public void 正常系_動画の長さを取得()
        {

            FfmpegWrapper.Ffmpeg ffmpeg = new Ffmpeg();

            string appdir = AppDomain.CurrentDomain.BaseDirectory;
            string InputFilePath = Path.Combine(appdir, "test.wmv");

            int duration = ffmpeg.GetVideoDuration(InputFilePath );
            
            Assert.IsTrue(duration == 30);

        }
    }


}
