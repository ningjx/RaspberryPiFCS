using MavLink;
using RaspberryPiFCS.Enum;
using System;
using LogType = RaspberryPiFCS.Enum.LogType;

namespace RaspberryPiFCS
{
    public delegate void DataHandler(byte[] bytes);
    public delegate void WatcherHandler();
    public delegate void ErrorMessageHandler(LogType errorType, string message, Exception ex);
    public delegate void UARTRecHandler(byte[] bytes);
    public delegate void MavlinkHandler(MavlinkPacket packet);
}
