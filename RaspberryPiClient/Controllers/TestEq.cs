using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightDataModel;

namespace RaspberryPiClient.Controllers
{
    public static class TestEq
    {
        static SerialPort serialPort;
        public static FlightData FlightData;

        static TestEq()
        {
            FlightData = new FlightData();

            string portName = SerialPort.GetPortNames()[0];// "COM3";
            serialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += SerialPort_DataReceived; 
            serialPort.Open();
        }

        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] readBytes = new byte[serialPort.BytesToRead];
            serialPort.Read(readBytes, 0, readBytes.Length);
            DecodeData(readBytes);
        }


        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="byteTemp"></param>
        static void DecodeData(byte[] byteTemp)
        {

            var byteList = byteTemp.GetBytesFromByte(new byte[] { 0x55, 0x51 }, 11);

            if (byteList.Count == 0)
            {
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
                            FlightData.ExtraData.Temperature = Data[3] / 100.0;
                        Data[0] = Data[0] / 32768.0 * 16;
                        Data[1] = Data[1] / 32768.0 * 16;
                        Data[2] = Data[2] / 32768.0 * 16;

                        FlightData.Attitude.Aacceleration_X = (float)Data[0];
                        FlightData.Attitude.Aacceleration_Y = (float)Data[1];
                        FlightData.Attitude.Aacceleration_Z = (float)Data[2];
                        break;
                    case 0x52:
                            //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                            FlightData.ExtraData.Temperature = Data[3] / 100.0;
                        Data[0] = Data[0] / 32768.0 * 2000;
                        Data[1] = Data[1] / 32768.0 * 2000;
                        Data[2] = Data[2] / 32768.0 * 2000;
                        FlightData.Attitude.Palstance_X = (float)Data[0];
                        FlightData.Attitude.Palstance_Y = (float)Data[1];
                        FlightData.Attitude.Palstance_Z = (float)Data[2];
                        break;
                    case 0x53:
                            //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                            FlightData.ExtraData.Temperature = Data[3] / 100.0;
                        Data[0] = Data[0] / 32768.0 * 180;
                        Data[1] = Data[1] / 32768.0 * 180;
                        Data[2] = Data[2] / 32768.0 * 180;
                        FlightData.Attitude.Angle_X = (float)Data[0] + 180;
                        FlightData.Attitude.Angle_Y = (float)Data[1] + 180;
                        FlightData.Attitude.Angle_Z = (float)Data[2] + 180;
                        break;
                    case 0x54:
                            //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                        FlightData.ExtraData.Temperature = Data[3] / 100.0;
                        FlightData.Attitude.Magnetic_X = (float)Data[0];
                        FlightData.Attitude.Magnetic_Y = (float)Data[1];
                        FlightData.Attitude.Magnetic_Z = (float)Data[2];
                        break;
                    case 0x55:
                            //Port[0] = Data[0];
                            //Port[1] = Data[1];
                            //Port[2] = Data[2];
                            //Port[3] = Data[3];
                            break;

                    case 0x56:
                        FlightData.Attitude.BarometricAltitude = BitConverter.ToInt32(t, 6) / 100.0F;
                        break;

                    case 0x57:
                        FlightData.GPSData.Longitude = BitConverter.ToInt32(t, 2);
                        FlightData.GPSData.Latitude = BitConverter.ToInt32(t, 6);
                        break;

                    case 0x58:
                        FlightData.GPSData.GPSAltitude = BitConverter.ToInt16(t, 2) / 10.0F;
                        FlightData.GPSData.GPSYaw = BitConverter.ToInt16(t, 4) / 10.0F;
                        FlightData.GPSData.GPSSpeed = BitConverter.ToInt16(t, 6) / 1e3F;
                        break;

                    case 0x59:
                            //StateDatasBus.FlightData.PositionData.GPSData.GPSAltitude = BitConverter.ToInt16(t, 2) / 10.0F;
                            //StateDatasBus.FlightData.PositionData.GPSData.GPSHeading = BitConverter.ToInt16(t, 4) / 10.0F;
                            //StateDatasBus.FlightData.PositionData.GPSData.GroundSpeed = BitConverter.ToInt16(t, 6) / 1e3F;
                            break;

                    case 0x5A:
                        FlightData.GPSData.SatellitesCount = (int)Data[0];
                        FlightData.GPSData.PositionalAccuracy = (float)Data[1];
                        FlightData.GPSData.HorizontalAccuracy = (float)Data[2];
                        FlightData.GPSData.VerticalAccuracy = (float)Data[3];
                        break;

                    default:
                        break;
                }
            });
        }

    }
}
