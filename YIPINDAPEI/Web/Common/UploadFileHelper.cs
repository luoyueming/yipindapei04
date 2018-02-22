using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Web.UI.WebControls;
using NewXzc.Common;

namespace NewXzc.Web.Common
{
    public class UploadFileHelper
    {
        /// <summary>
        /// 保存为JPEG格式，支持压缩质量选项
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="FileName"></param>
        /// <param name="Qty"></param>
        /// <returns></returns>
        public static bool KiSaveAsJPEG(Bitmap bmp, string FileName, int Qty)
        {
            try
            {
                EncoderParameter p;
                EncoderParameters ps;

                ps = new EncoderParameters(1);

                p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Qty);
                ps.Param[0] = p;

                bmp.Save(FileName, GetCodecInfo("image/jpeg"), ps);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 保存JPG时用
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns>得到指定mimeType的ImageCodecInfo</returns>
        public static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }
        /// <summary>
        /// 获取文件或图片的存储路径，如：E:\xzcimgdata\
        /// </summary>
        /// <returns></returns>
        public static string GetCofigUploadUrl()
        {
            return UploadFile_Config.GetConfigString("imgUploadUrl");
        }
        /// <summary>
        /// 获取图片访问地址，如：http://xzcimg.d6315.com/
        /// </summary>
        /// <returns></returns>
        public static string GetCofigShowUrl()
        {
            return UploadFile_Config.GetConfigString("imgShowUrl");
        }

        /// <summary>
        /// 获取网站的访问地址，如：http://www.xzhicnang.com/
        /// </summary>
        /// <returns></returns>
        public static string GetCofigHttpUrl()
        {
            return UploadFile_Config.GetConfigString("HttpUrl");
        }

        /// <summary>
        /// 根据文件路径，删除图片 zhangzilong 2013-07-16
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


        #region  用于获取上传后文件或图片的路径  宋明亮

        /// <summary>
        /// 获取文件或图片最外层存储的文件夹名称
        /// </summary>
        /// <returns></returns>
        public static string Img_File_Folder()
        {
            return "/UploadFile/";
        }

        /// <summary>
        /// 获取文件所在的文件夹的名称,1:申请认证，可传任意类型；2：上传文件；3：上传图片；4：上传半身照片；5：上传身份证照片（正面）；6：上传身份证照片（反面）；7：手持身份证照片；8：营业执照；9：申请表上传；
        /// </summary>
        /// <param name="type">文件类型：1:申请认证，可传任意类型；2：上传文件；3：上传图片；4：上传半身照片；5：上传身份证照片（正面）；6：上传身份证照片（反面）；7：手持身份证照片；8：营业执照；9：申请表上传；</param>
        /// <returns></returns>
        public static string GetImgTypePre(int type)
        {
            string val = "";
            if (type == 1)//申请认证，可传任意类型
            {
                val = "/Identificate/";
            }
            else if (type == 2)//上传文件
            {
                val = "/Files/";
            }
            else if (type == 3)//上传图片
            {
                val = "/Images/";
            }
            else if (type == 4)//上传半身照片
            {
                val = "/Upper_Body/";
            }
            else if (type == 5)//上传身份证照片（正面）
            {
                val = "/Upper_Body_Face/";
            }
            else if (type == 6)//上传身份证照片（反面）
            {
                val = "/Upper_Body_Back/";
            }
            else if (type == 7)//手持身份证照片
            {
                val = "/Upper_Body_Hand/";
            }
            else if (type == 8)//营业执照
            {
                val = "/Business_License/";
            }
            else if (type == 9)//申请表上传（文件）
            {
                val = "/Apply_Table/";
            }


            if (type == 2)
            {
                val = "/Apply_Table/";
            }
            else if (type == 3)
            {
                val = "/Apply_Table/";
            }
            else if (type < 10)
            {
                val = "/Apply_Table/";
            }

            return val;
        }

