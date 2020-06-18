using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlaneInstrumentControlLibrary.SoundHandle;


namespace PlaneInstrumentControlLibrary.B737EICAS
{
    public partial class B737EICAS : InstrumentControl
    {
        B737EICASSound sound;
        public B737EICAS()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint, true);
            sound = new B737EICASSound();
        }
        Bitmap backGroung = new Bitmap(B737EICASResource.background);
        Bitmap cover1 = new Bitmap(B737EICASResource.cover1);
        Bitmap cover2 = new Bitmap(B737EICASResource.cover2);
        Bitmap insBack = new Bitmap(B737EICASResource.insBack);
        Bitmap insBack2 = new Bitmap(B737EICASResource.insBack2);
        Bitmap top = new Bitmap(B737EICASResource.top);
        Bitmap eng_fail_1 = new Bitmap(B737EICASResource.ENG_FAIL_1);
        Bitmap eng_fail_2 = new Bitmap(B737EICASResource.ENG_FAIL_2);
        Bitmap low_vol_1 = new Bitmap(B737EICASResource.LOW_VOL_1);
        Bitmap low_vol_2 = new Bitmap(B737EICASResource.LOE_VOL_2);
        Bitmap ail1 = new Bitmap(B737EICASResource.ail1);
        Bitmap ail2 = new Bitmap(B737EICASResource.ail2);
        Bitmap break1 = new Bitmap(B737EICASResource.flp1);
        Bitmap break2 = new Bitmap(B737EICASResource.flp2);
        Bitmap elev = new Bitmap(B737EICASResource.elev);
        Bitmap rudder = new Bitmap(B737EICASResource.rudder);


        SolidBrush drawBrush = new SolidBrush(Color.White);
        SolidBrush infoBrush = new SolidBrush(Color.FromArgb(115, 255, 87));
        SolidBrush warningBrush = new SolidBrush(Color.FromArgb(255, 179, 27));
        SolidBrush errorBrush = new SolidBrush(Color.FromArgb(255, 0, 0));


        float tem, rpm1, rpm2, power1, power2, cos1, cos2, volte1, volte2, roll, pitch, yaw, airbreak;
        List<EICASInfo> texts = new List<EICASInfo>();

        EngineStatus engineStatus1 = EngineStatus.Nor;
        EngineStatus engineStatus2 = EngineStatus.Nor;
        float scale;
        int x, y, rx, ry;

        Point insBackPosition1 = new Point(8, 26);
        Point insBackRotation1 = new Point(51, 69);
        Point insBackPosition2 = new Point(143, 26);
        Point insBackRotation2 = new Point(187, 69);
        Point insBack2Position1 = new Point(5, 113);
        Point insBack2Rotation1 = new Point(52, 159);
        Point insBack2Position2 = new Point(140, 113);
        Point insBack2Rotation2 = new Point(187, 159);

        protected override void OnPaint(PaintEventArgs pe)
        {
            //获取控件缩放比
            scale = (float)Width / backGroung.Width;

            //绘制背景
            pe.Graphics.DrawImage(backGroung, 0, 0, backGroung.Width * scale, backGroung.Height * scale);

            RotateImage(pe, insBack2, InterpolPhyToAngle(power1, 0, 100, 27, 209), insBack2Position1, insBack2Rotation1, scale);
            RotateImage(pe, insBack2, InterpolPhyToAngle(power2, 0, 100, 27, 209), insBack2Position2, insBack2Rotation2, scale);
            pe.Graphics.DrawImage(cover2, 0, 0, cover2.Width * scale, cover2.Height * scale);

            RotateImage(pe, insBack, InterpolPhyToAngle(rpm1, 0, 100, 27, 204), insBackPosition1, insBackRotation1, scale);
            RotateImage(pe, insBack, InterpolPhyToAngle(rpm2, 0, 100, 27, 204), insBackPosition2, insBackRotation2, scale);
            pe.Graphics.DrawImage(cover1, 0, 0, cover1.Width * scale, cover1.Height * scale);

            PainWarning(pe);

            pe.Graphics.DrawImage(top, 0, 0, top.Width * scale, top.Height * scale);

            TranslateImage(pe, ail1, 0, 0, new Point(0, (int)(roll * 0.3)), scale);
            TranslateImage(pe, ail2, 0, 0, new Point(0, -(int)(roll * 0.3)), scale);
            TranslateImage(pe, break1, 0, 0, new Point(0, -(int)(airbreak * 0.3)), scale);
            TranslateImage(pe, break2, 0, 0, new Point(0, -(int)(airbreak * 0.3)), scale);
            TranslateImage(pe, elev, 0, 0, new Point(0, (int)(pitch * 0.3)), scale);
            TranslateImage(pe, rudder, 0, 0, new Point((int)(yaw * 0.35), 0), scale);

            Font altFont = new Font("Arial", 12 * scale);
            Font altFont1 = new Font("Arial", 14 * scale);
            Font altFont2 = new Font("Arial", 11 * scale);
            Font altFont3 = new Font("Arial", 10 * scale);
            pe.Graphics.DrawString(tem.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 36 * scale, 0);
            pe.Graphics.DrawString(rpm1.ToString("f0").PadLeft(5, '0'), altFont1, drawBrush, 59 * scale, 41 * scale);
            pe.Graphics.DrawString(rpm2.ToString("f0").PadLeft(5, '0'), altFont1, drawBrush, 194 * scale, 41 * scale);
            pe.Graphics.DrawString(power1.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 58 * scale, 134 * scale);
            pe.Graphics.DrawString(power2.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 193 * scale, 134 * scale);
            pe.Graphics.DrawString(cos1.ToString("f0").PadLeft(3, '0'), altFont2, drawBrush, 51 * scale, 271 * scale);
            pe.Graphics.DrawString(cos2.ToString("f0").PadLeft(3, '0'), altFont2, drawBrush, 197 * scale, 271 * scale);
            pe.Graphics.DrawString(volte1.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 302 * scale, 430 * scale);
            pe.Graphics.DrawString(volte2.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 402 * scale, 430 * scale);

            //pe.Graphics.DrawImage(textRetan, 0, 0, textRetan.Width * scale, textRetan.Height * scale);
            int yPosition = 60;
            foreach (var item in texts)
            {
                int rowCount = 1;
                switch (item.WarningType)
                {
                    case WarningType.Info:
                        pe.Graphics.DrawString(GetString(item.Text, out rowCount), altFont3, infoBrush, 273 * scale, yPosition * scale);
                        break;
                    case WarningType.Warning:
                        pe.Graphics.DrawString(GetString(item.Text, out rowCount), altFont3, warningBrush, 273 * scale, yPosition * scale);
                        break;
                    case WarningType.Error:
                        pe.Graphics.DrawString(GetString(item.Text, out rowCount), altFont3, errorBrush, 273 * scale, yPosition * scale);
                        break;
                }
                yPosition += 20 * rowCount;
            }
        }

        public void SetValues(float tem, float rpm1, float rpm2, float power1, float power2, float cos1, float cos2, float volte1, float volte2, float roll, float pitch, float yaw, float airbreak, EngineStatus en1 = EngineStatus.NoChange, EngineStatus en2 = EngineStatus.NoChange)
        {
            this.tem = tem;
            this.rpm1 = rpm1;
            this.rpm2 = rpm2;
            this.power1 = power1;
            this.power2 = power2;
            this.cos1 = cos1;
            this.cos2 = cos2;
            this.volte1 = volte1;
            this.volte2 = volte2;
            this.rpm1 = rpm1;
            this.rpm1 = rpm1;
            this.rpm1 = rpm1;
            this.rpm1 = rpm1;
            this.rpm1 = rpm1;
            this.roll = roll;
            this.pitch = pitch;
            this.yaw = yaw;
            this.airbreak = airbreak;
            if (en1 != EngineStatus.NoChange)
            {
                if (engineStatus1 != en1 && en1 == EngineStatus.LowVol)
                {
                    if (engineStatus1 == EngineStatus.Fail && engineStatus2 != EngineStatus.Fail)
                        sound.cscSound.Stop();
                    sound.PlaySync(SoundType.scSound);
                }
                if (engineStatus1 != en1 && en1 == EngineStatus.Fail && engineStatus2 != EngineStatus.Fail)
                    sound.cscSound.PlayLooping();
                if (engineStatus1 != en1 && en1 == EngineStatus.Nor && engineStatus2 != EngineStatus.Fail)
                    sound.cscSound.Stop();
                engineStatus1 = en1;
            }
            if (en2 != EngineStatus.NoChange)
            {
                if (engineStatus2 != en2 && en2 == EngineStatus.LowVol)
                {
                    if (engineStatus2 == EngineStatus.Fail && engineStatus1 != EngineStatus.Fail)
                        sound.cscSound.Stop();
                    sound.PlaySync(SoundType.scSound);
                }
                if (engineStatus2 != en2 && en2 == EngineStatus.Fail && engineStatus1 != EngineStatus.Fail)
                    sound.cscSound.PlayLooping();
                if (engineStatus2 != en2 && en2 == EngineStatus.Nor && engineStatus1 != EngineStatus.Fail)
                    sound.cscSound.Stop();
                engineStatus2 = en2;
            }
            Refresh();
        }

        public void SetTexts(List<EICASInfo> texts)
        {
            this.texts = texts;
        }

        public void SetXY(int x, int y, int rx, int ry)
        {
            this.x = x;
            this.y = y;
            this.rx = rx;
            this.ry = ry;
            Refresh();
        }


        public void CancelWarning()
        {
            sound.cscSound.Stop();
        }

        private void PainWarning(PaintEventArgs pe)
        {
            switch (engineStatus1)
            {
                case EngineStatus.Fail:
                    pe.Graphics.DrawImage(eng_fail_1, 0, 0, eng_fail_1.Width * scale, eng_fail_1.Height * scale);
                    break;
                case EngineStatus.LowVol:
                    pe.Graphics.DrawImage(low_vol_1, 0, 0, low_vol_1.Width * scale, low_vol_1.Height * scale);
                    break;
            }
            switch (engineStatus2)
            {
                case EngineStatus.Fail:
                    pe.Graphics.DrawImage(eng_fail_2, 0, 0, eng_fail_2.Width * scale, eng_fail_2.Height * scale);
                    break;
                case EngineStatus.LowVol:
                    pe.Graphics.DrawImage(low_vol_2, 0, 0, low_vol_2.Width * scale, low_vol_2.Height * scale);
                    break;
            }
        }

        private string GetString(string str, out int count)
        {
            string buffer = str.Clone() as string;
            int splitCount = 12;
            count = 1;
            for (int i = splitCount; i < str.Length; i += splitCount)
            {
                buffer = buffer.Insert(i + count - 1, "\n");
                count++;
            }
            return buffer;
        }
    }

    public class EICASInfo
    {
        public WarningType WarningType;
        public string Text;
    }

    public enum WarningType
    {
        Info, Warning, Error
    }

    public enum EngineStatus
    {
        Fail, LowVol, Nor, Unknown, NoChange
    }

    class B737EICASSound : Sound
    {
        SoundRes scSound = new SoundRes
        {
            FileName = @"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737EICAS\Sounds\SC.wav",
            MillionSec = 1000
        };

        public SoundPlayer cscSound = new SoundPlayer(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737EICAS\Sounds\CSC_fix.wav");

        protected override SoundRes GetSoundRes(int hashCode)
        {
            switch (hashCode)
            {
                case 0: return scSound;
                default: return new SoundRes();
            }
        }
        //protected override SoundPlayer GetSoundPlayer(int hashCode)
        //{
        //    switch (hashCode)
        //    {
        //        case 0: return cscSound;
        //        case 1: return scSound;
        //        default: return new SoundPlayer();
        //    }
        //}
    }

    enum SoundType
    {
        scSound
    }
}
