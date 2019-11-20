using MavLink4Net.Messages.Common;

namespace RaspberryPiFMS.Models
{
    public class MavlinkMessage1Hz
    {
        public BatteryStatusMessage BatteryStatusMessage = new BatteryStatusMessage();

        public void SetBatteryStatusMessage(short current,short temp,ushort[] vol)
        {
            BatteryStatusMessage.CurrentBattery = current;
            BatteryStatusMessage.Temperature = temp;
            BatteryStatusMessage.Voltages = vol;
            BatteryStatusMessage.Type = BatteryType.Lipo;
        }
    }
}
