using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Keystroke.API.Helpers
{
    public class Screenshot
    {
        public static string TakeScreenShot()
        {
            string screenShot = Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";

            using (Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(0,0,0,0,Screen.PrimaryScreen.Bounds.Size);
                }
                
                bitmap.Save(screenShot, ImageFormat.Png);
                return screenShot;
            }
        }
    }
}