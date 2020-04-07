using System;
using System.Collections.Generic;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.DirectX;
//using Microsoft.DirectX.DirectSound;


namespace PlaneInstrumentControlLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class Sound
    {
        private List<SoundRes> SoundBuffer = new List<SoundRes>();

        public Sound()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    lock (SoundBuffer)
                    {
                        int count = SoundBuffer.Count;
                        try
                        {
                            for (int i = 0; i < count; i++)
                            {
                                SoundBuffer[i].Play();
                            }
                        }
                        catch
                        {
                        }
                    }
                    Thread.Sleep(50);
                }
            });
        }

        /// <summary>
        /// 通过队列播放的方式，循环播放各种警告，每个声音在队列中只能唯一存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        public void PlayLoopInArray<T>(T sound)
        {
            Task.Run(() =>
            {
                lock (SoundBuffer)
                {
                    var instance = GetSoundRes(sound.GetHashCode());
                    if (!SoundBuffer.Contains(instance))
                        SoundBuffer.Add(instance);
                }
            });
        }

        /// <summary>
        /// 在循环队列中停止播放某个警告
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        public void StopPlayInArray<T>(T sound)
        {
            Task.Run(() =>
            {
                lock (SoundBuffer)
                {
                    var instance = GetSoundRes(sound.GetHashCode());
                    if (SoundBuffer.Contains(instance))
                        SoundBuffer.Remove(instance);
                }
            });
        }

        /// <summary>
        /// 异步播放某个声音
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        /// <param name="times">播放次数</param>
        public void PlaySync<T>(T sound, int times = 1)
        {
            GetSoundRes(sound.GetHashCode()).PlaySync(times);
        }

        /// <summary>
        /// 同步播放某个声音
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        /// <param name="times">播放次数</param>
        public void Play<T>(T sound, int times = 1)
        {
            GetSoundRes(sound.GetHashCode()).Play(times);
        }

        /// <summary>
        /// 播放某个声音指定次数之后，回调<see cref="Action"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        /// <param name="action"></param>
        /// <param name="times">播放次数</param>
        public void PlaySync<T>(T sound, Action action, int times = 1)
        {
            Task.Run(() =>
            {
                GetSoundRes(sound.GetHashCode()).Play(times);
                action.Invoke();
            });
        }

        /// <summary>
        /// 重写此方法，以获取指定音频资源
        /// </summary>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        protected virtual SoundRes GetSoundRes(int hashCode)
        {
            return new SoundRes();
        }
    }

    static class SystemSound
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

        static readonly Regex regex = new Regex("(Sounds)(.*)(wav)");

        public static void Play(string fileName, int miSec = 1000)
        {
            Task.Run(() =>
            {
                string device = regex.Match(fileName).Value.Replace(@"Sounds\", "").Replace(".wav", "");
                mciSendString($"close {device}", null, 0, IntPtr.Zero);
                mciSendString($"open {@fileName} alias {device}", null, 0, new IntPtr(0));
                mciSendString($"play {device}", null, 0, IntPtr.Zero);
                Thread.Sleep(miSec);
                mciSendString($"close {device}", null, 0, IntPtr.Zero);
            });
        }

        public static void Play(this SoundRes sound, int times = 1)
        {
            string device = regex.Match(sound.FileName).Value.Replace(@"Sounds\", "").Replace(".wav", "");
            for (int i = 0; i < times; i++)
            {
                mciSendString($"close {device}", null, 0, IntPtr.Zero);
                mciSendString($"open {sound.FileName} alias {device}", null, 0, new IntPtr(0));
                mciSendString($"play {device}", null, 0, IntPtr.Zero);
                Thread.Sleep(sound.MillionSec);
                mciSendString($"close {device}", null, 0, IntPtr.Zero);
            }
        }

        public static void PlaySync(this SoundRes sound, int times = 1)
        {
            Task.Run(() =>
            {
                string device = regex.Match(sound.FileName).Value.Replace(@"Sounds\", "").Replace(".wav", "");
                for (int i = 0; i < times; i++)
                {
                    mciSendString($"close {device}", null, 0, IntPtr.Zero);
                    mciSendString($"open {sound.FileName} alias {device}", null, 0, new IntPtr(0));
                    mciSendString($"play {device}", null, 0, IntPtr.Zero);
                    Thread.Sleep(sound.MillionSec);
                    mciSendString($"close {device}", null, 0, IntPtr.Zero);
                }
            });
        }
    }

    /// <summary>
    /// 声音对象
    /// </summary>
    public class SoundRes
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileName;
        /// <summary>
        /// 音频长度，单位ms
        /// </summary>
        public int MillionSec;

        public SoundRes()
        {

        }

        /// <summary>
        /// 默认音频时长为1000ms
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public SoundRes(string fileName)
        {
            FileName = fileName;
            MillionSec = 1000;
        }

        /// <summary>
        /// 音频资源
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="miSec">音频长度，单位ms</param>
        public SoundRes(string fileName, int miSec)
        {
            FileName = fileName;
            MillionSec = miSec;
        }
    }
}