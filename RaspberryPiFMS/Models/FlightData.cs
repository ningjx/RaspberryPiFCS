using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RaspberryPiFMS.Models
{
    public class FlightData
    {
        [JsonProperty("导航数据")]
        public NavData PositionData = new NavData();
        [JsonProperty("引擎数据")]
        public EngineData EngineData = new EngineData();
        [JsonProperty("电源数据")]
        public PowerData PowerData = new PowerData();
        [JsonProperty("其他数据")]
        public ExtraData ExtraData = new ExtraData();
    }




    public class Attitude
    {
        //加速度
        public float Aacceleration_X;
        public float Aacceleration_Y;
        public float Aacceleration_Z;

        //角速度
        public float Palstance_X;
        public float Palstance_Y;
        public float Palstance_Z;

        //磁场
        public float Magnetic_X;
        public float Magnetic_Y;
        public float Magnetic_Z;

        //角度
        public float Angle_X;
        public float Angle_Y;
        public float Angle_Z;

        [JsonProperty("气压高度")]
        public float BarometricAltitude;
    }

    public class NavData
    {
        [JsonProperty("雷达高度")]
        public float MicroAltitude;

        [JsonProperty("GPS数据")]
        public GPSData GPSData;

        [JsonProperty("姿态仪数据")]
        public Attitude Attitude;
    }

    public class EngineData
    {
        public int Engine01_RPM;
        public int Engine02_RPM;
        public int Engine03_RPM;
        public int Engine04_RPM;
    }

    public class PowerData
    {
        public List<BatteryData> BatteryDatas;
    }

    public class BatteryData
    {
        public float Cell01_Voltage;
        public float Cell02_Voltage;
        public float Cell03_Voltage;
        public float Cell04_Voltage;
        public float Cell05_Voltage;
        public float Cell06_Voltage;
        public float TotalVoltage;
        public float Temperature;
    }

    public class GPSData
    {
        [JsonProperty("GPS高度")]
        public float GPSAltitude;
        [JsonProperty("GPS经度")]
        public float Longitude;
        [JsonProperty("GPS纬度")]
        public float Latitude;
        [JsonProperty("GPS航向")]
        public float GPSHeading;
        [JsonProperty("地速")]
        public float GroundSpeed;
        [JsonProperty("卫星数量")]
        public float SatellitesNum;
        [JsonProperty("GPS精度")]
        public float PositonPrecision;
        [JsonProperty("水平精度")]
        public float LevelPrecision;
        [JsonProperty("垂直精度")]
        public float VertPrecision;
    }

    public class ExtraData
    {
        public double Temperature;
    }
}
