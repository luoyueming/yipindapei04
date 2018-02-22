using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using NewXzc.Common.Model;
using System.Web.UI.WebControls;

namespace NewXzc.Common
{
    public class UploadImages
    {
        /// <summary>
        /// 生成缩略图-create by zhangzilong 2013-01-08 14:15
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <param name="thumbnailPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        public static void MakeThumbnailAlbum(string originalImagePath, string thumbnailPath, int width, int height)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            ////指定高宽裁减（不变形）             
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

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板

            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充

            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);


            try
            {

                //上传这三张缩略图到论坛的文件夹下
                if (towidth == 200 || towidth == 120 || towidth == 48)
                {
                    //以jpg格式保存缩略图

                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    //以jpg格式保存缩略图

                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

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




        /// <summary>
        /// 上传图片-create by zhangzilong in 9.15
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UploadListImgs(FileUpload fileUpload, List<ImgInfo> list)
        {
            bool result = false;
            try
            {
                foreach (ImgInfo model in list)
                {
                    if (model.Status == 0)
                    {
                        fileUpload.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(model.SavePath));
                    }
                    else
                    {
                        //上传缩略图上
                        MakeThumbnail(model.SavePath, model.ThumbPath, model.ImgWidth, model.ImgHeight);
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;

        }

        /// <summary>
        /// 生成缩略图-create by zhangzilong in 9.15
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <param name="thumbnailPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            ////指定高宽裁减（不变形）             
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

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板

            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充

            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);


            try
            {

                //上传这三张缩略图到论坛的文件夹下
                if (towidth == 200 || towidth == 120 || towidth == 48)
                {
                    //以jpg格式保存缩略图

                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    //以jpg格式保存缩略图

                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

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

        /// <summary>
        /// 判断文件或图片是否存在

        /// </summary>
        /// <param name="file"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool FileExists(string filePath)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(filePath);
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除多条图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public void DelYuanPoto(List<string> list)
        {
            if (list.Count > 0)
            {
                foreach (string filePath in list)
                {
                    string ServicePath = System.Web.HttpContext.Current.Server.MapPath(filePath);
                    if (File.Exists(ServicePath))
                    {
                        File.Delete(ServicePath);
                    }
                }
            }

        }
    }
}
