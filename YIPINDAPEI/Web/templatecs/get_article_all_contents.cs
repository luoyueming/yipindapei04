using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using NewXzc.DBUtility;
using NewXzc.Web.Common;

namespace NewXzc.Web.templatecs
{
    public class get_article_all_contents : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            //int ftype = String_Manage.Return_Str_Int("type", 0);

            //context.Put("f",ftype);

            //if (ftype > 0)
            //{
            //    // 获取指定文件夹下的所有文件
            //    Get_Folder_Files(ftype,context);
            //}

            ////获取所有文章内容和文章封面图
            //Get_All_Contents(context);

            try
            {
                DataSet numds = DbHelperSQL.Query("select (select count(*) from CREATE_HTML_ARTICLE_TITLE) as total,(select count(*) from CREATE_HTML_ARTICLE_TITLE where isuse=0) as leftss");

                if (numds != null && numds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in numds.Tables[0].Rows)
                    {
                        int totals = Convert.ToInt32(dr["total"].ToString());
                        int ltotals = Convert.ToInt32(dr["leftss"].ToString());
                        context.Put("totals", totals);
                        context.Put("leftss", ltotals);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// 获取指定文件夹下的所有文件
        /// </summary>
        /// <param name="ftype"></param>
        private void Get_Folder_Files(int ftype, NVelocity.VelocityContext context)
        {
            try
            {
                string path = String_Manage.Return_Request_Str("furl");


                //context.Put("fl", path);

                DirectoryInfo di = new DirectoryInfo(path);
                //找到该目录下的文件 
                FileInfo[] fis = di.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    //文件名称
                    string filename = fi.Name;
                    //文件物理路径
                    string file_path = fi.FullName;
                    //文件扩展名
                    string file_desc = fi.Extension;

                    if (file_desc == ".txt")
                    {
                        StreamReader sr = new StreamReader(file_path, System.Text.Encoding.Default);
                        String txt_content = sr.ReadToEnd();

                        txt_content = txt_content.Replace("\r\n", "¤");
                        txt_content = txt_content.Replace("\n", "");

                        string[] txtarr = txt_content.Split('¤');

                        // 添加标题（关键词）和内容到数据库
                        Add_Title_Content_Keywords(ftype, txtarr);

                        sr.Close();
                    }

                }

                context.Put("r", "执行完成");
            }
            catch (Exception ex)
            {
                context.Put("r", ex.ToString());
            }


        }

        /// <summary>
        /// 添加标题（关键词）和内容到数据库
        /// </summary>
        /// <param name="type"></param>
        /// <param name="arr"></param>
        private void Add_Title_Content_Keywords(int type, string[] arr)
        {
            string sql = "";

            //标题（关键词）
            if (type == 1)
            {
                #region  将标题（关键词）添加到数据库中
                try
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string title = arr[i];

                        if (title != "")
                        {

                            sql = "select count(*) as cnt from CREATE_HTML_ARTICLE_TITLE where TITLE='" + title + "'";

                            if (DbHelperSQL.GetSingle(sql).ToString() == "0")
                            {
                                sql = "INSERT INTO CREATE_HTML_ARTICLE_TITLE(TITLE,ADDTIME) VALUES(@TITLE,@addtime)";

                                SqlParameter[] para = { 
                                              new SqlParameter("@TITLE",SqlDbType.NVarChar,200),
                                              new SqlParameter("@addtime",SqlDbType.DateTime)
                                              };
                                para[0].Value = title;
                                para[1].Value = DateTime.Now;

                                DbHelperSQL.ExecuteSql(sql, para);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errors = ex.ToString();
                }
                #endregion
            }//内容
            else if (type == 2)
            {
                #region  将文章内容添加到数据库中
                try
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string title = arr[i];

                        if (title != "")
                        {

                            sql = "select count(*) as cnt from CREATE_HTML_ARTICLE_CONTENT where ARTICLE_CONTENT='" + title + "'";

                            if (DbHelperSQL.GetSingle(sql).ToString() == "0")
                            {
                                sql = "INSERT INTO CREATE_HTML_ARTICLE_CONTENT(ARTICLE_CONTENT,ADDTIME) VALUES(@ARTICLE_CONTENT,@addtime)";

                                SqlParameter[] para = { 
                                            new SqlParameter("@ARTICLE_CONTENT",SqlDbType.NVarChar,2000),
                                            new SqlParameter("@addtime",SqlDbType.DateTime)
                                            };
                                para[0].Value = title;
                                para[1].Value = DateTime.Now;

                                DbHelperSQL.ExecuteSql(sql, para);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errors = ex.ToString();
                }
                #endregion
            }
        }


        /// <summary>
        /// 获取所有文章内容和文章封面图
        /// </summary>
        /// <param name="context"></param>
        private void Get_All_Contents(NVelocity.VelocityContext context)
        {
            #region  将文章详情中html标签去除

            try
            {
                //专访、主播、微信（无二级），资讯、搭配指南（有二级）
                DataSet ds = DbHelperSQL.Query("select id as article_id,isnull(IMG_URL,'') as article_imgurl,isnull((select top 1 CONTENTS from reds_parliament.dbo.hrenh_article_html where article_id=a.id),'') as article_content from reds_parliament.dbo.hrenh_article a where (types in(4,45,47) or types_pid in(1,354)) and isend=0 and pub_state=0");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //文章内容
                        List<string> sbr_content = new List<string>();
                        //文章内容下的img标签
                        List<string> sbr_content_img = new List<string>();

                        string id = dr["article_id"].ToString();
                        string article_imgurl = "http://image01.xzhichang.com/" + dr["article_imgurl"].ToString();
                        string all_content = dr["article_content"].ToString();

                        all_content = all_content.Replace("_ueditor_page_break_tag_", "");

                        all_content = all_content.ToLower().Replace("<br>", "¤").Replace("<br/>", "¤");

                        //文章内容中图片列表
                        sbr_content_img = Get_Img_UrlList.MyGetImgUrl_List(all_content);

                        ////去除img标签
                        //all_content = Regex.Replace(all_content, @"<img\b[^>]*>", "");

                        //all_content = Regex.Replace(all_content, "\r\n", "");

                        //all_content = all_content.Replace("&nbsp;", "");

                        //string[] allarr = all_content.Split('¤');

                        //for (int a = 0; a < allarr.Length; a++)
                        //{
                        //    string aacontents = allarr[a];
                        //    Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
                        //    aacontents = regex.Replace(aacontents, "");

                        //    aacontents = aacontents.Trim();

                        //    if (aacontents != "")
                        //    {
                        //        //sbr_content.Add(aacontents);
                        //    }
                        //}

                        //if (id != "")
                        //{
                        //    WriteError(sbr_content, id);
                        //}

                        //// 将文章内容下的图片和文章封面图存储到数据库
                        //Add_Title_Content(sbr_content_img, article_imgurl);
                    }

                    context.Put("r", "执行成功");
                }
            }
            catch (Exception ex)
            {
                context.Put("r", ex.ToString());
            }


            #endregion
        }


        /// <summary>
        /// 将文章内容下的图片和文章封面图存储到数据库
        /// </summary>
        /// <param name="content_img">文章内容下的图片</param>
        /// <param name="article_img">文章封面图</param>
        private void Add_Title_Content(List<string> content_img, string article_img)
        {

            #region  将文章内容下的图片添加到数据库中
            try
            {
                for (int i = 0; i < content_img.Count; i++)
                {
                    string article_img_list = content_img[i];

                    article_img_list = article_img_list.Replace("</p><p>¤</p><p>", "");

                    article_img_list = article_img_list.Replace("<p>", "").Replace("</p>", "").Replace("¤", "");

                    string sql_content_img = "select count(*) as cnt from CREATE_HTML_ARTICLE_CONTENT_IMG where ARTICLE_IMGURL='" + article_img_list + "'";

                    if (DbHelperSQL.GetSingle(sql_content_img).ToString() == "0")
                    {
                        sql_content_img = "INSERT INTO CREATE_HTML_ARTICLE_CONTENT_IMG(ARTICLE_IMGURL,ADDTIME) VALUES(@article_img,@addtime)";

                        SqlParameter[] para_content_img = { 
                                              new SqlParameter("@article_img",SqlDbType.NVarChar,200),
                                              new SqlParameter("@addtime",SqlDbType.DateTime)
                                              };
                        para_content_img[0].Value = article_img_list;
                        para_content_img[1].Value = DateTime.Now;

                        DbHelperSQL.ExecuteSql(sql_content_img, para_content_img);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            #endregion

            #region  将文章图片添加到数据库中
            try
            {

                article_img = article_img.Replace("</p><p>¤</p><p>", "");

                article_img = article_img.Replace("<p>", "").Replace("</p>", "").Replace("¤", "");

                string sql_article_img = "select count(*) as cnt from CREATE_HTML_ARTICLE_IMG where ARTICLE_IMGURL='" + article_img + "'";

                if (DbHelperSQL.GetSingle(sql_article_img).ToString() == "0")
                {
                    sql_article_img = "INSERT INTO CREATE_HTML_ARTICLE_IMG(ARTICLE_IMGURL,ADDTIME) VALUES(@article_img,@addtime)";

                    SqlParameter[] para_article_img = { 
                                              new SqlParameter("@article_img",SqlDbType.NVarChar,200),
                                              new SqlParameter("@addtime",SqlDbType.DateTime)
                                              };
                    para_article_img[0].Value = article_img;
                    para_article_img[1].Value = DateTime.Now;

                    DbHelperSQL.ExecuteSql(sql_article_img, para_article_img);
                }
            }
            catch (Exception ex)
            {

            }
            #endregion

        }


        /// <summary>
        /// 错误日志记录方法
        /// </summary>
        /// <param name="ex">错误信息</param>
        /// <param name="fun">所执行的函数</param>
        /// <param name="pageUrl">执行的页面（尽量填写，无法确定可为空）</param>
        /// <param name="parmeter">执行时的参数</param>
        private void WriteError(List<string> msg, string filename)
        {
            string FilePath = "";
            string FileName = "";
            string directoryPath = "";
            DateTime dt = DateTime.Now;

            string physicsPath = System.Web.HttpContext.Current.Server.MapPath("~/article_content_list/");

            directoryPath = physicsPath + dt.Year + dt.Month;

            if (!Directory.Exists(directoryPath))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(directoryPath);
            }

            FileName = filename + ".txt";
            FilePath = directoryPath + "/" + FileName;

            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }

            StreamReader sr = new StreamReader(FilePath, System.Text.Encoding.UTF8);

            try
            {
                string input = sr.ReadToEnd();
                sr.Close();

                for (int m = 0; m < msg.Count; m++)
                {
                    input = input + msg[m].ToString();
                    input += "\r\n";
                }
                StreamWriter sw = new StreamWriter(FilePath, false, System.Text.Encoding.UTF8);
                sw.Write(input);
                sw.Flush();
                sw.Close();

            }
            catch (Exception exc)
            {

            }
        }




    }
}