        /// <summary>
        /// 获取用户上传图片或文件所在文件夹的名称
        /// </summary>
        /// <param name="use_type">0:项目，1：资金，2：申请认证 3:文章 4：投诉凭证</param>
        /// <returns></returns>
        public static string GetImg_Use_Pre(int use_type)
        {
            string val = "";

            if (use_type == 0)//项目
            {
                val = "/projects/";
            }
            else if (use_type == 1)//资金
            {
                val = "/moneys/";
            }
            else if (use_type == 2)//申请认证
            {
                val = "/identificate/";
            }
            else if (use_type == 3)//文章
            {
                val = "/article/";
            }
            else if (use_type == 4)//投诉凭证
            {
                val = "/report/";
            }
            else if (use_type == 5)//首页推荐
            {
                val = "/recommend_index/";
            }

            return val;
        }


        /// <summary>
        /// 返回上传文件夹下的文件的路径
        /// </summary>
        /// <param name="httpurl">当前页面的地址栏中的路径</param>
        /// <param name="img_url">当前图片的路径</param>
        /// <returns></returns>
        public static string GetUploadImgUrl(string httpurl, string img_url)
        {
            string cur_url = "";

            if (img_url.IndexOf("../") >= 0)
            {
                cur_url = img_url.Replace("../", "");
            }
            else
            {
                cur_url = img_url;
            }

            //if (httpurl.IndexOf("localhost") >= 0)
            //{
            //    cur_url = GetCofigShowUrl() + cur_url;
            //}
            //else if (httpurl.IndexOf("192.168.1.3") >= 0)
            //{
            //    cur_url = GetCofigShowUrl() + cur_url;
            //}
            //else
            //{
            //    cur_url = GetCofigShowUrl() + cur_url;
            //}

            cur_url = GetCofigShowUrl() + cur_url;

            return cur_url;
        }
        public static string GetUploadImgUrl(string img_url)
        {
            return GetCofigShowUrl() + img_url;
        }
        /// <summary>
        /// 返回当前文件所存储的文件夹的路径，如：E:/share/imgdata/
        /// </summary>
        /// <param name="httpurl"></param>
        /// <returns></returns>
        public static string GetUploadImgPre(string httpurl)
        {
            string UploadDir = @"" + GetCofigUploadUrl() + "";//@"E:/imgdata/";//图片保存的文件夹
            //if (httpurl.IndexOf("192.168.1.3") >= 0)
            //{
            //    UploadDir = @"Z:/";//@"E:/share/imgdata/";//图片保存的文件夹
            //}
            //else
            //{
            //    UploadDir = @"" + GetCofigUploadUrl() + "";//@"E:/share/imgdata/";//图片保存的文件夹
            //}
            return UploadDir;
        }

        /// <summary>
        /// 获取完整的图片的路径（项目或资金）
        /// </summary>
        /// <param name="httpurl">当前访问页面的url地址</param>
        /// <param name="userguid">发布人的userguid</param>
        /// <param name="type">文件类型：1:申请认证，可传任意类型；2：上传文件；3：上传图片；4：上传半身照片；5：上传身份证照片（正面）；6：上传身份证照片（反面）；7：手持身份证照片；8：营业执照；9：申请表上传；</param>
        /// <param name="filename">图片名称</param>
        /// <param name="issmall">是否显示封面图，true：是，false：否</param>
        /// <returns></returns>
        public static string Get_All_Img_Url(string httpurl, string filename, bool issmall)
        {
            string first_folder_name = "";// Img_File_Folder();
            string last_folder_name = "";

            if (filename.IndexOf("/") >= 0)
            {
                first_folder_name = filename.Substring(0, filename.LastIndexOf("/"));
                first_folder_name = first_folder_name.Substring(first_folder_name.IndexOf("/") + 1);
                last_folder_name = filename.Substring(filename.LastIndexOf("/") + 1);
            }

            if (filename.IndexOf("|") >= 0)
            {
                if (last_folder_name != "")
                {
                    last_folder_name = last_folder_name.Substring(0, last_folder_name.IndexOf("|"));
                }
                else
                {
                    last_folder_name = filename.Substring(0, filename.IndexOf("|"));
                }
            }

            if (filename.IndexOf("/") < 0 && filename.IndexOf("|") < 0)
            {
                last_folder_name = filename;
            }

            string img_url = "";// first_folder_name.Substring(first_folder_name.IndexOf("/") + 1) + userguid;
            //img_url = img_url + GetImgTypePre(type);

            if (issmall)
            {
                img_url = "small_" + last_folder_name;
            }
            else
            {
                img_url = last_folder_name;
            }

            if (first_folder_name != "")
            {
                img_url = first_folder_name + "/" + img_url;
            }

            img_url = GetUploadImgUrl(httpurl, img_url);

            return img_url;
        }

