using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SysWal
{
    class Captcha
    {
        private static Random random = new Random();
        /*
        public void CreateCaptcha()
        {
            Bitmap bmp = new Bitmap(Server.MapPath("~\\users\azslo\\onedrive\\documents\\visual studio 2015\\Projects\\Captcha.jpg"));
            MemoryStream mem = new MemoryStream();
            int width = bmp.Width;
            int height = bmp.Height;
            string fontfamily = "Arial";
            string text = Request.Cookies["Captcha"]["value"];

            Bitmap bitmap = new Bitmap(bmp,new Size(width, height));
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int XcopyRight = width - 150;
            int YcopyRight = height - 50;

            Rectangle rect;
            Font font;
            int newFontSize = 45;

            font = new Font(fontfamily, newFontSize, FontStyle.Italic);


        }
        */

        /*
                public static string RandomString(int length)
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    return new string(Enumerable.Repeat(chars, length)
                      .Select(s => s[random.Next(s.Length)]).ToArray());
                }
        */
        public string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }


}
}
