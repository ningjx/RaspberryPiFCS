using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GMap.NET.WindowsForms
{
    public class GMapOverlayExt : GMapOverlay
    {
        public GMapOverlayExt()
        {
        }

        public GMapOverlayExt(string id) : base(id)
        {
        }

        protected GMapOverlayExt(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override void OnRender(Graphics g)
        {
            base.OnRender(g);
            g.DrawString("22", new Font("宋体", 20), new SolidBrush(Color.Black), Control.Width * (float)Control.Zoom / 2, Control.Height * (float)Control.Zoom / 2);

        }
    }
}