        /// <summary>
        /// 获取文件下载路径
        /// </summary>
        /// <param name="httpurl">当前访问页面的url地址</param>
        /// <param name="filename">图片或文件的名称</param>
        /// <param name="issmall">是否显示封面图，true：是，false：否</param>
        /// <returns></returns>
        public static string Get_DownLoad_File_Url(string httpurl, string filename, bool issmall)
        {
            string first_folder_name = "";// Img_File_Folder();
            string last_folder_name = "";

            if (filename.IndexOf("/") >= 0)
            {
                first_folder_name = filename.Substring(0, filename.LastIndexOf("/"));
                first_folder_name = first_folder_name.Substring(first_folder_name.IndexOf("/") + 1);
                last_folder_name = filename.Substring(filename.LastIndexOf("/") + 1);
            }

            if (filename.IndexOf("|") >= 0)
            {
                if (last_folder_name != "")
                {
                    last_folder_name = last_folder_name.Substring(0, last_folder_name.IndexOf("|"));
                }
                else
                {
                    last_folder_name = filename.Substring(0, filename.IndexOf("|"));
                }
            }

            if (filename.IndexOf("/") < 0 && filename.IndexOf("|") < 0)
            {
                last_folder_name = filename;
            }

            string img_url = "";// first_folder_name.Substring(first_folder_name.IndexOf("/") + 1) + userguid;
            //img_url = img_url + GetImgTypePre(type);

            if (issmall)
            {
                img_url = "small_" + last_folder_name;
            }
            else
            {
                img_url = last_folder_name;
            }

            if (first_folder_name != "")
            {
                img_url = first_folder_name + "/" + img_url;
            }

            img_url = UploadFileHelper.GetUploadImgPre(httpurl) + img_url;

            return img_url;
        }

        /// <summary>
        /// 获取文件的名称
        /// </summary>
        /// <param name="filename">文件、图片的名称</param>
        /// <param name="issmall">是否显示封面图，true：是，false：否</param>
        /// <param name="isnewfilename">是否显示原文件名称，true：是，false：否（显示重新命名后的文件或图片的名称）</param>
        /// <returns></returns>
        public static string Get_DownLoad_File_Name(string filename, bool issmall, bool isnewfilename)
        {
            string first_folder_name = "";// Img_File_Folder();
            string last_folder_name = "";

            if (filename.IndexOf("/") >= 0)
            {
                first_folder_name = filename.Substring(0, filename.LastIndexOf("/"));
                first_folder_name = first_folder_name.Substring(first_folder_name.IndexOf("/") + 1);
                last_folder_name = filename.Substring(filename.LastIndexOf("/") + 1);
            }

            if (filename.IndexOf("|") >= 0)
            {
                if (isnewfilename)
                {
                    if (last_folder_name != "")
                    {
                        last_folder_name = last_folder_name.Substring(last_folder_name.IndexOf("|") + 1);
                    }
                    else
                    {
                        last_folder_name = filename.Substring(filename.IndexOf("|") + 1);
                    }
                }
                else
                {
                    if (last_folder_name != "")
                    {
                        last_folder_name = last_folder_name.Substring(0, last_folder_name.IndexOf("|"));
                    }
                    else
                    {
                        last_folder_name = filename.Substring(0, filename.IndexOf("|"));
                    }
                }
            }

            if (filename.IndexOf("/") < 0 && filename.IndexOf("|") < 0)
            {
                last_folder_name = filename;
            }

            string img_url = "";// first_folder_name.Substring(first_folder_name.IndexOf("/") + 1) + userguid;
            //img_url = img_url + GetImgTypePre(type);

            if (issmall)
            {
                img_url = "small_" + last_folder_name;
            }
            else
            {
                img_url = last_folder_name;
            }

            return img_url;
        }

        #endregion

