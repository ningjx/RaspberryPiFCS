using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Handlers
{
    public delegate void DataHandler(byte[] bytes);
    public delegate void WatcherHandler();
}
