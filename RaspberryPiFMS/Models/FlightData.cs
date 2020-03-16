using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RaspberryPiFMS.Models
{
    public class FlightData
    {
        /// <summary>
        /// 导航数据
        /// </summary>
        [JsonProperty("导航数据")]
        public NavData PositionData = new NavData();

        /// <summary>
        /// 引擎数据
        /// </summary>
        [JsonProperty("引擎数据")]
        public EngineData EngineData = new EngineData();

        /// <summary>
        /// 电源数据
        /// </summary>
        [JsonProperty("电源数据")]
        public PowerData PowerData = new PowerData();

        /// <summary>
        /// 其他数据
        /// </summary>
        [JsonProperty("其他数据")]
        public ExtraData ExtraData = new ExtraData();
    }




    public class Attitude
    {
        /// <summary>
        /// 加速度X
        /// </summary>
        [JsonProperty("加速度X")]
        public float Aacceleration_X;

        /// <summary>
        /// 加速度Y
        /// </summary>
        [JsonProperty("加速度Y")]
        public float Aacceleration_Y;

        /// <summary>
        /// 加速度Z
        /// </summary>
        [JsonProperty("加速度Z")]
        public float Aacceleration_Z;

        /// <summary>
        /// 角速度Z
        /// </summary>
        [JsonProperty("角速度X")]
        public float Palstance_X;

        /// <summary>
        /// 角速度Y
        /// </summary>
        [JsonProperty("角速度Y")]
        public float Palstance_Y;

        /// <summary>
        /// 角速度Z
        /// </summary>
        [JsonProperty("角速度Z")]
        public float Palstance_Z;

        /// <summary>
        /// 磁场X
        /// </summary>
        [JsonProperty("磁场X")]
        public float Magnetic_X;

        /// <summary>
        /// 磁场Y
        /// </summary>
        [JsonProperty("磁场Y")]
        public float Magnetic_Y;

        /// <summary>
        /// 磁场Z
        /// </summary>
        [JsonProperty("磁场Z")]
        public float Magnetic_Z;

        /// <summary>
        /// 磁场X
        /// </summary>
        [JsonProperty("磁场X")]
        public float Angle_X;

        /// <summary>
        /// 磁场Y
        /// </summary>
        [JsonProperty("磁场Y")]
        public float Angle_Y;

        /// <summary>
        /// 磁场Z
        /// </summary>
        [JsonProperty("磁场Z")]
        public float Angle_Z;

        /// <summary>
        /// 气压高度
        /// </summary>
        [JsonProperty("气压高度")]
        public float BarometricAltitude;
    }

    public class NavData
    {
        /// <summary>
        /// 雷达高度
        /// </summary>
        [JsonProperty("雷达高度")]
        public float MicroAltitude;

        /// <summary>
        /// GPS数据
        /// </summary>
        [JsonProperty("GPS数据")]
        public GPSData GPSData = new GPSData();

        /// <summary>
        /// 姿态仪数据
        /// </summary>
        [JsonProperty("姿态仪数据")]
        public Attitude Attitude = new Attitude();
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
        /// <summary>
        /// GPS高度
        /// </summary>
        [JsonProperty("GPS高度")]
        public float GPSAltitude;

        /// <summary>
        /// GPS经度
        /// </summary>
        [JsonProperty("GPS经度")]
        public double Longitude;

        /// <summary>
        /// GPS纬度
        /// </summary>
        [JsonProperty("GPS纬度")]
        public double Latitude;

        /// <summary>
        /// GPS航向
        /// </summary>
        [JsonProperty("GPS航向")]
        public float GPSHeading;

        /// <summary>
        /// 地速
        /// </summary>
        [JsonProperty("地速")]
        public float GroundSpeed;

        /// <summary>
        /// GPS偏航
        /// </summary>
        [JsonProperty("GPS偏航")]
        public float GPSYaw;

        /// <summary>
        /// 卫星数量
        /// </summary>
        [JsonProperty("卫星数量")]
        public float SatellitesNum;

        /// <summary>
        /// GPS精度
        /// </summary>
        [JsonProperty("GPS精度")]
        public float PositonPrecision;

        /// <summary>
        /// 水平精度
        /// </summary>
        [JsonProperty("水平精度")]
        public float LevelPrecision;

        /// <summary>
        /// 垂直精度
        /// </summary>
        [JsonProperty("垂直精度")]
        public float VertPrecision;

        /// <summary>
        /// 卫星数
        /// </summary>
        [JsonProperty("卫星数")]
        public int SatellitesCount;

        /// <summary>
        /// 位置精度
        /// </summary>
        [JsonProperty("位置精度")]
        public float PositionalAccuracy;

        /// <summary>
        /// 水平精度
        /// </summary>
        [JsonProperty("水平精度")]
        public float HorizontalAccuracy;

        /// <summary>
        /// 垂直精度
        /// </summary>
        [JsonProperty("垂直精度")]
        public float VerticalAccuracy;


    }

    public class ExtraData
    {
        public double Temperature;
    }
}
