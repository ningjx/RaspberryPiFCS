using MavLink4Net.Messages.Common;

namespace RaspberryPiFCS.Models
{
    public static class MavlinkMessage50Hz
    {
        public static AttitudeMessage AttitudeMessage = new AttitudeMessage();

        public static void SetAttitudeMessage(float roll,float pitch,float yaw)
        {
            AttitudeMessage.Roll = roll;
            AttitudeMessage.Pitch = pitch;
            AttitudeMessage.Yaw = yaw;
        }
    }
}
