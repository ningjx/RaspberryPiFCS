using MavLink4Net.Messages.Common;

namespace RaspberryPiFCS.Models
{
    public static class MavlinkMessage1Hz
    {
        public static BatteryStatusMessage BatteryStatusMessage = new BatteryStatusMessage();

        public static void SetBatteryStatusMessage(short current,short temp,ushort[] vol)
        {
            BatteryStatusMessage.CurrentBattery = current;
            BatteryStatusMessage.Temperature = temp;
            BatteryStatusMessage.Voltages = vol;
            BatteryStatusMessage.Type = BatteryType.Lipo;
        }
    }
}
