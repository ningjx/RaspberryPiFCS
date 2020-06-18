using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Unit
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
        [TestMethod]
        public void TEst()
        {


            Play(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737EICAS\Sounds\CSC_fix.wav");







            int error = mciSendString($"open {@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737EICAS\Sounds\CSC_fix.wav"} alias myDivece", null, 0, new IntPtr(0));
            int werr = mciSendString($"open {@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\pullup.wav"} alias myDivece1", null, 0, new IntPtr(0));
            if (error == 0)
            {
                mciSendString("play myDivece", null, 0, IntPtr.Zero); //播放
                mciSendString("play myDivece1", null, 0, new IntPtr(0)); //播放

                //mciSendString("stop myDivece", null, 0, new IntPtr(0)); //播放
                //mciSendString("stop myDivece1", null, 0, new IntPtr(0)); //播放

                mciSendString("play myDivece", null, 0, IntPtr.Zero); //播放
                mciSendString("play myDivece1", null, 0, new IntPtr(0)); //播放

                mciSendString("stop myDivece", null, 0, new IntPtr(0)); //播放
                mciSendString("stop myDivece1", null, 0, new IntPtr(0)); //播放

            }
            else
            {
                StringBuilder errorText = new StringBuilder();
                mciGetErrorString(error, errorText, 50);
            }

        }
        
        /// <summary>
        /// 向媒体控制接口发送控制命令
        /// </summary>
        /// <param name="lpszCommand">命令，参见
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd743572(v=vs.85).aspx </param>
        /// <param name="lpszReturnString">命令返回的信息，如果没有需要返回的信息可以为null</param>
        /// <param name="cchReturn">指定返回信息的字符串大小</param>
        /// <param name="hwndCallback">回调句柄，如果命令参数中没有指定notify标识，可以为new IntPtr(0)</param>
        /// <returns>返回命令执行状态的错误代码</returns>
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string lpszCommand, StringBuilder returnString, int bufferSize, IntPtr hwndCallback);
        /// <summary>
        /// 返回对执行状态错误代码的描述
        /// </summary>
        /// <param name="errorCode">mciSendCommand或者mciSendString返回的错误代码</param>
        /// <param name="errorText">对错误代码的描述字符串</param>
        /// <param name="errorTextSize">指定字符串的大小</param>
        /// <returns>如果ERROR Code未知，返回false</returns>
        [DllImport("winmm.dll")]
        static extern bool mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);
        static Regex regex = new Regex("(Sounds)(.*)(wav)");

        public static void Play(string fileName,int sec = 1)
        {
            Task.Run(() =>
            {
                string device = regex.Match(fileName).Value.Replace(@"Sounds\", "").Replace(".wav", "");
                mciSendString($"close {device}", null, 0, IntPtr.Zero);
                mciSendString($"open {@fileName} alias {device}", null, 0, new IntPtr(0));
                mciSendString($"play {device}", null, 0, IntPtr.Zero);
                Thread.Sleep(sec * 1000);
                mciSendString($"close {device}", null, 0, IntPtr.Zero);
            });
        }
    }

    public static class Test
    {
        /// <summary>
        /// 向媒体控制接口发送控制命令
        /// </summary>
        /// <param name="lpszCommand">命令，参见
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd743572(v=vs.85).aspx </param>
        /// <param name="lpszReturnString">命令返回的信息，如果没有需要返回的信息可以为null</param>
        /// <param name="cchReturn">指定返回信息的字符串大小</param>
        /// <param name="hwndCallback">回调句柄，如果命令参数中没有指定notify标识，可以为new IntPtr(0)</param>
        /// <returns>返回命令执行状态的错误代码</returns>
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string lpszCommand, StringBuilder returnString, int bufferSize, IntPtr hwndCallback);
        /// <summary>
        /// 返回对执行状态错误代码的描述
        /// </summary>
        /// <param name="errorCode">mciSendCommand或者mciSendString返回的错误代码</param>
        /// <param name="errorText">对错误代码的描述字符串</param>
        /// <param name="errorTextSize">指定字符串的大小</param>
        /// <returns>如果ERROR Code未知，返回false</returns>
        [DllImport("winmm.dll")]
        static extern bool mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);


    }
}
