using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NewXzc.Common;
using System.Net;
using System.Drawing;

namespace NewXzc.Web.Common
{
    public class Save_Img_Http
    {


        /**/
        /// <summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式，HW：指定高宽缩放（可能变形），W：指定宽，高按比例，H：指定高，宽按比例 ，Cut：指定高宽裁减（不变形）  </param>     
        public static void Cut_Img_Exe(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                 
                    break;
                case "W"://指定宽，高按比例                     
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例 
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图 
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


        ////用ASP.NET实现下载远程图片保存到本地的方法 保存抓取远程图片的方法
        ////1.利用WebRequest，WebResponse 类
        /// <summary>
        /// 用ASP.NET实现下载远程图片保存到本地的方法 保存抓取远程图片的方法，1.利用WebRequest，WebResponse 类
        /// </summary>
        /// <param name="img_url_http">远程图片的路径</param>
        /// <param name="imgtype">图片的扩展名（如：jpg，png，jpeg……）</param>
        /// <param name="imgpre">图片的前缀，0：默认，1：large_，2：middle_，3：small_</param>
        /// <returns></returns>
        public static string Save_Img_WebRequest(string img_url_http, string imgtype,int imgpre=0)
        {

            string imgurl = ImgHelper.GetCofigShowUrl();
            string uploaddir = ImgHelper.GetCofigUploadUrl().Replace("\\", "/");

            try
            {
                WebRequest wreq = WebRequest.Create(img_url_http);
                HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
                Stream s = wresp.GetResponseStream();
                System.Drawing.Image img;
                img = System.Drawing.Image.FromStream(s);
                //img.Save("D:\\aa.png", ImageFormat.Png);   //保存

                #region  保存图片到本地

                string savename = uploaddir + "Think_UserHead";

                if (!Directory.Exists(savename))
                {
                    Directory.CreateDirectory(savename);
                }

                savename += "/" + DateTime.Now.ToString("yyyy");

                if (!Directory.Exists(savename))
                {
                    Directory.CreateDirectory(savename);
                }

                savename += "/" + DateTime.Now.ToString("MM");

                if (!Directory.Exists(savename))
                {
                    Directory.CreateDirectory(savename);
                }

                savename += "/" + DateTime.Now.ToString("dd");

                if (!Directory.Exists(savename))
                {
                    Directory.CreateDirectory(savename);
                }

                //使用时间+随机数重命名文件
                string strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff");//取得时间字符串
                Random ran = new Random();
                string strRan = Convert.ToString(ran.Next(100, 999));//生成三位随机数

                string imgpre_val = "";
                if (imgpre == 0)
                {
                    imgpre_val = "";
                }
                else if (imgpre == 1)
                {
                    imgpre_val = "large_";
                }
                else if (imgpre == 2)
                {
                    imgpre_val = "middle_";
                }
                else if (imgpre == 3)
                {
                    imgpre_val = "small_";
                }

                savename += "/" + imgpre_val + strDateTime + strRan + "." + imgtype;

                img.Save(savename);   //保存

                //生成缩略图
                if (imgpre == 0)
                {
                    string newsavename = savename.Substring(0, savename.LastIndexOf("/")+1) + "large_" + savename.Substring(savename.LastIndexOf("/")+1);
                    Cut_Img_Exe(savename, newsavename, 100, 100, "Cut");

                    savename = newsavename;
                }
                Cut_Img_Exe(savename, savename.Replace("large_", "middle_"), 50, 50, "Cut");
                Cut_Img_Exe(savename, savename.Replace("large_", "small_"), 50, 50, "Cut");
                
                #endregion

                imgurl += savename.Replace(uploaddir, "");
            }
            catch (Exception ex)
            {

            }

            return imgurl;
        }

        ////2.利用 WebClient 类
        public static string Save_Img_WebClient(string img_url_http, string imgtype)
        {
            string imgurl = ImgHelper.GetCofigShowUrl();
            string uploaddir = ImgHelper.GetCofigUploadUrl().Replace("\\", "/");

            try
            {
                System.Net.WebClient my = new System.Net.WebClient();
                byte[] mybyte;

                mybyte = my.DownloadData(img_url_http);

                MemoryStream ms = new MemoryStream(mybyte);
                System.Drawing.Image img_webclient;
                img_webclient = System.Drawing.Image.FromStream(ms);
                //img.Save("D:\\aa.png", ImageFormat.Png);   //保存

                #region  保存图片到本地

                string savename = uploaddir + "Think_UserHead";

                if (!Directory.Exists(savename))
                {
                    Directory.CreateDirectory(savename);
                }

                savename += "/" + DateTime.Now.ToString("yyyy");

                if (!Directory.Exists(savename))
                {
                    Directory.CreateDirectory(savename);
                }

                savename += "/" + DateTime.Now.ToString("MM");

                if (!Directory.Exists(savename))
                {
                    Directory.CreateDirectory(savename);
                }

                savename += "/" + DateTime.Now.ToString("dd");

                if (!Directory.Exists(savename))
                {
                    Directory.CreateDirectory(savename);
                }

                //使用时间+随机数重命名文件
                string strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff");//取得时间字符串
                Random ran = new Random();
                string strRan = Convert.ToString(ran.Next(100, 999));//生成三位随机数
                savename += "/" + strDateTime + strRan + "." + imgtype;

                img_webclient.Save(savename);   //保存

                #endregion

                imgurl += savename.Replace(uploaddir, "");
            }
            catch (Exception ex)
            {

            }

            return imgurl;
        }

    }
}