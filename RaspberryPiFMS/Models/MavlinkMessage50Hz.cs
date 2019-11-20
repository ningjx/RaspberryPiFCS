using MavLink4Net.Messages.Common;

namespace RaspberryPiFMS.Models
{
    public class MavlinkMessage50Hz
    {
        public AttitudeMessage AttitudeMessage = new AttitudeMessage();

        public void SetAttitudeMessage(float roll,float pitch,float yaw)
        {
            AttitudeMessage.Roll = roll;
            AttitudeMessage.Pitch = pitch;
            AttitudeMessage.Yaw = yaw;
        }
    }
}
