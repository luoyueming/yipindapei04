using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;
using NewXzc.Web.Common.uhelper;
using NewXzc.Common;
using NewXzc.DBUtility;
using System.Data;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace NewXzc.Web.templatecs.Handle
{
    /// <summary>
    /// Create_html_handle 的摘要说明
    /// </summary>
    public class Create_html_handle : IHttpHandler
    {

        string save_folder = "";

        BLL.RED_RECOMMEND recommend_bll = new BLL.RED_RECOMMEND();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            string action = String_Manage.Return_Request_Str("action");

            save_folder = ConfigHelper.GetConfigString("html_folder_name");

            switch (action)
            {
                case "get_pagelist"://获取分页列表
                    get_pagelist(context);
                    break;
            }
        }

        #region  具体操作

        #region  生成文章详情静态页
        private void create_html(HttpContext context)
        {
            string result = "ok";
            int top = String_Manage.Return_Request_Int("top", 0);

            if (top <= 0)
            {
                result = "notop";
            }
            else
            {
                try
                {
                    string filename = "";
                    string filepath = "";
                    Encoding code = Encoding.GetEncoding("utf-8");

                    string save_filepath = "";

                    string fpath = System.Web.HttpContext.Current.Server.MapPath("~" + save_folder);

                    //本地
                    string fpath_wap = fpath.Replace("HONGRENHUI", "HONGRENHUI_WAP");
                    //外网
                    if (fpath_wap.IndexOf("wap") < 0)
                    {
                        fpath_wap = fpath_wap.Replace("hrenh", "hrenh-wap");
                    }

                    //如果不存在就创建file文件夹，PC端
                    if (!Directory.Exists(fpath))
                    {
                        Directory.CreateDirectory(fpath);
                    }


                    //如果不存在就创建file文件夹，WAP端
                    if (!Directory.Exists(fpath_wap))
                    {
                        Directory.CreateDirectory(fpath_wap);
                    }

                    //文章标题（关键词）
                    string atitle = "";
                    //文章Keywords（关键词）
                    string atitle_keywords = "";
                    //文章封面图
                    string aimgurl = "";
                    //文章详情
                    string acontent = "";

                    string sql_title = "select top " + top + " id,TITLE from CREATE_HTML_ARTICLE_TITLE where isuse=0";
                    //sql_title = "select top " + top + " id,TITLE from CREATE_HTML_ARTICLE_TITLE where isuse=0 order by newid()";
                    DataSet ds_title = DbHelperSQL.Query(sql_title);

                    if (ds_title != null && ds_title.Tables[0].Rows.Count > 0)
                    {
                        if (top > ds_title.Tables[0].Rows.Count)
                        {
                            result = "less";
                        }
                        else
                        {
                            foreach (DataRow drtitle in ds_title.Tables[0].Rows)
                            {
                                string ttid = drtitle["id"].ToString();
                                atitle = drtitle["title"].ToString();


                                #region  给文章标题随机再添加一个标题
                                //try
                                //{
                                //    string atitle_two = DbHelperSQL.GetSingle("select top 1 TITLE from CREATE_HTML_ARTICLE_TITLE where isuse=0 and id not in(" + ttid + ") order by newid()").ToString();

                                //    if (atitle_two != "")
                                //    {
                                //        atitle = atitle + "|" + atitle_two;
                                //    }
                                //}
                                //catch (Exception ex)
                                //{

                                //}

                                //给文章keywords重新赋值
                                atitle_keywords = atitle.Replace("|", ",");
                                #endregion


                                DateTime dt = DateTime.Now;

                                // 读取PC端模板文件   
                                string temp = HttpContext.Current.Server.MapPath("~/HTML_MODEL/detail.htm");
                                StreamReader sr = null;
                                StreamWriter sw = null;
                                string str = "";
                                try
                                {
                                    sr = new StreamReader(temp, code);
                                    str = sr.ReadToEnd(); // 读取文件   
                                }
                                catch (Exception exp)
                                {
                                    HttpContext.Current.Response.Write(exp.Message);
                                    HttpContext.Current.Response.End();
                                    sr.Close();
                                }


                                // 读取WAP端模板文件   
                                string temp_wap = HttpContext.Current.Server.MapPath("~/HTML_MODEL/wap/detail.htm");
                                StreamReader sr_wap = null;
                                StreamWriter sw_wap = null;
                                string str_wap = "";
                                try
                                {
                                    sr_wap = new StreamReader(temp_wap, code);
                                    str_wap = sr_wap.ReadToEnd(); // 读取文件   
                                }
                                catch (Exception exp)
                                {
                                    HttpContext.Current.Response.Write(exp.Message);
                                    HttpContext.Current.Response.End();
                                    sr_wap.Close();
                                }

                                filename = dt.ToString("yyyyMMddHHmmss") + dt.Millisecond + ".htm";

                                filepath = fpath + filename;

                                #region  获取文章封面图
                                aimgurl = DbHelperSQL.GetSingle("select top 1 ARTICLE_IMGURL from CREATE_HTML_ARTICLE_IMG order by newid()").ToString();
                                #endregion

                                #region  获取文章段落

                                string content_first = "";
                                string show_content_first = "";

                                acontent = "";

                                string sql_content = "select top 10 ARTICLE_CONTENT from CREATE_HTML_ARTICLE_CONTENT order by newid()";
                                DataSet ds_content = DbHelperSQL.Query(sql_content);

                                if (ds_content != null && ds_content.Tables[0].Rows.Count > 0)
                                {
                                    int dc = 0;

                                    Random imgrnd = new Random();
                                    int img_index = imgrnd.Next(3, 5);

                                    foreach (DataRow drcontent in ds_content.Tables[0].Rows)
                                    {
                                        #region  获取图片内容中的配图
                                        Random rnd = new Random();
                                        int r = rnd.Next(1, 10);


                                        #endregion

                                        if (dc == 0)
                                        {
                                            content_first = drcontent["ARTICLE_CONTENT"].ToString();
                                            show_content_first = HttpContext.Current.Server.HtmlEncode(NewXzc.Common.Input.GetSubString(content_first, 90));
                                        }
                                        dc++;


                                        #region  在文章段落中随机添加2个关键词
                                        string random2_title = "";

                                        try
                                        {
                                            string sql_title_random2 = "select top 2 TITLE from CREATE_HTML_ARTICLE_TITLE order by newid()";
                                            DataSet ds_title_random2 = DbHelperSQL.Query(sql_title_random2);

                                            if (ds_title_random2 != null && ds_title_random2.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow randoms in ds_title_random2.Tables[0].Rows)
                                                {
                                                    random2_title += "，" + randoms["TITLE"].ToString();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        #endregion

                                        #region  在文章段落中随便添加图片

                                        string random_img = "";

                                        if (dc % img_index == 0)
                                        {
                                            try
                                            {
                                                string sql_img_random = "select top 1 ARTICLE_IMGURL from CREATE_HTML_ARTICLE_CONTENT_IMG order by newid()";
                                                random_img = "<p style='text-align:center;'><img src='" + DbHelperSQL.GetSingle(sql_img_random).ToString() + "' alt='" + atitle + "'></p>";
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }

                                        #endregion

                                        acontent += "<p>" + drcontent["ARTICLE_CONTENT"].ToString() + random2_title + "</p>";
                                        acontent += random_img;
                                    }
                                }

                                #endregion

                                // 替换PC模板内容   
                                // 这时,模板文件已经读入到名称为str的变量中了   

                                //模板页中的title
                                str = str.Replace("@title", atitle + "_女装搭配|红人汇");
                                //模板页中的keywords   
                                str = str.Replace("@keywords", atitle_keywords + ",女装搭配");
                                //模板页中的description 
                                str = str.Replace("@description", show_content_first);
                                //模板页中的文章标题
                                str = str.Replace("@articletitle", atitle);
                                str = str.Replace("@edittime", TimeParser.ReturnCurTime(dt.ToString(), 1));
                                //文章详情
                                str = str.Replace("@articlecontent", acontent);




                                //最新资讯第一篇文章ID
                                int article_id = 0;
                                try
                                {
                                    article_id = Convert.ToInt32(DbHelperSQL.GetSingle("select top 1 id from hrenh_article a where types=354 and isend=0 and pub_state=0").ToString());
                                }
                                catch (Exception ex)
                                {

                                }


                                //获取更多文章
                                str = str.Replace("@list_more", GetList_More(article_id));

                                //获取今日热点
                                str = str.Replace("@jrrd", Get_Jrrd());

                                //获取右侧轮播图
                                str = str.Replace("@mrjx_ad", Get_Tj_Img(0, 2100, 1010));

                                //获取大家都在搜
                                str = str.Replace("@djdzs", Get_Tj_Img(0, 2100, 1004));

                                string xgyd = GetList_fuzhuang(article_id);

                                string[] xiangguanyuedu = xgyd.Split('@');

                                string xgyd_left = "";
                                string xgyd_right = "";

                                for (int x = 0; x < xiangguanyuedu.Length; x++)
                                {
                                    if (x == 0)
                                    {
                                        xgyd_left = xiangguanyuedu[x];
                                    }
                                    else
                                    {
                                        xgyd_right = xiangguanyuedu[x];
                                    }
                                }

                                //获取相关阅读，左侧
                                str = str.Replace("@list_fuzhuang_left", xgyd_left);

                                //获取相关阅读，右侧
                                str = str.Replace("@list_fuzhuang_right", xgyd_right);


                                // 替换WAP端模板内容   
                                // 这时,模板文件已经读入到名称为str_wap的变量中了   

                                //模板页中的title
                                str_wap = str_wap.Replace("@title", atitle + "_女装搭配|红人汇");
                                //模板页中的keywords   
                                str_wap = str_wap.Replace("@keywords", atitle_keywords + ",女装搭配");
                                //模板页中的description 
                                str_wap = str_wap.Replace("@description", show_content_first);
                                //模板页中的文章标题
                                str_wap = str_wap.Replace("@articletitle", atitle);
                                //文章详情
                                str_wap = str_wap.Replace("@articlecontent", acontent);


                                // 写文件，PC端 
                                try
                                {
                                    sw = new StreamWriter(filepath, false, code);
                                    sw.Write(str);
                                    sw.Flush();
                                }
                                catch (Exception ex)
                                {
                                    HttpContext.Current.Response.Write(ex.Message);
                                    HttpContext.Current.Response.End();
                                }
                                finally
                                {
                                    sw.Close();
                                }



                                // 写文件，WAP端   
                                try
                                {
                                    //本地
                                    string filepath_wap = filepath.Replace("HONGRENHUI", "HONGRENHUI_WAP");
                                    //外网
                                    if (filepath_wap.IndexOf("wap") < 0)
                                    {
                                        filepath_wap = filepath_wap.Replace("hrenh", "hrenh-wap");
                                    }

                                    sw_wap = new StreamWriter(filepath_wap, false, code);
                                    sw_wap.Write(str_wap);
                                    sw_wap.Flush();
                                }
                                catch (Exception ex)
                                {
                                    HttpContext.Current.Response.Write(ex.Message);
                                    HttpContext.Current.Response.End();
                                }
                                finally
                                {
                                    sw_wap.Close();
                                }


                                //将已经生成的文章标题（关键词）状态改为已使用（即isuse=1）
                                try
                                {
                                    DbHelperSQL.ExecuteSql("update CREATE_HTML_ARTICLE_TITLE set isuse=1 where id=" + ttid);
                                }
                                catch (Exception ex)
                                {

                                }

                                #region  将生成之后的静态页地址信息添加到数据中

                                try
                                {
                                    save_filepath = save_folder + filename;

                                    string sql_html = "INSERT INTO CREATE_HTML_ARTICLE_DETAIL_HTML(FILEURLS,ARTICLE_TITLE,ARTICLE_IMGURL,ADDTIME,ARTICLE_CONTENTS,ARTICLE_Tags) VALUES(@FILEURLS,@ARTICLE_TITLE,@ARTICLE_IMGURL,@addtime,@ARTICLE_CONTENTS,@ARTICLE_Tags)";

                                    SqlParameter[] para_content_img = { 
                                              new SqlParameter("@FILEURLS",SqlDbType.NVarChar,100),
                                              new SqlParameter("@ARTICLE_TITLE",SqlDbType.NVarChar,200),
                                              new SqlParameter("@ARTICLE_IMGURL",SqlDbType.NVarChar,200),
                                              new SqlParameter("@addtime",SqlDbType.DateTime),
                                              new SqlParameter("@ARTICLE_CONTENTS",SqlDbType.NVarChar,500),
                                              new SqlParameter("@ARTICLE_Tags",SqlDbType.NVarChar,50)
                                              };
                                    para_content_img[0].Value = save_filepath;
                                    para_content_img[1].Value = atitle;
                                    para_content_img[2].Value = aimgurl;
                                    para_content_img[3].Value = DateTime.Now;
                                    para_content_img[4].Value = content_first;
                                    para_content_img[5].Value = "";

                                    DbHelperSQL.ExecuteSql(sql_html, para_content_img);
                                }
                                catch (Exception ex)
                                {

                                }

                                #endregion
                            }
                        }
                    }
                    else
                    {
                        result = "less";
                    }

                }
                catch (System.Exception ex)
                {
                    //result = ex.Message;
                    result = "no";
                }
            }

            context.Response.Write(result);
        }


        #region  获取详情页及列表页相关的文章信息


        /// <summary>
        /// 获取指南栏目下最新的10条记录
        /// </summary>
        /// <param name="context"></param>
        private string GetList_More(int articleid)
        {
            StringBuilder listsbr = new StringBuilder();

            int pagesize = 10;

            int zhinan_type = 354;

            string where = " (types=" + zhinan_type + " or types_pid=" + zhinan_type + ") and isend=0 and pub_state=0 ";

            where += " and id<>" + articleid;

            string sql = "select top " + pagesize + " id,title,isimg,img_url,contents,edittime,(isnull(read_cnt,0)+FALSH_READ_CNT) as read_cnt from hrenh_article where " + where + "order by istop desc,edittime desc";

            DataSet ds = DbHelperSQL.Query(sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    #region  文章列表的信息
                    int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                    string show_article_title = StringHelper.ReturnNumStr(article_title, 1, 34);
                    int isimg = String_Manage.Return_Int(dr["isimg"].ToString(), 0);
                    string article_img = "/template/img/nocontent.png";

                    if (isimg == 1)
                    {
                        article_img = ImgHelper.Get_UploadImgUrl(dr["img_url"].ToString(), 1);
                    }

                    string article_contents = StringHelper.ReturnNumStr(dr["contents"].ToString(), 1, 102);
                    string article_contents_noimg = StringHelper.ReturnNumStr(dr["contents"].ToString(), 1, 170);
                    string article_addtime = TimeParser.ReturnCurTime(dr["edittime"].ToString(), 1);

                    string article_url = "href=\"/article_detail_" + article_id + "\" target=\"_blank\" title=\"" + article_title + "\"";

                    article_url = "href=\"/zhinan/detail_" + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                    int read_cnt = String_Manage.Return_Int(dr["read_cnt"].ToString(), 0);

                    #endregion

                    #region  加载文章列表

                    show_article_title = StringHelper.ReturnNumStr(article_title, 1, 18);
                    listsbr.AppendFormat("<dl class=\"claint-main-list\">");
                    listsbr.AppendFormat("<dt><a {0}><img src=\"{1}\" alt=\"{2}\"></a></dt>", article_url, article_img, article_title);
                    listsbr.AppendFormat("<dd>");
                    listsbr.AppendFormat("<h3><a {0}>{1}</a></h3>", article_url, show_article_title);
                    listsbr.AppendFormat("<p><a {0}>{1}</a></p>", article_url, article_contents);
                    //listsbr.AppendFormat("<span><i class=\"mar0\">网络红人</i><i>国家运动员</i><i>网络红人</i></span>");
                    listsbr.AppendFormat("<span style=\"border:0;margin-left:0px;color:#999;font-size:12px;\" >{0}<i  style=\"position:absolute; right: -20px;top: -3px;color:#999;border:none;\">{1}人阅</i></span>", article_addtime, read_cnt);
                    //listsbr.AppendFormat("<i  style=\"position:absolute; right: -20px;top: 13px;\">{0}人阅</i>", read_cnt);
                    listsbr.AppendFormat("</dd>");
                    listsbr.AppendFormat("</dl>");


                    #endregion
                }
            }
            else
            {
                //listsbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
            }

            return listsbr.ToString();
        }


        /// <summary>
        /// 获取上衣、裤装、裙装、女鞋、包包、配饰、美妆等栏目下的最新的16条记录
        /// </summary>
        /// <param name="context"></param>
        private string GetList_fuzhuang(int articleid)
        {
            StringBuilder sbr = new StringBuilder();

            StringBuilder listsbr = new StringBuilder();

            int pagesize = 16;

            string where = " types between 49 and 55 and isend=0 and pub_state=0 ";

            where += " and id<>" + articleid;

            string sql = "select top " + pagesize + " id,title,types from hrenh_article where " + where + "order by istop desc,edittime desc";

            DataSet ds = DbHelperSQL.Query(sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    #region  文章列表的信息
                    int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                    string show_article_title = StringHelper.ReturnNumStr(article_title, 1, 17);

                    int fuzhuang_type = Convert.ToInt32(dr["types"].ToString());

                    string fuzhuang_type_parname = "";

                    switch (fuzhuang_type)
                    {
                        case 49:
                            fuzhuang_type_parname = "shangyisy";
                            break;
                        case 50:
                            fuzhuang_type_parname = "kuzhuangkz";
                            break;
                        case 51:
                            fuzhuang_type_parname = "qunzhuangqz";
                            break;
                        case 52:
                            fuzhuang_type_parname = "nvxienx";
                            break;
                        case 53:
                            fuzhuang_type_parname = "baobaobb";
                            break;
                        case 54:
                            fuzhuang_type_parname = "peiships";
                            break;
                        case 55:
                            fuzhuang_type_parname = "meizhuangmz";
                            break;
                    }

                    string article_url = "href=\"/" + fuzhuang_type_parname + "/show_" + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                    #endregion

                    #region  加载文章列表

                    if (i % 2 == 0)
                    {
                        sbr.AppendFormat("<li><a {0}>{1}</a></li>", article_url, show_article_title);
                    }
                    else
                    {
                        listsbr.AppendFormat("<li><a {0}>{1}</a></li>", article_url, show_article_title);
                    }
                    #endregion
                }
            }
            else
            {
                //listsbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
            }

            string result = sbr.ToString() + "@" + listsbr.ToString();

            return result;
        }


        /// <summary>
        /// 今日热点
        /// </summary>
        /// <param name="context"></param>
        private string Get_Jrrd()
        {
            StringBuilder sbr = new StringBuilder();
            DataSet ds = null;
            int pid = 2100;

            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(24, " pageid=" + pid + " and cid=1012 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int total = ds.Tables[0].Rows.Count;

                int cols = 6;

                int rows = 0;

                if (total % cols == 0)
                {
                    rows = total / cols;
                }
                else
                {
                    rows = total / cols + 1;
                }

                for (int i = 0; i < rows; i++)
                {
                    sbr.AppendFormat("<div class=\"today_div\">");

                    for (int j = i * cols; j < (i + 1) * cols; j++)
                    {
                        if (j < total)
                        {
                            DataRow dr = ds.Tables[0].Rows[j];

                            string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                            string show_title = StringHelper.ReturnNumStr(title, 1, 17);
                            string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                            //链接打开方式，1：在本页面打，2：在新页面打开
                            int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                            sbr.AppendFormat("<div class=\"fcut\">");

                            sbr.AppendFormat("<div class=\"mask\">");

                            sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                            sbr.AppendFormat("{0}</a>", show_title);

                            sbr.AppendFormat("</div>");

                            sbr.AppendFormat("</div>");
                        }
                        else
                        {
                            break;
                        }

                    }

                    sbr.AppendFormat("</div>");
                }
            }

            return sbr.ToString();
        }


        /// <summary>
        /// 获取指定数目的图片，可为0
        /// </summary>
        /// <param name="top">具体的数目，可为0</param>
        /// <param name="pageid">页面ID</param>
        /// <param name="cid">栏目ID</param>
        /// <returns></returns>
        public string Get_Tj_Img(int top, int pageid, int cid)
        {
            StringBuilder sbr = new StringBuilder();


            DataSet ds = recommend_bll.GetList_NewIndex(top, " pageid=" + pageid + " and cid=" + cid + " ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 0, 18);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"300\" width=\"640\"";

                    if (cid == 1002 || cid == 1003)
                    {
                        sbr.AppendFormat("<div class=\"item\"><div class=\"g-pic\">");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            httpurl = " href='javascript:;' ";
                        }
                        else
                        {
                            httpurl = Return_HttpURL.Return_Url(httpurl, openstyle);
                        }
                        sbr.AppendFormat("<a title=\"{0}\" class=\"white\" {1}><img width=\"320\" height=\"250\" border=\"0\" alt=\"{0}\" src=\"{2}\"><em class=\"fcut\"><span>{3}</span></em></a>", title, httpurl, imgurl, show_title);
                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("</div>");
                    }
                    else if (cid == 1004)
                    {
                        string curhref = " href='javascript:;' ";

                        if (httpurl != "")
                        {
                            curhref = " href='" + httpurl + "' target='_blank' ";
                        }

                        string red_title = title;

                        if (title == "羽绒服" || title == "打底裤" || title == "短靴" || title == "呢子大衣" || title == "雪地靴" || title == "卫衣" || title == "风衣" || title == "毛衣" || title == "针织衫" || title == "毛呢裙" || title == "韩范")
                        {
                            red_title = "<font style='color:red;'>" + title + "</font>";
                        }

                        sbr.AppendFormat("<a {1} title=\"{0}\">{2}</a><span style=\"margin-left:10px;margin-right:10px;\">/</span>", title, curhref, red_title);
                    }
                    else if (cid == 1008)
                    {
                        if (openstyle != 1 && openstyle != 2)
                        {
                            httpurl = " href='javascript:;' ";
                        }
                        else
                        {
                            httpurl = Return_HttpURL.Return_Url(httpurl, openstyle);
                        }
                        sbr.AppendFormat("<a {0} title=\"{1}\"><img src=\"{2}\" alt=\"{1}\"></a>", httpurl, title, imgurl);
                    }
                    else if (cid == 1009)
                    {
                        sbr.AppendFormat("<div class=\"item\"><div class=\"g-pic\">");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            httpurl = " href='javascript:;' ";
                        }
                        else
                        {
                            httpurl = Return_HttpURL.Return_Url(httpurl, openstyle);
                        }
                        //sbr.AppendFormat("<a title=\"{0}\" class=\"white\" href=\"{1}\"><img width=\"280\" height=\"429\" border=\"0\" alt=\"{0}\" src=\"{2}\"><em class=\"fcut\"><span>{3}</span></em></a>", title, httpurl, imgurl, show_title);
                        sbr.AppendFormat("<a title=\"{0}\" class=\"white\" {1}><img width=\"280\" height=\"429\" border=\"0\" alt=\"{0}\" src=\"{2}\"></a>", title, httpurl, imgurl, show_title);
                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("</div>");
                    }
                    else if (cid == 1005)//详情页热门推荐轮播图
                    {
                        show_title = StringHelper.ReturnNumStr(title, 1, 15);

                        sbr.AppendFormat("<div class=\"item\"><div class=\"g-pic\">");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            httpurl = " href='javascript:;' ";
                        }
                        else
                        {
                            httpurl = Return_HttpURL.Return_Url(httpurl, openstyle);
                        }
                        sbr.AppendFormat("<a {1} title=\"{0}\" class=\"white\"><img border=\"0\" alt=\"{0}\" src=\"{2}\"><em class=\"fcut\"><span>{3}</span></em></a>", title, httpurl, imgurl, show_title);
                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("</div>");
                    }
                    else if (cid == 1006)
                    {
                        show_title = StringHelper.ReturnNumStr(title, 1, 22);

                        string cname_class = "";

                        if (i == 0)
                        {
                            cname_class = "mar10";
                        }

                        sbr.AppendFormat("<dl class=\"ltjh-list {0}\">", cname_class);

                        if (openstyle != 1 && openstyle != 2)
                        {
                            httpurl = " href='javascript:;' ";
                        }
                        else
                        {
                            httpurl = Return_HttpURL.Return_Url(httpurl, openstyle);
                        }
                        sbr.AppendFormat("<dt><a {0} title=\"{1}\"><img src=\"{2}\" alt=\"{1}\"></a></dt>", httpurl, title, imgurl);
                        sbr.AppendFormat("<dd><a {0} title=\"{1}\">{2}</a></dd>", httpurl, title, show_title);
                        sbr.AppendFormat("</dl>");
                    }
                    else if (cid == 1007)
                    {
                        show_title = StringHelper.ReturnNumStr(title, 1, 13);
                        desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 1, 13);

                        if (openstyle != 1 && openstyle != 2)
                        {
                            httpurl = " href='javascript:;' ";
                        }
                        else
                        {
                            httpurl = Return_HttpURL.Return_Url(httpurl, openstyle);
                        }

                        sbr.AppendFormat("<div class=\"item\"><div class=\"g-pic\"><a {0} title=\"{1}\"><img width=\"300\" height=\"390\" border=\"0\" alt=\"{1}\" src=\"{2}\"></a></div>", httpurl, title, imgurl);
                        sbr.AppendFormat("<p>");
                        sbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", httpurl, title, show_title);
                        sbr.AppendFormat("<span>{0}</span>", desc);
                        sbr.AppendFormat("</p>");
                        sbr.AppendFormat("<p class=\"everyday-pickC\">");

                        sbr.AppendFormat("</p>");

                        sbr.AppendFormat("<span style=\"position:absolute;left:15px;top:343px;color:#fff;font-size:20px;\"><i style=\"color:#c0292a\"></i>/<b style=\"color:#ffffff\"></b></span>");

                        sbr.AppendFormat("</div>");

                    }
                    else if (cid == 1010)
                    {
                        show_title = StringHelper.ReturnNumStr(title, 1, 23);
                        desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 1, 13);

                        if (openstyle != 1 && openstyle != 2)
                        {
                            httpurl = " href='javascript:;' ";
                        }
                        else
                        {
                            httpurl = Return_HttpURL.Return_Url(httpurl, openstyle);
                        }

                        sbr.AppendFormat("<div class=\"item\"><div class=\"g-pic\"><a {0} title=\"{1}\"><img width=\"300\" height=\"390\" border=\"0\" alt=\"{1}\" src=\"{2}\"></a></div>", httpurl, title, imgurl);
                        sbr.AppendFormat("<p>");
                        sbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", httpurl, title, show_title);
                        //sbr.AppendFormat("<span>{0}</span>", desc);
                        sbr.AppendFormat("</p>");
                        sbr.AppendFormat("<p class=\"everyday-pickC\">");

                        sbr.AppendFormat("</p>");

                        sbr.AppendFormat("<span style=\"position:absolute;left:15px;top:343px;color:#fff;font-size:20px;\"><i style=\"color:#c0292a\"></i>/<b style=\"color:#ffffff\"></b></span>");

                        sbr.AppendFormat("</div>");

                    }
                }
            }

            return sbr.ToString();
        }


        #endregion



        #endregion

        #region  生成文章列表
        private void create_list(HttpContext context)
        {
            string result = "ok";
            int pagesize = 10;
            int pagecnt = 0;
            int top = 1000;
            int total = 0;


            string fpath = System.Web.HttpContext.Current.Server.MapPath("~" + save_folder);


            //本地
            string fpath_wap = fpath.Replace("HONGRENHUI", "HONGRENHUI_WAP");
            //外网
            if (fpath_wap.IndexOf("wap") < 0)
            {
                fpath_wap = fpath_wap.Replace("hrenh", "hrenh-wap");
            }

            //如果不存在就创建file文件夹，PC端
            if (!Directory.Exists(fpath))
            {
                Directory.CreateDirectory(fpath);
            }


            //如果不存在就创建file文件夹，WAP端
            if (!Directory.Exists(fpath_wap))
            {
                Directory.CreateDirectory(fpath_wap);
            }


            List<string> list = new List<string>();
            List<string> list_wap = new List<string>();

            try
            {
                string sql = "select top " + top + " article_title,article_imgurl,article_contents,fileurls,addtime,ARTICLE_Tags from CREATE_HTML_ARTICLE_DETAIL_HTML where datediff(day,'" + DateTime.Now + "',addtime)=0";
                DataSet ds = DbHelperSQL.Query(sql);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    total = ds.Tables[0].Rows.Count;

                    if (total % pagesize == 0)
                    {
                        pagecnt = total / pagesize;
                    }
                    else
                    {
                        pagecnt = total / pagesize + 1;
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string atitle = dr["article_title"].ToString();
                        string show_article_title = StringHelper.ReturnNumStr(atitle, 0, 36);
                        string imgurls = dr["article_imgurl"].ToString();
                        string mcontent = dr["article_contents"].ToString();
                        string article_url = "http://www.hrenh.com" + dr["fileurls"].ToString();
                        string article_url_wap = "http://m.hrenh.com" + dr["fileurls"].ToString();
                        string pubtime = TimeParser.ReturnCurTime(dr["addtime"].ToString(), 1);
                        string taglist = dr["ARTICLE_Tags"].ToString();

                        StringBuilder sbr = new StringBuilder();
                        StringBuilder sbr_wap = new StringBuilder();

                        #region  加载PC端文章列表

                        article_url = "href='" + article_url + "' target='_blank' title='" + atitle + "'";

                        sbr.AppendFormat("<li>");
                        sbr.AppendFormat("<dl>");
                        sbr.AppendFormat("<dt><a {0}><img src=\"{1}\" alt=\"{2}\"></a></dt>", article_url, imgurls, atitle);
                        sbr.AppendFormat("<dd style=\"position:absolute;left:0;top:20px;left:336px;max-height:69px;\">");

                        string static_class = "";

                        sbr.AppendFormat("<h4 {2} style=\"position:static;\"><a {0}>{1}</a></h4>", article_url, show_article_title, static_class);
                        sbr.AppendFormat("<p class=\"list-content\" {2}  style=\"position:static;\"><a {0}> {1}</a></p>", article_url, mcontent, static_class);

                        sbr.AppendFormat("<p style=\"position:relative;background:url('/template/img/clock.png') no-repeat left center;\">");
                        sbr.AppendFormat("<span style=\"border:0;margin-left:0px;color:#999;padding-left:20px;\" >{0}</span>", pubtime);
                        sbr.AppendFormat("<i  style=\"position:absolute; right: -20px;top: 13px;\">{0}人阅</i>", 0);
                        sbr.AppendFormat("</p>");
                        sbr.AppendFormat("</dd>");
                        sbr.AppendFormat("</dl>");
                        sbr.AppendFormat("</li>");

                        #endregion


                        #region  加载WAP端文章列表

                        article_url_wap = "href='" + article_url_wap + "' title='" + atitle + "' ";
                        show_article_title = StringHelper.ReturnNumStr(atitle, 1, 20);

                        string article_contents = StringHelper.ReturnNumStr(mcontent, 1, 25);

                        sbr_wap.AppendFormat("<div class=\"q_lrList no commondiv\">");
                        sbr_wap.AppendFormat("<dl>");
                        sbr_wap.AppendFormat("<dt>");
                        sbr_wap.AppendFormat("<a {0}>", article_url_wap);
                        sbr_wap.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurls, atitle);
                        sbr_wap.AppendFormat("</a>");
                        sbr_wap.AppendFormat("</dt>");
                        sbr_wap.AppendFormat("<dd>");
                        sbr_wap.AppendFormat("<a {0}>", article_url_wap);
                        sbr_wap.AppendFormat("<strong class=\"tit\">{0}</strong>", show_article_title);
                        sbr_wap.AppendFormat("<div class=\"txt\">{0}</div>", article_contents);
                        sbr_wap.AppendFormat("</a>");
                        sbr_wap.AppendFormat("</dd>");
                        sbr_wap.AppendFormat("</dl>");
                        sbr_wap.AppendFormat("</div>");
                        #endregion


                        list.Add(sbr.ToString());
                        list_wap.Add(sbr_wap.ToString());
                    }

                    for (int i = 0; i < pagecnt; i++)
                    {

                        // 读取模板文件   
                        string temp = HttpContext.Current.Server.MapPath("~/HTML_MODEL/list.htm");
                        StreamReader sr = null;
                        StreamWriter sw = null;
                        string str = "";
                        Encoding code = Encoding.GetEncoding("utf-8");
                        string filename = "";
                        string filepath = "";

                        try
                        {
                            sr = new StreamReader(temp, code);
                            str = sr.ReadToEnd(); // 读取文件   
                        }
                        catch (Exception exp)
                        {
                            HttpContext.Current.Response.Write(exp.Message);
                            HttpContext.Current.Response.End();
                            sr.Close();
                        }


                        // 读取WAP端模板文件   
                        string temp_wap = HttpContext.Current.Server.MapPath("~/HTML_MODEL/wap/list.htm");
                        StreamReader sr_wap = null;
                        StreamWriter sw_wap = null;
                        string str_wap = "";
                        try
                        {
                            sr_wap = new StreamReader(temp_wap, code);
                            str_wap = sr_wap.ReadToEnd(); // 读取文件   
                        }
                        catch (Exception exp)
                        {
                            HttpContext.Current.Response.Write(exp.Message);
                            HttpContext.Current.Response.End();
                            sr_wap.Close();
                        }


                        save_folder = "list";

                        if (i == 0)
                        {
                            filename = save_folder + ".htm";
                        }
                        else
                        {
                            filename = save_folder + "_" + (i + 1).ToString() + ".htm";
                        }

                        filepath = fpath + filename;


                        // 替换内容   
                        // 这时,模板文件已经读入到名称为str的变量中了   

                        //模板页中的title
                        str = str.Replace("@title", "女装搭配|红人汇");
                        //模板页中的keywords   
                        str = str.Replace("@keywords", "女装搭配,红人汇");
                        //模板页中的description 
                        str = str.Replace("@description", "女装搭配，红人汇");

                        string list10 = "";

                        string list10_wap = "";

                        for (int j = i * pagesize; j < (i + 1) * pagesize; j++)
                        {
                            if (j < list.Count)
                            {
                                list10 += list[j].ToString();
                                list10_wap += list_wap[j].ToString();
                            }
                            else
                            {
                                break;
                            }
                        }

                        //模板页中的文章列表
                        str = str.Replace("@list", list10);

                        //string pages = NewXzc.Common.NewArticlePageHtml.pageHtml(total, pagesize,"http://www.hrenh.com/"+save_folder+"/"+save_folder);

                        ////模板页中的分页列表
                        //str = str.Replace("@pages", pages);

                        //获取总共有多少条记录
                        str = str.Replace("@total", total.ToString());

                        //获取当前页页码
                        str = str.Replace("@page", (i + 1).ToString());



                        //最新资讯第一篇文章ID
                        int article_id = 0;
                        try
                        {
                            article_id = Convert.ToInt32(DbHelperSQL.GetSingle("select top 1 id from hrenh_article a where types=354 and isend=0 and pub_state=0").ToString());
                        }
                        catch (Exception ex)
                        {

                        }

                        //获取今日热点
                        str = str.Replace("@jrrd", Get_Jrrd());

                        //获取右侧轮播图
                        str = str.Replace("@mrjx_ad", Get_Tj_Img(0, 2100, 1010));

                        //获取大家都在搜
                        str = str.Replace("@djdzs", Get_Tj_Img(0, 2100, 1004));



                        #region  WAP端

                        // 替换内容   
                        // 这时,模板文件已经读入到名称为str的变量中了   

                        //模板页中的title
                        str_wap = str_wap.Replace("@title", "女装搭配|红人汇");
                        //模板页中的keywords   
                        str_wap = str_wap.Replace("@keywords", "女装搭配,红人汇");
                        //模板页中的description 
                        str_wap = str_wap.Replace("@description", "女装搭配，红人汇");

                        //模板页中的文章列表
                        str_wap = str_wap.Replace("@list", list10_wap);

                        //获取总共有多少条记录
                        str_wap = str_wap.Replace("@total", total.ToString());

                        //获取当前页页码
                        str_wap = str_wap.Replace("@page", (i + 1).ToString());

                        #endregion




                        // 写文件，PC端 
                        try
                        {
                            sw = new StreamWriter(filepath, false, code);
                            sw.Write(str);
                            sw.Flush();
                        }
                        catch (Exception ex)
                        {
                            HttpContext.Current.Response.Write(ex.Message);
                            HttpContext.Current.Response.End();
                        }
                        finally
                        {
                            sw.Close();
                        }



                        // 写文件，WAP端   
                        try
                        {
                            //本地
                            string filepath_wap = filepath.Replace("HONGRENHUI", "HONGRENHUI_WAP");
                            //外网
                            if (filepath_wap.IndexOf("wap") < 0)
                            {
                                filepath_wap = filepath_wap.Replace("hrenh", "hrenh-wap");
                            }

                            sw_wap = new StreamWriter(filepath_wap, false, code);
                            sw_wap.Write(str_wap);
                            sw_wap.Flush();
                        }
                        catch (Exception ex)
                        {
                            HttpContext.Current.Response.Write(ex.Message);
                            HttpContext.Current.Response.End();
                        }
                        finally
                        {
                            sw_wap.Close();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result = "no";
            }

            context.Response.Write(result);
        }


        #endregion

        #region  获取分页列表

        private void get_pagelist(HttpContext context)
        {
            int pagesize = 10;
            int total = String_Manage.Return_Request_Int("total", 0);
            int cpage = String_Manage.Return_Request_Int("page", 1);

            string pages = NewXzc.Common.NewArticlePageHtml.pageHtml_CreateHtml(total, pagesize, cpage);

            context.Response.Write(pages);

        }

        #endregion

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