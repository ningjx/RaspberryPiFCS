using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiFMS.Models;

namespace RaspberryPiFMS.Helper
{
    public class RemoteContrlHelper
    {
        public RemoteDataModel remoteData;

        private ThreadStart reciverT;
        private Thread reciver;


        public RemoteContrlHelper()
        {
            remoteData = new RemoteDataModel();
            reciverT = () => ReciveRemoteData();
            reciver = new Thread(reciverT);
            reciver.
        }


        public void 

        private void ReciveRemoteData()
        {

        }
    }
}
