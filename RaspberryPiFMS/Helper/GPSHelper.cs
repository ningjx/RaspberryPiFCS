using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Helper
{
    /// <summary>
    /// 维特智能-WT901B+NEO6M GPS
    /// </summary>
    public static class GPSHelper
    {
        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="byteTemp"></param>
        public static void DecodeData(byte[] byteTemp)
        {
            double[] Data = new double[4];

            Data[0] = BitConverter.ToInt16(byteTemp, 2);
            Data[1] = BitConverter.ToInt16(byteTemp, 4);
            Data[2] = BitConverter.ToInt16(byteTemp, 6);
            Data[3] = BitConverter.ToInt16(byteTemp, 8);
            switch (byteTemp[1])
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

                    StateDatasBus.FlightData.PositionData.Attitude.Aacceleration_X = (float)Data[0];
                    StateDatasBus.FlightData.PositionData.Attitude.Aacceleration_Y = (float)Data[1];
                    StateDatasBus.FlightData.PositionData.Attitude.Aacceleration_Z = (float)Data[2];
                    break;
                case 0x52:
                    //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                    StateDatasBus.FlightData.ExtraData.Temperature = Data[3] / 100.0;
                    Data[0] = Data[0] / 32768.0 * 2000;
                    Data[1] = Data[1] / 32768.0 * 2000;
                    Data[2] = Data[2] / 32768.0 * 2000;
                    StateDatasBus.FlightData.PositionData.Attitude.Palstance_X = (float)Data[0];
                    StateDatasBus.FlightData.PositionData.Attitude.Palstance_Y = (float)Data[1];
                    StateDatasBus.FlightData.PositionData.Attitude.Palstance_Z = (float)Data[2];
                    break;
                case 0x53:
                    //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                    StateDatasBus.FlightData.ExtraData.Temperature = Data[3] / 100.0;
                    Data[0] = Data[0] / 32768.0 * 180;
                    Data[1] = Data[1] / 32768.0 * 180;
                    Data[2] = Data[2] / 32768.0 * 180;
                    StateDatasBus.FlightData.PositionData.Attitude.Angle_X = (float)Data[0];
                    StateDatasBus.FlightData.PositionData.Attitude.Angle_Y = (float)Data[1];
                    StateDatasBus.FlightData.PositionData.Attitude.Angle_Z = (float)Data[2];
                    break;
                case 0x54:
                    //Data[3] = Data[3] / 32768 * double.Parse(textBox9.Text) + double.Parse(textBox8.Text);
                    StateDatasBus.FlightData.ExtraData.Temperature = Data[3] / 100.0;
                    StateDatasBus.FlightData.PositionData.Attitude.Magnetic_X = (float)Data[0];
                    StateDatasBus.FlightData.PositionData.Attitude.Magnetic_Y = (float)Data[1];
                    StateDatasBus.FlightData.PositionData.Attitude.Magnetic_Z = (float)Data[2];
                    break;
                case 0x55:
                    //Port[0] = Data[0];
                    //Port[1] = Data[1];
                    //Port[2] = Data[2];
                    //Port[3] = Data[3];
                    break;

                case 0x56:
                    StateDatasBus.FlightData.PositionData.Attitude.BarometricAltitude = BitConverter.ToInt32(byteTemp, 6) / 100.0F;
                    break;

                case 0x57:
                    StateDatasBus.FlightData.PositionData.GPSData.Longitude = BitConverter.ToInt32(byteTemp, 2);
                    StateDatasBus.FlightData.PositionData.GPSData.Latitude = BitConverter.ToInt32(byteTemp, 6);
                    break;

                case 0x58:
                    StateDatasBus.FlightData.PositionData.GPSData.GPSAltitude = BitConverter.ToInt16(byteTemp, 2) / 10.0F;
                    StateDatasBus.FlightData.PositionData.GPSData.GPSHeading = BitConverter.ToInt16(byteTemp, 4) / 10.0F;
                    StateDatasBus.FlightData.PositionData.GPSData.GroundSpeed = BitConverter.ToInt16(byteTemp, 6) / 1e3F;
                    break;
                default:
                    break;
            }
        }
    }
}