        ///Code highlighting produced by Actipro CodeHighlighter (freeware)http://www.CodeHighlighter.com/-->/// <summary>
        /// 获取一个图片按等比例缩小后的大小。
        /// </summary>
        /// <param name="maxWidth">需要缩小到的宽度</param>
        /// <param name="maxHeight">需要缩小到的高度</param>
        /// <param name="imageOriginalWidth">图片的原始宽度</param>
        /// <param name="imageOriginalHeight">图片的原始高度</param>
        /// <returns>返回图片按等比例缩小后的实际大小</returns>
        public static Size GetNewSize(int maxWidth, int maxHeight, int imageOriginalWidth, int imageOriginalHeight)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(imageOriginalWidth);
            double sh = Convert.ToDouble(imageOriginalHeight);
            double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight);

            if (sw < mw && sh < mh)
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = maxWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = maxHeight;
                w = (h * sw) / sh;
            }

            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }

        /// <summary>
        /// 对给定的一个图片（Image对象）生成一个指定大小的缩略图。
        /// </summary>
        /// <param name="originalImage">原始图片</param>
        /// <param name="thumMaxWidth">缩略图的宽度</param>
        /// <param name="thumMaxHeight">缩略图的高度</param>
        /// <returns>返回缩略图的Image对象</returns>
        public static System.Drawing.Image GetThumbNailImage(System.Drawing.Image originalImage, int thumMaxWidth, int thumMaxHeight)
        {
            Size thumRealSize = Size.Empty;
            System.Drawing.Image newImage = originalImage;
            Graphics graphics = null;

            try
            {
                thumRealSize = GetNewSize(thumMaxWidth, thumMaxHeight, originalImage.Width, originalImage.Height);
                newImage = new Bitmap(thumRealSize.Width, thumRealSize.Height);
                graphics = Graphics.FromImage(newImage);

                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.Clear(Color.Transparent);

                graphics.DrawImage(originalImage, new Rectangle(0, 0, thumRealSize.Width, thumRealSize.Height), new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);
            }
            catch { }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                    graphics = null;
                }
            }

            return newImage;
        }
        /// <summary>
        /// 对给定的一个图片文件生成一个指定大小的缩略图。
        /// </summary>
        /// <param name="originalImage">图片的物理文件地址</param>
        /// <param name="thumMaxWidth">缩略图的宽度</param>
        /// <param name="thumMaxHeight">缩略图的高度</param>
        /// <returns>返回缩略图的Image对象</returns>
        public static System.Drawing.Image GetThumbNailImage(string imageFile, int thumMaxWidth, int thumMaxHeight)
        {
            System.Drawing.Image originalImage = null;
            System.Drawing.Image newImage = null;

            try
            {
                originalImage = System.Drawing.Image.FromFile(imageFile);
                newImage = GetThumbNailImage(originalImage, thumMaxWidth, thumMaxHeight);
            }
            catch { }
            finally
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                    originalImage = null;
                }
            }

            return newImage;
        }
        /// <summary>
        /// 对给定的一个图片文件生成一个指定大小的缩略图，并将缩略图保存到指定位置。
        /// </summary>
        /// <param name="originalImageFile">图片的物理文件地址</param>
        /// <param name="thumbNailImageFile">缩略图的物理文件地址</param>
        /// <param name="thumMaxWidth">缩略图的宽度</param>
        /// <param name="thumMaxHeight">缩略图的高度</param>
        public static void MakeThumbNail(string originalImageFile, string thumbNailImageFile, int thumMaxWidth, int thumMaxHeight)
        {
            System.Drawing.Image newImage = GetThumbNailImage(originalImageFile, thumMaxWidth, thumMaxHeight);
            try
            {
                newImage.Save(thumbNailImageFile, ImageFormat.Jpeg);
            }
            catch
            { }
            finally
            {
                newImage.Dispose();
                newImage = null;
            }
        }
        /// <summary>
        /// 将一个图片的内存流调整为指定大小，并返回调整后的内存流。
        /// </summary>
        /// <param name="originalImageStream">原始图片的内存流</param>
        /// <param name="newWidth">新图片的宽度</param>
        /// <param name="newHeight">新图片的高度</param>
        /// <returns>返回调整后的图片的内存流</returns>
        public static MemoryStream ResizeImage(Stream originalImageStream, int newWidth, int newHeight)
        {
            MemoryStream newImageStream = null;
            System.Drawing.Image newImage = GetThumbNailImage(System.Drawing.Image.FromStream(originalImageStream), newWidth, newHeight);
            if (newImage != null)
            {
                newImageStream = new MemoryStream();
                newImage.Save(newImageStream, ImageFormat.Jpeg);
            }

            return newImageStream;
        }
        /// <summary>
        /// 将一个内存流保存为磁盘文件。
        /// </summary>
        /// <param name="stream">内存流</param>
        /// <param name="newFile">目标磁盘文件地址</param>
        public static void SaveStreamToFile(Stream stream, string newFile)
        {
            if (stream == null || stream.Length == 0 || string.IsNullOrEmpty(newFile))
            {
                return;
            }

            byte[] buffer = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buffer, 0, buffer.Length);
            FileStream fileStream = new FileStream(newFile, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Flush();
            fileStream.Close();
            fileStream.Dispose();
        }
        /// <summary>
        /// 对一个指定的图片加上图片水印效果。
        /// </summary>
        /// <param name="imageFile">图片文件地址</param>
        /// <param name="waterImage">水印图片（Image对象）</param>
        public static void CreateImageWaterMark(string imageFile, System.Drawing.Image waterImage)
        {
            if (string.IsNullOrEmpty(imageFile) || !File.Exists(imageFile) || waterImage == null)
            {
                return;
            }

            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(imageFile);

            if (originalImage.Width - 10 < waterImage.Width || originalImage.Height - 10 < waterImage.Height)
            {
                return;
            }

            Graphics graphics = Graphics.FromImage(originalImage);

            int x = originalImage.Width - waterImage.Width - 10;
            int y = originalImage.Height - waterImage.Height - 10;
            int width = waterImage.Width;
            int height = waterImage.Height;

            graphics.DrawImage(waterImage, new Rectangle(x, y, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
            graphics.Dispose();

            MemoryStream stream = new MemoryStream();
            originalImage.Save(stream, ImageFormat.Jpeg);
            originalImage.Dispose();

            System.Drawing.Image imageWithWater = System.Drawing.Image.FromStream(stream);

            imageWithWater.Save(imageFile);
            imageWithWater.Dispose();
        }
        /// <summary>
        /// 对一个指定的图片加上文字水印效果。
        /// </summary>
        /// <param name="imageFile">图片文件地址</param>
        /// <param name="waterText">水印文字内容</param>
        public static void CreateTextWaterMark(string imageFile, string waterText)
        {
            if (string.IsNullOrEmpty(imageFile) || string.IsNullOrEmpty(waterText) || !File.Exists(imageFile))
            {
                return;
            }

            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(imageFile);

            Graphics graphics = Graphics.FromImage(originalImage);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            SolidBrush brush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
            Font waterTextFont = new Font("Arial", 16, FontStyle.Regular);
            SizeF waterTextSize = graphics.MeasureString(waterText, waterTextFont);

            float x = (float)originalImage.Width - waterTextSize.Width - 10F;
            float y = (float)originalImage.Height - waterTextSize.Height - 10F;

            graphics.DrawString(waterText, waterTextFont, brush, x, y);

            graphics.Dispose();
            brush.Dispose();

            MemoryStream stream = new MemoryStream();
            originalImage.Save(stream, ImageFormat.Jpeg);
            originalImage.Dispose();

            System.Drawing.Image imageWithWater = System.Drawing.Image.FromStream(stream);

            imageWithWater.Save(imageFile);
            imageWithWater.Dispose();
        }

        /// <summary>
        /// 判断上传组件是否包含内容。
        /// </summary>
        /// <param name="fileUpload">ASP.NET 2.0标准上传组件</param>
        /// <returns>如果数据有效，则返回True，否则返回False</returns>
        public static bool IsAttachmentValid(FileUpload fileUpload)
        {
            if (fileUpload != null &&
                fileUpload.PostedFile != null &&
                !string.IsNullOrEmpty(fileUpload.PostedFile.FileName) &&
                fileUpload.PostedFile.ContentLength > 0)
            {
                return true;
            }
            return false;
        }


    }
}