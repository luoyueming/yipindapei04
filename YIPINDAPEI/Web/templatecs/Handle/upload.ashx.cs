using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NewXzc.Web.Common;

namespace NewXzc.Web.templatecs.Handle
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                HttpPostedFile file = context.Request.Files["Filedata"];

                if (file != null)
                {
                    //判断图片长宽是否符合标准

                    System.Drawing.Image fristImage = System.Drawing.Image.FromStream(file.InputStream);
                    if (fristImage.Width < 224 || fristImage.Height < 224)
                    {
                        context.Response.Write("sizeError");
                    }
                    else
                    {
                        //上传图片的扩展名
                        string fileExtension = Path.GetExtension(file.FileName);//上传文件的后缀
                        //判断文件格式
                        if (!CheckValidExt(fileExtension))
                        {
                            context.Response.Write("错误提示：文件格式不正确！" + fileExtension);
                            return;
                        }
                        //根目录文件名称
                        string folder_name = context.Request["folder_name"].ToString();

                        //使用时间+随机数重命名文件
                        string strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff");//取得时间字符串
                        Random ran = new Random();
                        string strRan = Convert.ToString(ran.Next(100, 999));//生成三位随机数
                        string saveName = strDateTime + strRan + fileExtension;

                        string UploadDir = UploadFileHelper.GetUploadImgPre(HttpContext.Current.Request.Url.ToString());
                        string path = DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd");

                        string imgPath = UploadDir + folder_name + "/" + path;

                        imgPath = imgPath.Replace("\\", "/");

                        //判断是否有该文件夹，没有就创建
                        if (!Directory.Exists(imgPath))
                        {
                            Directory.CreateDirectory(imgPath);
                        }

                        file.SaveAs(imgPath + "/" + saveName);

                        System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(imgPath + "/" + saveName);
                        int intHeight = imgPhoto.Height;//图片的高
                        int intWidth = imgPhoto.Width;//图片的宽度 

                        string showimg = UploadFileHelper.GetCofigShowUrl();
                        //下面这句代码缺少的话，上传成功后上队列的显示不会自动消失
                        context.Response.Write("{path:'" + showimg + folder_name + "/" + path + "/" + saveName + "',jdpath:'" + folder_name + "/" + path + "/" + saveName + "',height:'" + intHeight + "',width:'" + intWidth + "'}");
                    }
                }
                else
                {
                    context.Response.Write("0");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
        }

        #region 检测扩展名的有效性 +bool CheckValidExt(string sExt)
        /// <summary>
        /// 检测扩展名的有效性 +bool CheckValidExt(string sExt)
        /// </summary>
        /// <param name="sExt">文件名扩展名</param>
        /// <returns>如果扩展名有效,返回true,否则返回false.</returns>
        public bool CheckValidExt(string strExt)
        {
            string AllowExt = "7z|aiff|asf|avi|bmp|csv|doc|docx|fla|flv|gif|gz|gzip|jpeg|jpg|mid|mov|mp3|mp4|mpc|mpeg|mpg|ods|odt|pdf|png|ppt|pptx|pxd|qt|ram|rar|rm|rmi|rmvb|rtf|sdc|sitd|swf|sxc|sxw|tar|tgz|tif|tiff|txt|vsd|wav|wma|wmv|xls|xlsx|xml|zip";//支持的文件格式 
            bool flag = false;
            string[] arrExt = AllowExt.Split('|');
            foreach (string filetype in arrExt)
            {
                if (filetype.ToLower() == strExt.ToLower().Replace(".", ""))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}