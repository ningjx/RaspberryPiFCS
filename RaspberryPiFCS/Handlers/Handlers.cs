using RaspberryPiFCS.Enum;
using System;

namespace RaspberryPiFCS
{
    public delegate void DataHandler(byte[] bytes);
    public delegate void WatcherHandler();
    public delegate void ErrorMessageHandler(ErrorType errorType, string message, Exception ex);
    public delegate void UARTRecHandler(byte[] bytes);
}
