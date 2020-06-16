namespace RaspberryPiFCS.Models
{
    public class RemoteSignal
    {
        public object Channel01;
        public object Channel02;
        public object Channel03;
        public object Channel04;
        public object Channel05;
        public object Channel06;
        public object Channel07;
        public object Channel08;
        public object Channel09;
        public object Channel10;
        public object Channel11;
        public object Channel12;
        public object Channel13;
        public object Channel14;
        public object Channel15;
        public object Channel16;


        public object this[int index]
        {
            set
            {
                switch (index)
                {
                    case 1:
                        Channel01 = value;
                        break;
                    case 2:
                        Channel02 = value;
                        break;
                    case 3:
                        Channel03 = value;
                        break;
                    case 4:
                        Channel04 = value;
                        break;
                    case 5:
                        Channel05 = value;
                        break;
                    case 6:
                        Channel06 = value;
                        break;
                    case 7:
                        Channel07 = value;
                        break;
                    case 8:
                        Channel08 = value;
                        break;
                    case 9:
                        Channel09 = value;
                        break;
                    case 10:
                        Channel10 = value;
                        break;
                    case 11:
                        Channel11 = value;
                        break;
                    case 12:
                        Channel12 = value;
                        break;
                    case 13:
                        Channel13 = value;
                        break;
                    case 14:
                        Channel14 = value;
                        break;
                    case 15:
                        Channel15 = value;
                        break;
                    case 16:
                        Channel16 = value;
                        break;
                }
            }
        }
    }
}
