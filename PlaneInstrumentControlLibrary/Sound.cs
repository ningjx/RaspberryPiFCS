using System;
using System.Collections.Generic;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.DirectX;
//using Microsoft.DirectX.DirectSound;


namespace PlaneInstrumentControlLibrary
{
    /// <summary>
    /// 封装SoundPlayer，同一时间只支持一个音频播放
    /// </summary>
    public class Sound
    {
        private List<int> SoundBuffer = new List<int>();
        private int SoundBufferLoop;
        private int currentPlay;
        private bool isPriority = false;
        private bool loopingEnable = false;
        private List<SoundPlayer> loopings = new List<SoundPlayer>();

        public Sound()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (isPriority)
                        continue;
                    try
                    {
                        lock (SoundBuffer)
                        {
                            foreach (var item in SoundBuffer)
                            {
                                currentPlay = item;
                                GetSoundPlayer(item).Play();
                                Thread.Sleep(1000);
                            }
                            SoundBuffer.Clear();
                        }
                        Thread.Sleep(50);
                    }
                    catch
                    {
                    }
                }
            });
        }

        /// <summary>
        /// 播放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        public void Play<T>(T sound)
        {
            Task.Run(() =>
            {
                lock (SoundBuffer)
                {
                    if (!SoundBuffer.Contains(sound.GetHashCode()))
                    {
                        SoundBuffer.Add(sound.GetHashCode());
                    }
                }
            });
        }

        /// <summary>
        /// 高优先级播放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        public void PlayNow<T>(T sound)
        {
            isPriority = true;
            GetSoundPlayer(currentPlay.GetHashCode()).Stop();
            GetSoundPlayer(sound.GetHashCode()).Play();
            isPriority = false;
        }

        /// <summary>
        /// 高优先级循环播放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        /// <param name="func">匿名函数返回True时，保持播放，返回false则停止播放</param>
        public void PlayLoopNow<T>(T sound,Func<bool> func)
        {
            isPriority = true;
            GetSoundPlayer(currentPlay.GetHashCode()).Stop();
            GetSoundPlayer(sound.GetHashCode()).PlayLooping();
            while (func.Invoke())
            {
                Thread.Sleep(100);
            }
            GetSoundPlayer(sound.GetHashCode()).Stop();
            isPriority = false;
        }

        /// <summary>
        /// 循环播放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        public void PlayLoop<T>(T sound)
        {
            Task.Run(() =>
            {
                while (isPriority)
                {
                    Thread.Sleep(500);
                }
                //currentPlay = SoundBufferLoop = sound.GetHashCode();
                var instance = GetSoundPlayer(sound.GetHashCode());
                loopings.Add(instance);
                instance.PlayLooping();
            });
        }

        /// <summary>
        /// 假循环的方式播放多个声音
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sounds"></param>
        public void PlayLoop<T>(List<T> sounds)
        {
            Task.Run(() =>
            {
                while (isPriority)
                {
                    Thread.Sleep(500);
                }
                //currentPlay = SoundBufferLoop = sound.GetHashCode();
                List<SoundPlayer> instances = new List<SoundPlayer>();
                sounds.ForEach(t => instances.Add(GetSoundPlayer(t.GetHashCode())));
                loopingEnable = true;
                while (loopingEnable)
                {
                    instances.ForEach(t => { t.Play();Thread.Sleep(1000); });
                }
            });
        }

        /// <summary>
        /// 停止当前假循环播放
        /// </summary>
        public void StopFakeLoop()
        {
            loopingEnable = false;
            GetSoundPlayer(SoundBufferLoop.GetHashCode()).Stop();
            //loopings.ForEach(t => t.Stop());
        }

        /// <summary>
        /// 停止指定的循环声音
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sound"></param>
        public void Stop<T>(T sound)
        {
            GetSoundPlayer(sound.GetHashCode()).Stop();
        }

        /// <summary>
        /// 停止所有真的循环的声音
        /// </summary>
        public void StopAllLoop()
        {
            lock (loopings)
            {
                loopings.ForEach(t => t.Stop());
                loopings.Clear();
            }
        }

        /// <summary>
        /// 返回<see cref="SoundPlayer">实例.需重写
        /// </summary>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        protected virtual SoundPlayer GetSoundPlayer(int hashCode)
        {
            return new SoundPlayer();
        }

    }


    //public static class Sound
    //{
    //    [DllImport("winmm.dll", SetLastError = true)]
    //    static extern bool PlaySound(string pszSound, UIntPtr hmod, uint fdwSound);
    //    /// <summary>
    //    /// 向媒体控制接口发送控制命令
    //    /// </summary>
    //    /// <param name="lpszCommand">命令，参见
    //    /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd743572(v=vs.85).aspx </param>
    //    /// <param name="lpszReturnString">命令返回的信息，如果没有需要返回的信息可以为null</param>
    //    /// <param name="cchReturn">指定返回信息的字符串大小</param>
    //    /// <param name="hwndCallback">回调句柄，如果命令参数中没有指定notify标识，可以为new IntPtr(0)</param>
    //    /// <returns>返回命令执行状态的错误代码</returns>
    //    [DllImport("winmm.dll")]
    //    static extern Int32 mciSendString(string lpszCommand, StringBuilder returnString, int bufferSize, IntPtr hwndCallback);
    //    [DllImport("winmm.dll")]
    //    private static extern long sndPlaySound(string lpszSoundName, long uFlags);

    //    /// <summary>
    //    /// 返回对执行状态错误代码的描述
    //    /// </summary>
    //    /// <param name="errorCode">mciSendCommand或者mciSendString返回的错误代码</param>
    //    /// <param name="errorText">对错误代码的描述字符串</param>
    //    /// <param name="errorTextSize">指定字符串的大小</param>
    //    /// <returns>如果ERROR Code未知，返回false</returns>
    //    [DllImport("winmm.dll")]
    //    static extern bool mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);


    //    [Flags]
    //    public enum SoundFlags
    //    {
    //        /// <summary>play synchronously (default)</summary>
    //        SND_SYNC = 0x0000,
    //        /// <summary>play asynchronously</summary>
    //        SND_ASYNC = 0x0001,
    //        /// <summary>silence (!default) if sound not found</summary>
    //        SND_NODEFAULT = 0x0002,
    //        /// <summary>pszSound points to a memory file</summary>
    //        SND_MEMORY = 0x0004,
    //        /// <summary>loop the sound until next sndPlaySound</summary>
    //        SND_LOOP = 0x0008,
    //        /// <summary>don’t stop any currently playing sound</summary>
    //        SND_NOSTOP = 0x0010,
    //        /// <summary>Stop Playing Wave</summary>
    //        SND_PURGE = 0x40,
    //        /// <summary>don’t wait if the driver is busy</summary>
    //        SND_NOWAIT = 0x00002000,
    //        /// <summary>name is a registry alias</summary>
    //        SND_ALIAS = 0x00010000,
    //        /// <summary>alias is a predefined id</summary>
    //        SND_ALIAS_ID = 0x00110000,
    //        /// <summary>name is file name</summary>
    //        SND_FILENAME = 0x00020000,
    //        /// <summary>name is resource name or atom</summary>
    //        SND_RESOURCE = 0x00040004
    //    }

    //    public static void Play(string strFileName)
    //    {
    //        PlaySound(strFileName, UIntPtr.Zero,
    //           (uint)(SoundFlags.SND_FILENAME | SoundFlags.SND_SYNC | SoundFlags.SND_NOSTOP));
    //    }

    //    public static void PlayLoop(string strFileName)
    //    {
    //        PlaySound(strFileName, UIntPtr.Zero,
    //       (uint)(SoundFlags.SND_LOOP));
    //    }

    //    public static void mciPlay(string device,string fileName,bool isRepeat)
    //    {
    //        string playCommand = "open " + fileName + $" alias {device}";
    //        var err = mciSendString(playCommand, null, 0, IntPtr.Zero);
    //        if (isRepeat)
    //             mciSendString($"play {device} repeat", null, 0, IntPtr.Zero);
    //         else
    //             mciSendString($"play {device}", null, 0, IntPtr.Zero);
    //    }


    //    public static void sndPlay(string strFileName)
    //    {
    //        sndPlaySound(strFileName, (long)SoundFlags.SND_SYNC);
    //    }

    //    public static void OpenFile(string device,string fileName)
    //    {
    //        string playCommand = "open " + fileName + $" type mpegvideo  alias {device}";
    //        var err = mciSendString(playCommand, null, 0, IntPtr.Zero);
    //        if (err != 0)
    //        {

    //        }
    //    }

    //    public static void SendCommang(string command,string device,string secCommand = "")
    //    {
    //        mciSendString($"{command} {device} {secCommand}", null, 0, IntPtr.Zero);
    //    }
    //}

    //public class SoundData
    //{
    //    public string FileName;
    //    public string DeviceName;

    //    public SoundData(string fileName, string device)
    //    {
    //        FileName = fileName;
    //        DeviceName = device;
    //        Sound.OpenFile(device, fileName);
    //    }

    //    public void Play()
    //    {
    //        Sound.SendCommang("play", DeviceName);
    //        Thread.Sleep(1000);
    //        Sound.SendCommang("close", DeviceName);
    //    }

    //    public void PlayLoop()
    //    {
    //        Sound.SendCommang("play", DeviceName, "repeat");
    //    }

    //    public void Pause()
    //    {
    //        Sound.SendCommang("pause", DeviceName);
    //    }

    //    public void Stop()
    //    {
    //        Sound.SendCommang("stop", DeviceName);
    //    }

    //    public void Close()
    //    {
    //        Sound.SendCommang("close", DeviceName);
    //    }
    //}

    //public class SoundData
    //{
    //    private Device device = new Device();
    //    private SecondaryBuffer buf;
    //    public SoundData(string flieName,string deviceName,Control control = null)
    //    {
    //        device.SetCooperativeLevel(IntPtr.Zero, CooperativeLevel.Priority);
    //        buf = new SecondaryBuffer(flieName, device);
    //    }
    //
    //    public void Play()
    //    {
    //        buf.Play(0, BufferPlayFlags.Default);
    //    }
    //
    //    public void PlayLoop()
    //    {
    //        buf.Play(0, BufferPlayFlags.Looping);
    //    }
    //
    //    public void Stop()
    //    {
    //        buf.Stop();
    //    }
    //}
}