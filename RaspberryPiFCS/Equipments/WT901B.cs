using System;
using RaspberryPiFCS.Helper;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using RaspberryPiFCS.SystemMessage;

namespace RaspberryPiFCS.Equipments
{
    /// <summary>
    /// 维特智能-WT901B&NEO6M GPS
    /// </summary>
    public class WT901B : IEquipment_UART
    {
        private bool _lock = false;

        private UARTHelper uart;

        public EquipmentData EquipmentData { get; } = new EquipmentData();

        public string ComName { get; } = string.Empty;


        /// <summary>
        /// 设备初始化
        /// </summary>
        /// <param name="comName">串口名</param>
        public WT901B(string comName)
        {
            ComName = comName;
        }

        /// <summary>
        /// 启动设备
        /// </summary>
        /// <returns></returns>
        public bool Lunch()
        {
            try
            {
                uart = new UARTHelper(ComName);
                uart.ReceivedEvent += ReceivedEvent;
                uart.Open();
                EquipmentData.IsEnable = true;
            }
            catch (Exception ex)
            {
                ErrorMessage.Add(Enum.ErrorType.Error, "启动WT901B失败！", ex);
                EquipmentData.IsEnable = false;
                return false;
            }
            return true;
        }

        private void ReceivedEvent(object sender, byte[] bytes)
        {
            DecodeData(bytes);
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="byteTemp"></param>
        private void DecodeData(byte[] byteTemp)
        {
            if (_lock)
                return;
            _lock = true;

            var byteList = byteTemp.GetBytesFromByte(new byte[] { 0x55, 0x50 }, 11);

            if (byteList.Count == 0)
            {
                _lock = false;
                return;
            }

            byteList.ForEach(t =>
            {
                double[] Data = new double[4];
                Data[0] = BitConverter.ToInt16(t, 2);
                Data[1] = BitConverter.ToInt16(t, 4);
                Data[2] = BitConverter.ToInt16(t, 6);
                Data[3] = BitConverter.ToInt16(t, 8);
                switch (t[1])
                {
                    case 0x50:
                        //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                        //ChipTime[0] = (short)(2000 + byteTemp[2]);
                        //ChipTime[1] = byteTemp[3];
                        //ChipTime[2] = byteTemp[4];
                        //ChipTime[3] = byteTemp[5];
                        //ChipTime[4] = byteTemp[6];
                        //ChipTime[5] = byteTemp[7];
                        //ChipTime[6] = BitConverter.ToInt16(byteTemp, 8);


                        break;
                    case 0x51:
                        //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                        StateDatasBus.FlightData.ExtraData.Temperature = Data[3] / 100.0;
                        Data[0] = Data[0] / 32768.0 * 16;
                        Data[1] = Data[1] / 32768.0 * 16;
                        Data[2] = Data[2] / 32768.0 * 16;

                        StateDatasBus.FlightData.NavData.Attitude.Aacceleration_X = (float)Data[0];
                        StateDatasBus.FlightData.NavData.Attitude.Aacceleration_Y = (float)Data[1];
                        StateDatasBus.FlightData.NavData.Attitude.Aacceleration_Z = (float)Data[2];
                        break;
                    case 0x52:
                        //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                        StateDatasBus.FlightData.ExtraData.Temperature = Data[3] / 100.0;
                        Data[0] = Data[0] / 32768.0 * 2000;
                        Data[1] = Data[1] / 32768.0 * 2000;
                        Data[2] = Data[2] / 32768.0 * 2000;
                        StateDatasBus.FlightData.NavData.Attitude.Palstance_X = (float)Data[0];
                        StateDatasBus.FlightData.NavData.Attitude.Palstance_Y = (float)Data[1];
                        StateDatasBus.FlightData.NavData.Attitude.Palstance_Z = (float)Data[2];
                        break;
                    case 0x53:
                        //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                        StateDatasBus.FlightData.ExtraData.Temperature = Data[3] / 100.0;
                        Data[0] = Data[0] / 32768.0 * 180;
                        Data[1] = Data[1] / 32768.0 * 180;
                        Data[2] = Data[2] / 32768.0 * 180;
                        StateDatasBus.FlightData.NavData.Attitude.Angle_X = (float)Data[0] + 180;
                        StateDatasBus.FlightData.NavData.Attitude.Angle_Y = (float)Data[1] + 180;
                        StateDatasBus.FlightData.NavData.Attitude.Angle_Z = (float)Data[2] + 180;
                        break;
                    case 0x54:
                        //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                        StateDatasBus.FlightData.ExtraData.Temperature = Data[3] / 100.0;
                        StateDatasBus.FlightData.NavData.Attitude.Magnetic_X = (float)Data[0];
                        StateDatasBus.FlightData.NavData.Attitude.Magnetic_Y = (float)Data[1];
                        StateDatasBus.FlightData.NavData.Attitude.Magnetic_Z = (float)Data[2];
                        break;
                    case 0x55:
                        //Port[0] = Data[0];
                        //Port[1] = Data[1];
                        //Port[2] = Data[2];
                        //Port[3] = Data[3];
                        break;

                    case 0x56:
                        StateDatasBus.FlightData.NavData.Attitude.BarometricAltitude = BitConverter.ToInt32(t, 6) / 100.0F;
                        break;

                    case 0x57:
                        StateDatasBus.FlightData.NavData.GPSData.Longitude = BitConverter.ToInt32(t, 2);
                        StateDatasBus.FlightData.NavData.GPSData.Latitude = BitConverter.ToInt32(t, 6);
                        break;

                    case 0x58:
                        StateDatasBus.FlightData.NavData.GPSData.GPSAltitude = BitConverter.ToInt16(t, 2) / 10.0F;
                        StateDatasBus.FlightData.NavData.GPSData.GPSYaw = BitConverter.ToInt16(t, 4) / 10.0F;
                        StateDatasBus.FlightData.NavData.GPSData.GroundSpeed = BitConverter.ToInt16(t, 6) / 1e3F;
                        break;

                    case 0x59:
                        //StateDatasBus.FlightData.PositionData.GPSData.GPSAltitude = BitConverter.ToInt16(t, 2) / 10.0F;
                        //StateDatasBus.FlightData.PositionData.GPSData.GPSHeading = BitConverter.ToInt16(t, 4) / 10.0F;
                        //StateDatasBus.FlightData.PositionData.GPSData.GroundSpeed = BitConverter.ToInt16(t, 6) / 1e3F;
                        break;

                    case 0x5A:
                        StateDatasBus.FlightData.NavData.GPSData.SatellitesCount = (int)Data[0];
                        StateDatasBus.FlightData.NavData.GPSData.PositionalAccuracy = (float)Data[1];
                        StateDatasBus.FlightData.NavData.GPSData.HorizontalAccuracy = (float)Data[2];
                        StateDatasBus.FlightData.NavData.GPSData.VerticalAccuracy = (float)Data[3];
                        break;

                    default:
                        break;
                }
            });
            _lock = false;
        }
    }
}
