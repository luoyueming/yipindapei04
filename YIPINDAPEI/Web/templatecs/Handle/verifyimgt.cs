using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NVelocity;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace NewXzc.Web.templatecs.Handle
{
    public class verifyimgt : Base.BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {

            //string id = GetString("id");
            //if (!string.IsNullOrEmpty(id))
            //{
            //    CreateCheckCodeImage(id);
            //}
            //else
            //{
                CreateCheckCodeImage(GenerateCheckCode());
            //}

        }
        #region 生成验证码
        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }
            //HttpCookie checkcode = new HttpCookie("CheckCode");
            //checkcode.Value = checkCode;
            //Response.AppendCookie(checkcode);

            //			Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));
            HttpContext.Current.Session["code"] = checkCode.ToLower();
            return checkCode;
        }

        private void CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;
            //System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Bitmap image = new Bitmap(60, 30);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(Color.White);
                for (int i = 0; i < 2; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Gulim", 14, FontStyle.Bold);
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.SeaGreen, Color.FromArgb(56, 147, 232), 1.2f, true);
                int k = 0;
                foreach (char s in checkCode)
                {
                    g.DrawString(s.ToString(), font, brush, k * 12, random.Next(5));
                    k++;
                }
                for (int i = 0; i < 80; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //g.DrawRectangle(new Pen(Color.FromArgb(181, 199, 222)), 0, 0, image.Width - 1, image.Height - 1);
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        #endregion validatecode
    }
}