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
                mciSendString("play myDivece", null, 0, IntPtr.Zero); //����
                mciSendString("play myDivece1", null, 0, new IntPtr(0)); //����

                //mciSendString("stop myDivece", null, 0, new IntPtr(0)); //����
                //mciSendString("stop myDivece1", null, 0, new IntPtr(0)); //����

                mciSendString("play myDivece", null, 0, IntPtr.Zero); //����
                mciSendString("play myDivece1", null, 0, new IntPtr(0)); //����

                mciSendString("stop myDivece", null, 0, new IntPtr(0)); //����
                mciSendString("stop myDivece1", null, 0, new IntPtr(0)); //����

            }
            else
            {
                StringBuilder errorText = new StringBuilder();
                mciGetErrorString(error, errorText, 50);
            }

        }
        
        /// <summary>
        /// ��ý����ƽӿڷ��Ϳ�������
        /// </summary>
        /// <param name="lpszCommand">����μ�
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd743572(v=vs.85).aspx </param>
        /// <param name="lpszReturnString">����ص���Ϣ�����û����Ҫ���ص���Ϣ����Ϊnull</param>
        /// <param name="cchReturn">ָ��������Ϣ���ַ�����С</param>
        /// <param name="hwndCallback">�ص������������������û��ָ��notify��ʶ������Ϊnew IntPtr(0)</param>
        /// <returns>��������ִ��״̬�Ĵ������</returns>
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string lpszCommand, StringBuilder returnString, int bufferSize, IntPtr hwndCallback);
        /// <summary>
        /// ���ض�ִ��״̬������������
        /// </summary>
        /// <param name="errorCode">mciSendCommand����mciSendString���صĴ������</param>
        /// <param name="errorText">�Դ������������ַ���</param>
        /// <param name="errorTextSize">ָ���ַ����Ĵ�С</param>
        /// <returns>���ERROR Codeδ֪������false</returns>
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
        /// ��ý����ƽӿڷ��Ϳ�������
        /// </summary>
        /// <param name="lpszCommand">����μ�
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd743572(v=vs.85).aspx </param>
        /// <param name="lpszReturnString">����ص���Ϣ�����û����Ҫ���ص���Ϣ����Ϊnull</param>
        /// <param name="cchReturn">ָ��������Ϣ���ַ�����С</param>
        /// <param name="hwndCallback">�ص������������������û��ָ��notify��ʶ������Ϊnew IntPtr(0)</param>
        /// <returns>��������ִ��״̬�Ĵ������</returns>
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string lpszCommand, StringBuilder returnString, int bufferSize, IntPtr hwndCallback);
        /// <summary>
        /// ���ض�ִ��״̬������������
        /// </summary>
        /// <param name="errorCode">mciSendCommand����mciSendString���صĴ������</param>
        /// <param name="errorText">�Դ������������ַ���</param>
        /// <param name="errorTextSize">ָ���ַ����Ĵ�С</param>
        /// <returns>���ERROR Codeδ֪������false</returns>
        [DllImport("winmm.dll")]
        static extern bool mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);


    }
}
