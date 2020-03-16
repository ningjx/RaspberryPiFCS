using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Helper;
using RaspberryPiFCS.Interface;

namespace RaspberryPiFCS.Controller
{
    public class LoraController : IController
    {
        private UART _LoraUart;
        private Timer _timer = new Timer(10);
        private bool _lock = false;

        public LoraController()
        {
            _LoraUart = new UART(4665,4670);
            _timer.Elapsed += SendData;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void SendData(object sender, ElapsedEventArgs e)
        {
            if (_lock)
                return;
            if(StateDatasBus.FlightState.LoraDataMode== LoraDataMode.FlightData)
            {
                //发送飞行数据
            }
        }

        public void SendConfigData()
        {
            _lock = true;

            //发送配置数据
            _lock = false;
        }

        public void SendErrorData(ErrorType errorType, string message, Exception ex)
        {
            _lock = true;
            string data = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")} 异常类型：{errorType.GetType().Name} 消息：{message} 异常消息：{ex.Message} 异常位置：{ex.StackTrace}";
            byte[] bytes = new byte[10000];
            bytes[1] = 250;
            Encoding.UTF8.GetBytes(data).CopyTo(bytes,1);
            _LoraUart.SenBytes = bytes;
            _lock = false;
        }
    }
}
