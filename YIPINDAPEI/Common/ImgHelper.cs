using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace NewXzc.Common
{
    public class ImgHelper
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
            return ConfigHelper.GetConfigString("imgUploadUrl");
        }
        /// <summary>
        /// 获取图片访问地址，如：http://xzcimg.d6315.com/
        /// </summary>
        /// <returns></returns>
        public static string GetCofigShowUrl()
        {
            return ConfigHelper.GetConfigString("imgShowUrl");
        }

        /// <summary>
        /// 获取网站的访问地址，如：http://www.xzhicnang.com/
        /// </summary>
        /// <returns></returns>
        public static string GetCofigHttpUrl()
        {
            return ConfigHelper.GetConfigString("HttpUrl");
        }

        /// <summary>
        /// 得到先知图片上传路径 zhangzilong 2013-07-16
        /// </summary>
        /// <returns></returns>
        /// 
        public static string GetUploadUrlFarseer()
        {
            return GetCofigUploadUrl() + @"farseer\";
        }
        public static string GetShowUrlFarseer()
        {
            
            return GetCofigShowUrl()+ @"farseer/";
        }
        /// <summary>
        /// 得到文章配图上传路径 zhangzilong 2013-07-25
        /// </summary>
        /// <returns></returns>
        public static string GetUploadUrlFigure()
        {
            return GetCofigUploadUrl() + @"figure\";
            //return @"E:\imgData\figure\";
            //return @"E:\share\imgdata\figure\";
        }


        /// <summary>
        /// 得到图片访问路径 zhangzilong 2013-09-13
        /// </summary>
        /// <returns></returns>
        public static string GetShowPicFrom()
        {
            return @"http://xzcimg.d6315.com/";
        }


        /// <summary>
        /// 得到文章配图访问路径 zhangzilong 2013-07-25
        /// </summary>
        /// <returns></returns>
        public static string GetShowUrlFigure()
        {
            
            return GetCofigShowUrl()+@"figure/";
        }

        /// <summary>
        /// 得到文章编辑器图片上传路径 zhangzilong 2013-07-25
        /// </summary>
        /// <returns></returns>
        public static string GetUploadUrlEditor()
        {
            return GetCofigUploadUrl() + @"hongren_editor\";
        }

        /// <summary>
        /// 得到文章编辑器图片访问路径 zhangzilong 2013-07-25
        /// </summary>
        /// <returns></returns>
        public static string GetShowUrlEditor()
        {
            return GetCofigShowUrl() + @"hongren_editor/";
        }

        /// <summary>
        /// 得到文章编辑器图片上传路径 zhangzilong 2013-08-01
        /// </summary>
        /// <returns></returns>
        public static string GetUploadHeadUrl()
        {
            return GetCofigUploadUrl() + @"head\";
            //return @"E:\imgData\head\";
            
        }

        /// <summary>
        /// 得到文章编辑器图片访问路径 zhangzilong 2013-08-01
        /// </summary>
        /// <returns></returns>
        public static string GetShowHeadUrl()
        {
            return GetCofigShowUrl() + @"head/";
            //return @"http://localhost:88/head/";
        }

        public static string GetOsPhoto()
        {
            return GetCofigShowUrl() + @"OfficeStory/";
        }
        public static string GetPlanPhoto()
        {
            return GetCofigShowUrl() + @"plan/";
        }
        public static string GetfarseerPhoto()
        {
            return GetCofigShowUrl() + @"farseer/";
        }
        //企业图片路径
        public static string GetCompanyPhoto()
        {
            return GetCofigShowUrl()+ @"CompanyImg/";

        }
        public static string GetUploadUrlCompanyImg()
        {
            return GetCofigUploadUrl() + @"CompanyImg\";
        }
        /// <summary>
        /// 删除图片 zhangzilong 2013-07-16
        /// </summary>
        /// <param name="fileFromPath">图片类型</param>
        /// <param name="type">图片所属类型：1先知</param>
        public static void DeleteImageFile(string fileFromPath,int type) 
        {
            if(type==1)
            {
                DeleteFile(GetUploadUrlFarseer() + fileFromPath);
                DeleteFile(GetUploadUrlFarseer() + "middle_" + fileFromPath);
                DeleteFile(GetUploadUrlFarseer() + "small_" + fileFromPath);
            }
            
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

        //获得系统头像
        public static string GetSystemImgUrl(string imgURL, int type)
        {
            string typeName = "";
            if (type == 1)
            {
                typeName = "large_";
            }
            else if (type == 2)
            {
                typeName = "middle_";
            }
            else if (type == 3)
            {
                typeName = "small_";
            }
            else
            {
                typeName = "";
            }

            return GetShowHeadUrl()+"/system/" + typeName + imgURL;
        }


        /// <summary>
        /// 返回上传文件夹下的文件的路径
        /// </summary>
        /// <param name="httpurl">当前页面的地址栏中的路径</param>
        /// <param name="img_url">当前图片的路径</param>
        /// <returns></returns>
        public static string ReturnUploadImgUrl(string httpurl, string img_url)
        {
            string cur_url = "";

            if (img_url.IndexOf("../") >= 0)
            {
                cur_url = img_url.Replace("../","");
            }

            if (httpurl.IndexOf("localhost") >= 0)
            {
                cur_url = "http://localhost:8080/" + cur_url;
            }
            else if (httpurl.IndexOf("192.168.1.3") >= 0)
            {
                cur_url = "http://192.168.1.3:88/" + cur_url;
            }
            else
            {
                cur_url = GetCofigShowUrl() + cur_url;
            }

            return cur_url;
        }

        /// <summary>
        /// 返回当前文件所存储的文件夹的路径
        /// </summary>
        /// <param name="httpurl"></param>
        /// <returns></returns>
        public static string ReturnUploadImgPre(string httpurl)
        {
            string UploadDir = @""+GetCofigUploadUrl()+"";//@"E:/imgdata/";//图片保存的文件夹
            //if (httpurl.IndexOf("192.168.1.3") >= 0)
            //{
            //    UploadDir = @"E:/share/imgdata/";//图片保存的文件夹
            //}
            //else
            //{
            //    UploadDir = @"" + GetCofigUploadUrl() + "";//@"E:/share/imgdata/";//图片保存的文件夹
            //}
            return UploadDir;
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
        /// 返回当前文件所存储的文件夹的路径，如：E:/share/imgdata/
        /// </summary>
        /// <param name="httpurl"></param>
        /// <returns></returns>
        public static string GetUploadImgPre(string httpurl)
        {
            string UploadDir = @"" + GetCofigUploadUrl() + "";//@"E:/imgdata/";//图片保存的文件夹
            if (httpurl.IndexOf("192.168.1.3") >= 0)
            {
                UploadDir = @"Z:/";//@"E:/share/imgdata/";//图片保存的文件夹
            }
            else
            {
                UploadDir = @"" + GetCofigUploadUrl() + "";//@"E:/share/imgdata/";//图片保存的文件夹
            }
            return UploadDir;
        }

    

        /// <summary>
        /// 获取图片的上传路径
        /// </summary>
        /// <param name="key">第一级文件夹名称</param>
        /// <returns></returns>
        public static string GetSaveImagePath(string key)
        {
            string path = GetCofigUploadUrl() + "/" + key + "/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }


        #region 获取不同格式的图片路径
        /// <summary>
        /// 获取不同格式的图片路径（返回结果如：http://resume/a.jpg）
        /// </summary>
        /// <param name="imgURL">数据存储图片路径</param>
        /// <param name="type">1:大图，2中图，3小图</param>
        /// <returns></returns>
        public static string GetImgUrl(string imgURL, int type)
        {
            string UploadPath = "";
            string url = "";

            //是自己上传的头像
            if (imgURL.IndexOf("/") >= 0)
            {
                int num = imgURL.LastIndexOf('/');
                string front = imgURL.Substring(0, num + 1);
                string end = imgURL.Substring(num + 1);

                //string cur_http_urls = ReturnUrl.ReturnCurUrl().ToLower();

                if (type == 1)
                {
                    UploadPath = "large_";
                }
                else if (type == 2)
                {
                    UploadPath = "middle_";
                }
                else if (type == 3)
                {
                    UploadPath = "small_";
                }

                url=GetCofigShowUrl() + front + UploadPath + end;

            }
            else//还没有上传过头像
            {
                if (type == 1)
                {
                    UploadPath = imgURL;
                }
                else if (type == 2)
                {
                    UploadPath = "mid_"+imgURL;
                }
                else if (type == 3)
                {
                    UploadPath = "small_"+imgURL;
                }

                url = GetCofigHttpUrl() + "template/images/default_head/" + UploadPath;
            }

            return url;
        }


        /// <summary>
        /// 获取不同格式的图片路径（返回结果如：D:/resume/a.jpg）
        /// </summary>
        /// <param name="imgURL">数据存储图片路径</param>
        /// <param name="type">1:大图，2中图，3小图</param>
        /// <returns></returns>
        public static string GetImgUrl(string imgURL, int type,int local)
        {
            string UploadPath = "";
            string url = "";

            //是自己上传的头像
            if (imgURL.IndexOf("/") >= 0)
            {
                int num = imgURL.LastIndexOf('/');
                string front = imgURL.Substring(0, num + 1);
                string end = imgURL.Substring(num + 1);

                //string cur_http_urls = ReturnUrl.ReturnCurUrl().ToLower();

                if (type == 1)
                {
                    UploadPath = "large_";
                }
                else if (type == 2)
                {
                    UploadPath = "middle_";
                }
                else if (type == 3)
                {
                    UploadPath = "small_";
                }

                url = GetCofigUploadUrl() + front + UploadPath + end;

            }
            else//还没有上传过头像
            {
                if (type == 1)
                {
                    UploadPath = imgURL;
                }
                else if (type == 2)
                {
                    UploadPath = "mid_" + imgURL;
                }
                else if (type == 3)
                {
                    UploadPath = "small_" + imgURL;
                }

                url = HttpContext.Current.Server.MapPath("~/template/images/default_head/") + UploadPath;
            }

            return url;
        }

        #endregion

        #region 获取不同格式的图片路径
        /// <summary>
        /// 获取不同格式的图片路径
        /// </summary>
        /// <param name="imgURL">数据存储图片路径</param>
        /// <param name="type">1:大图，2中图，3小图</param>
        /// <returns></returns>
        public static string GetImgUrl(string imgURL, int type,string path)
        {
            string UploadPath = "";
            string url = "";

            //是自己上传的头像
            if (imgURL.IndexOf("/") >= 0)
            {
                int num = imgURL.LastIndexOf('/');
                string front = imgURL.Substring(0, num + 1);
                string end = imgURL.Substring(num + 1);

                //string cur_http_urls = ReturnUrl.ReturnCurUrl().ToLower();

                if (type == 1)
                {
                    UploadPath = "large_";
                }
                else if (type == 2)
                {
                    UploadPath = "middle_";
                }
                else if (type == 3)
                {
                    UploadPath = "small_";
                }

                url = path + front + UploadPath + end;

            }
            else//还没有上传过头像
            {
                if (type == 1)
                {
                    UploadPath = imgURL;
                }
                else if (type == 2)
                {
                    UploadPath = "mid_" + imgURL;
                }
                else if (type == 3)
                {
                    UploadPath = "small_" + imgURL;
                }

                url = path + "template/images/default_head/" + UploadPath;
            }

            return url;
        }
        #endregion


        /// <summary>
        /// 获取图片访问地址，如：http://xzcimg.d6315.com/
        /// </summary>
        /// <returns></returns>
        public static string GetMoney()
        {
            return ConfigHelper.GetConfigString("MoneyCount");
        }


        /// <summary>
        /// 获取用户头像，1：大头像，2：中头像，3：小头像
        /// </summary>
        /// <param name="imgURL">头像地址</param>
        /// <param name="type">1：大头像，2：中头像，3：小头像</param>
        /// <returns></returns>
        public static string Return_User_Head(string imgURL, int type)
        {
            string typeName = "";
            if (type == 1)
            {
                typeName = "large_";
            }
            else if (type == 2)
            {
                typeName = "middle_";
            }
            else if (type == 3)
            {
                typeName = "small_";
            }
            else
            {
                typeName = "";
            }

            string cur_img_url = imgURL;

            if (imgURL.IndexOf("/") < 0)
            {
                cur_img_url = "/template/img/default_head/" + typeName + imgURL;
            }
            else
            {
                cur_img_url = ImgHelper.GetCofigShowUrl() + imgURL.Substring(0, imgURL.LastIndexOf("/")+1) + typeName + imgURL.Substring(imgURL.LastIndexOf("/")+1);
            }

            return cur_img_url;
        }


        #region  获取通过UPLOAD上传后的图片的路径


        /// <summary>
        /// 返回上传文件夹下的文件的路径
        /// </summary>
        /// <param name="httpurl">当前页面的地址栏中的路径</param>
        /// <param name="img_url">当前图片的路径</param>
        /// <param name="type">1：大图，2：小图</param>
        /// <returns></returns>
        public static string Get_UploadImgUrl(string img_url, int type)
        {
            string cur_url = "";

            if (img_url != "")
            {
                if (type == 1)
                {
                    cur_url = GetCofigShowUrl() + img_url;
                }
                else
                {
                    cur_url = GetCofigShowUrl() + img_url.Substring(0, img_url.LastIndexOf("/") + 1) + "small_" + img_url.Substring(img_url.LastIndexOf("/") + 1);
                }
            }

            return cur_url;
        }



        /// <summary>
        /// 获取后台红人用户头像，1：大头像（400*400），2：中头像（300*300），3：小头像（200*200）
        /// </summary>
        /// <param name="imgURL">头像地址</param>
        /// <param name="type">1：大头像（400*400），2：中头像（300*300），3：小头像（200*200）</param>
        /// <returns></returns>
        public static string Get_Upload_User_Head(string imgURL, int type)
        {
            string typeName = "";
            if (type == 1)
            {
                typeName = "large_";
            }
            else if (type == 2)
            {
                typeName = "middle_";
            }
            else if (type == 3)
            {
                typeName = "small_";
            }
            else
            {
                typeName = "";
            }

            string cur_img_url = imgURL;

            if (imgURL=="")
            {
                cur_img_url = "";
            }
            else
            {
                cur_img_url = ImgHelper.GetCofigShowUrl() + imgURL.Substring(0, imgURL.LastIndexOf("/") + 1) + typeName + imgURL.Substring(imgURL.LastIndexOf("/") + 1);
            }

            return cur_img_url;
        }

        #endregion

    }
}
