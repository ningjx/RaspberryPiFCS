using System.Collections.Generic;
using System.Linq;
using BrandonPotter.XBox;


namespace RaspberryPiClient.Helper
{
    public static class XboxHelper
    {
        public static List<XBoxController> _xBoxController = new List<XBoxController>();

        public static int Connect()
        {
            try
            {
                _xBoxController = XBoxController.GetConnectedControllers().ToList();
            }
            catch
            {

            }
            return _xBoxController.Count;
        }
    }
}
