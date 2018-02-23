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
    public class get_article_all_contents_upload : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            int ftype = String_Manage.Return_Request_Int("type", 0);

            context.Put("f", ftype);

            if (ftype > 0)
            {
                // 获取指定文件夹下的所有文件
                Get_Folder_Files(ftype, context);
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
                        string title = arr[i].Replace("</p><p>¤</p><p>", "");

                        title = title.Replace("<p>", "").Replace("</p>", "").Replace("¤", "");

                        title = title.Replace("'", "");

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
                        string title = arr[i].Replace("</p><p>¤</p><p>", "");

                        title = title.Replace("<p>", "").Replace("</p>", "").Replace("¤", "");

                        title = title.Replace("'", "");

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