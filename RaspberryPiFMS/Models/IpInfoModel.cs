namespace RaspberryPiFMS.Models
{
    public class IpInfoModel
    {
        public string ip;
        public string port;
        public double ping;

        public IpInfoModel(string ip,string port,double ping)
        {
            this.ip = ip;
            this.port = port;
            this.ping = ping;
        }
    }
}
