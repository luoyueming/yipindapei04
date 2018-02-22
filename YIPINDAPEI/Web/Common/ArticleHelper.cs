using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NewXzc.DBUtility;
using System.Text;
using NewXzc.Common;
using NewXzc.Web.Common.uhelper;

namespace NewXzc.Web.Common
{
    public class ArticleHelper
    {
        static StringBuilder sbr = new StringBuilder();
        static DataSet ds = null;
        static BLL.RED_RECOMMEND recommend_bll = new BLL.RED_RECOMMEND();
        static int pid = 1100;
            

        /// <summary>
        /// 获取文章类型名称
        /// </summary>
        /// <param name="type">文章类型</param>
        /// <returns></returns>
        public static string Get_Type_Name(int type)
        {
            DataSet type_ds = DbHelperSQL.Query("select typename from hrenh_article_type where id="+type+"");

            string nav_http_name = "";

            if (type_ds != null && type_ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < type_ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = type_ds.Tables[0].Rows[i];

                    nav_http_name = dr["typename"].ToString();
                }
            }

            return nav_http_name;
        }


        /// <summary>
        /// 获取文章类型名称，新资讯（店铺）
        /// </summary>
        /// <param name="type">文章类型</param>
        /// <param name="type_parent">文章总类型，0：默认，1：新资讯（店铺）</param>
        /// <returns></returns>
        public static string Get_Type_Name(int type,int type_parent)
        {
            DataSet type_ds = DbHelperSQL.Query("select (select typename from hrenh_article_type where id="+type+") as typename,(select dbo.return_article_typeidlist("+type+","+type_parent+")) as typenamelist");

            string nav_http_name = "";
            string typenamelist = "";

            if (type_ds != null && type_ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < type_ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = type_ds.Tables[0].Rows[i];

                    nav_http_name = dr["typename"].ToString();
                    typenamelist = dr["typenamelist"].ToString();

                    string[] typearr = typenamelist.Split('|');

                    string type_now_name=typearr[1].ToString();

                    if (type_now_name != "")
                    {
                        nav_http_name = nav_http_name + "," + type_now_name;
                    }

                }
            }

            return nav_http_name;
        }



        /// <summary>
        /// 获取文章标题
        /// </summary>
        /// <param name="id">文章标题</param>
        /// <returns></returns>
        public static string GetArticleTitle(string id)
        {
            string title = "";
            int aid = String_Manage.Return_Int(id, 0);

            try
            {
                title = DbHelperSQL.GetSingle("select top 1 isnull(title,'') as title from hrenh_article where id=" + aid + " and pub_state=0").ToString();
            }
            catch (Exception ex)
            {

            }

            return title;
        }


        /// <summary>
        /// 获取最新资讯，前1条
        /// </summary>
        /// <param name="top">指定需要获取的数目</param>
        /// <param name="where">查询条件</param>
        /// <param name="num">标题字数限制</param>
        /// <returns></returns>
        public static string Get_New_GG(int top, string where = "", int num = 30)
        {
            sbr.Clear();

            try
            {
                string sql = "select top " + top + " id,title,types from hrenh_article where isend=0 and pub_state=0";

                if (where != "")
                {
                    sql += " and " + where;
                }

                sql += " order by addtime desc";

                ds = DbHelperSQL.Query(sql);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];

                        int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                        string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                        string show_article_title = StringHelper.ReturnNumStr(article_title, 1, num);
                        string article_url = "href=\"/article_detail_" + article_id + "\" target=\"_blank\" title=\"" + StringHelper.ReturnNumStr(article_title, 1, 0) + "\"";

                        article_url = "href=\"/" + ArticleHelper.Get_Nav_Type_Name(Convert.ToInt32(dr["types"].ToString())) + "/detail_" + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                        sbr.AppendFormat("<a {0}>{1}</a>", article_url, article_title);

                    }
                }
                else
                {
                    sbr.Append("");
                }
            }
            catch (Exception ex)
            {
                sbr.AppendFormat(ex.ToString());
            }

            return sbr.ToString();
        }

        /// <summary>
        /// 获取最新资讯，前9条
        /// </summary>
        /// <param name="top">指定需要获取的数目</param>
        /// <param name="where">查询条件</param>
        /// <param name="num">标题字数限制</param>
        /// <returns></returns>
        public static string Get_New_ZX(int top,string where="",int num=22)
        {
            sbr.Clear();

            try
            {
                string sql = "select top " + top + " id,title,types from hrenh_article where isend=0 and pub_state=0";

                if (where != "")
                {
                    sql += " and " + where;
                }
                else
                {
                    sql += " and (types in(1,3,4,5) or types_pid in(1,3,4,5)) ";
                }

                sql += " order by istop desc,edittime desc";

                ds = DbHelperSQL.Query(sql);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];

                        int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                        string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                        string show_article_title = StringHelper.ReturnNumStr(article_title, 1, num);
                        string article_url = "href=\"/article_detail_" + article_id + "\" target=\"_blank\" title=\"" + article_title + "\"";

                        article_url = "href=\"/" + ArticleHelper.Get_Nav_Type_Name(Convert.ToInt32(dr["types"].ToString())) + "/detail_" + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                        sbr.AppendFormat("<li>");
                        sbr.AppendFormat("<a {0}>{1}</a>",article_url,show_article_title);
                        sbr.AppendFormat("</li>");

                    }
                }
                else
                {
                    sbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
                }
            }
            catch (Exception ex)
            {
                sbr.AppendFormat(ex.ToString());
            }

            return sbr.ToString();
        }

        /// <summary>
        /// 获取文章列表右侧广告图
        /// </summary>
        /// <returns></returns>
        public static string Get_Article_List_Ad()
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(1, " pageid="+pid+" and cid=1006 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 0, 22);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"250\" width=\"300\"";


                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">",title);
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                    }
                    else
                    {
                        sbr.AppendFormat("<a {1} title=\"{0}\">", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                    }
                }
            }

            return sbr.ToString();
        }

        /// <summary>
        /// 获取文章相关热门图片
        /// </summary>
        /// <returns></returns>
        public static string Get_New_Img()
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(4, " pageid=" + pid + " and cid=1007 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 0, 9);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"90\" width=\"138\"";

                    sbr.AppendFormat("<li>");

                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">", title);
                    }
                    else
                    {
                        sbr.AppendFormat("<a {1} title=\"{0}\">", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                    }
                    sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                    sbr.AppendFormat("<p>{0}</p>", show_title);
                    sbr.AppendFormat("</a>");

                    sbr.AppendFormat("</li>");
                }
            }

            return sbr.ToString();
        }

        /// <summary>
        /// 获取文章相关热门文章
        /// </summary>
        /// <param name="article_id">当前被访问的文章的ID，可为0（代表列表页）</param>
        /// <returns></returns>
        public static string Get_New_Article(int article_id)
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(10, " pageid=" + pid + " and cid=1008 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string title = "";

                    try
                    {
                        title = DbHelperSQL.GetSingle("select top 1 isnull(title,0) as title from hrenh_article where id=" + desc + " and pub_state=0").ToString();
                    }
                    catch (Exception ex)
                    {

                    }

                    title = StringHelper.ReturnNumStr(title, 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 0, 18);


                    sbr.AppendFormat("<li>");

                    sbr.AppendFormat("<i>{0}</i>",(i+1));

                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">{1}</a>", title,show_title);
                    }
                    else
                    {
                        sbr.AppendFormat("<a {1} title=\"{0}\">{2}</a>", title, Return_HttpURL.Return_Url(httpurl, openstyle),show_title);
                    }

                    //int cnt = 0;
                    //try
                    //{
                    //    cnt=Convert.ToInt32(DbHelperSQL.GetSingle("select top 1 isnull(read_cnt,0) as cnt from hrenh_article where id="+desc+"").ToString());
                    //}
                    //catch (Exception ex)
                    //{

                    //}

                    //if (desc == article_id.ToString())
                    //{
                    //    cnt = cnt + 1;
                    //}

                    //sbr.AppendFormat("<span><b>{0}</b>&nbsp;阅读</span>",cnt);
                    
                    sbr.AppendFormat("</li>");
                }
            }

            return sbr.ToString();
        }

        /// <summary>
        /// 获取文章详情右侧广告图
        /// </summary>
        /// <returns></returns>
        public static string Get_Article_Detail_Ad(int cid)
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(1, " pageid=" + pid + " and cid= "+cid);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 0, 22);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"250\" width=\"300\"";

                    if (cid == 1011)
                    {
                        width_height = "height=\"60\" width=\"640\"";
                    }


                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">", title);
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                    }
                    else
                    {
                        sbr.AppendFormat("<a {1} title=\"{0}\">", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                    }
                }
            }

            return sbr.ToString();
        }

        /// <summary>
        /// 获取文章详情底部广告图
        /// </summary>
        /// <returns></returns>
        public static string Get_Article_Detail_Foot_Ad()
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(1, " pageid=" + pid + " and cid=1010 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 0, 22);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"66\" width=\"640\"";


                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">", title);
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                    }
                    else
                    {
                        sbr.AppendFormat("<a {1} title=\"{0}\">", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                    }
                }
            }

            return sbr.ToString();
        }


        /// <summary>
        /// 获取当前类型重写后的名称缩写，1：红人新闻，2：图片写真，3：影视作品，4：红人背后，5：红人观，6：公告
        /// </summary>
        /// <param name="type">当前文章类型，1：红人新闻，2：图片写真，3：影视作品，4：红人背后，5：红人观，6：公告</param>
        /// <param name="get_type">获取当前类型的名称，1：红人新闻，2：图片写真，3：影视作品，4：红人背后，5：红人观，6：公告</param>
        /// <returns></returns>
        public static string Get_Nav_Type_Name(int type,int get_type=0)
        {
            string typename = "hrzx";
            typename = "zixun";

            Model.HRENH_ARTICLE_TYPE type_model = new Model.HRENH_ARTICLE_TYPE();
            BLL.HRENH_ARTICLE_TYPE type_bll = new BLL.HRENH_ARTICLE_TYPE();

            if (type <= 0 || type > 6)
            {
                if (type < 49)
                {
                    type_model = type_bll.GetModel(type);

                    if (type_model != null)
                    {
                        //不是红人风尚
                        if (type != 35 && type != 40 && type != 41 && type != 42 && type != 43 && type != 44 && type != 45 && type != 46 && type != 47)
                        {
                            type = type_model.PID;
                        }
                    }
                }

            }

            if (get_type == 0)
            {
                switch (type)
                {
                    //case 1:
                    //    typename = "hrzx";
                    //    break;
                    //case 2:
                    //    typename = "hrxz";
                    //    break;
                    //case 3:
                    //    typename = "hrzp";
                    //    break;
                    //case 4:
                    //    typename = "hrzf";
                    //    break;
                    //case 5:
                    //    typename = "hrzt";
                    //    break;
                    //case 6:
                    //    typename = "gg";
                    //    break;
                    //case 35:
                    //    typename = "hrfs";
                    //    break;
                    //case 40:
                    //    typename = "xwk";
                    //    break;
                    //case 41:
                    //    typename = "tuig";
                    //    break;
                    //case 42:
                    //    typename = "hrsd";
                    //    break;
                    //case 43:
                    //    typename = "hrzc";
                    //    break;
                    //case 44:
                    //    typename = "hrcz";
                    //    break;
                    //case 45:
                    //    typename = "hrzb";
                    //    break;
                    case 1:
                        typename = "zixun";
                        break;
                    case 2:
                        typename = "xiezhen";
                        break;
                    case 3:
                        typename = "zuopin";
                        break;
                    case 4:
                        typename = "zhuanfang";
                        break;
                    case 5:
                        typename = "hrzt";
                        break;
                    case 6:
                        typename = "gg";
                        break;
                    case 35:
                        typename = "hrfs";
                        break;
                    case 40:
                        typename = "xingwangka";
                        break;
                    case 41:
                        typename = "yingxiao";
                        break;
                    case 42:
                        typename = "whjj/redian";
                        break;
                    case 43:
                        typename = "hrzc";
                        break;
                    case 44:
                        typename = "chengzhang";
                        break;
                    case 45:
                        typename = "whjj/wanghongzhubo";
                        break;
                    case 46:
                        typename = "whjj/weibo";
                        break;
                    case 47:
                        typename = "whjj/weixin";
                        break;
                    case 49:
                        typename = "shangyi";
                        break;
                    case 50:
                        typename = "kuzhuang";
                        break;
                    case 51:
                        typename = "qunzhuang";
                        break;
                    case 52:
                        typename = "nvxie";
                        break;
                    case 53:
                        typename = "baobao";
                        break;
                    case 54:
                        typename = "peishi";
                        break;
                    case 55:
                        typename = "meizhuang";
                        break;
                    case 354:
                        typename = "zhinan";
                        break;
                }
            }
            else
            {
                switch (get_type)
                {
                    case 1:
                        typename = "红人资讯";
                        break;
                    case 2:
                        typename = "红人写真";
                        break;
                    case 3:
                        typename = "红人作品";
                        break;
                    case 4:
                        typename = "红人专访";
                        break;
                    case 5:
                        typename = "红人专题";
                        break;
                    case 6:
                        typename = "红人公告";
                        break;
                    case 35:
                        typename = "红人风尚";
                        break;
                    case 40:
                        typename = "星网咖";
                        break;
                    case 41:
                        typename = "网红营销";
                        break;
                    case 42:
                        typename = "热点";
                        break;
                    case 43:
                        typename = "众筹";
                        break;
                    case 44:
                        typename = "成长";
                        break;
                    case 45:
                        typename = "主播";
                        break;
                    case 46:
                        typename = "微博";
                        break;
                    case 47:
                        typename = "微信";
                        break;
                    case 49:
                        typename = "shangyi";
                        break;
                    case 50:
                        typename = "kuzhuang";
                        break;
                    case 51:
                        typename = "qunzhuang";
                        break;
                    case 52:
                        typename = "nvxie";
                        break;
                    case 53:
                        typename = "baobao";
                        break;
                    case 54:
                        typename = "peishi";
                        break;
                    case 55:
                        typename = "meizhuang";
                        break;
                    case 354:
                        typename = "搭配指南";
                        break;
                    default:
                        typename = "红人资讯";
                        break;
                }
            }

            return typename;
        }

        /// <summary>
        /// 获取当前被访问页面的面包屑导航
        /// </summary>
        /// <param name="type">当前被访问页面的父级页面ID</param>
        /// <param name="stype">当前被访问页面的子级类型ID</param>
        /// <param name="page">0：列表，1：详情</param>
        /// <returns></returns>
        public static string Get_Article_Nav(int type, int stype,int page=0)
        {
            sbr.Clear();

            string navname = "资讯";
            string sonname = "资讯";
            string article_url = "hrzx";
            article_url = "zixun";

            switch (type)
            {
                //case 1:
                //    article_url = "hrzx";
                //    break;
                //case 2:
                //    article_url = "hrxz";
                //    break;
                //case 3:
                //    article_url = "hrzp";
                //    break;
                //case 4:
                //    article_url = "hrzf";
                //    break;
                //case 5:
                //    article_url = "hrzt";
                //    break;
                //case 6:
                //    article_url = "gg";
                //    break;
                //case 35:
                //    article_url = "hrfs";
                //    break;
                //case 40:
                //    article_url = "xwk";
                //    break;
                //case 41:
                //    article_url = "tuig";
                //    break;
                //case 42:
                //    article_url = "hrsd";
                //    break;
                //case 43:
                //    article_url = "hrzc";
                //    break;
                //case 44:
                //    article_url = "hrcz";
                //    break;
                //case 45:
                //    article_url = "hrzb";
                //    break;
                case 1:
                    article_url = "zixun";
                    break;
                case 2:
                    article_url = "xiezhen";
                    break;
                case 3:
                    article_url = "zuopin";
                    break;
                case 4:
                    article_url = "zhuanfang";
                    break;
                case 5:
                    article_url = "hrzt";
                    break;
                case 6:
                    article_url = "gg";
                    break;
                case 35:
                    article_url = "hrfs";
                    break;
                case 40:
                    article_url = "xingwangka";
                    break;
                case 41:
                    article_url = "yingxiao";
                    break;
                case 42:
                    article_url = "whjj/redian";
                    break;
                case 43:
                    article_url = "hrzc";
                    break;
                case 44:
                    article_url = "chengzhang";
                    break;
                case 45:
                    article_url = "whjj/wanghongzhubo";
                    break;
                case 46:
                    article_url = "whjj/weibo";
                    break;
                case 47:
                    article_url = "whjj/weixin";
                    break;
                case 354:
                    article_url = "zhinan";
                    break;
            }

            article_url = "/"+article_url;

            if (type > 0)
            {
                navname = new BLL.HRENH_ARTICLE_TYPE().GetModel(type).TypeName;

                sonname = navname.Replace("红人", "");
            }

            if (stype > 0)
            {
                sonname = new BLL.HRENH_ARTICLE_TYPE().GetModel(stype).TypeName;
                if (type < 1)
                {
                    sonname = sonname.Replace("红人", "");

                }
            }

            if (page == 0)
            {
                sbr.AppendFormat("<span>衣品搭配</span><i>></i><span>{0}</span><i>></i><span>{1}列表</span>|{1}列表", navname.Replace("红人", ""), sonname);
            }
            else
            {
                navname=navname.Replace("红人", "");
                navname = "<a href='" + article_url + "' title='" + navname + "'>" + navname + "</a>";

                string hrenh_navname = "<a href='http://www.ypindapei.com' title='衣品搭配'>衣品搭配</a>";

                if (stype > 0)
                {
                    if (type < 1)
                    {
                        sbr.AppendFormat("<span>{1}</span><i>></i><span>{0}</span><i>></i><span>正文</span>", navname, hrenh_navname);
                    }
                    else
                    {
                        string surl = "/zhinan/";
                        switch (stype)
                        {
                            case 359:
                                surl += "clys";
                                break;
                            case 360:
                                surl += "fzdp";
                                break;
                            case 361:
                                surl += "hzjq";
                                break;
                            case 362:
                                surl += "hdth";
                                break;
                            case 369:
                                surl += "psdp";
                                break;
                        }
                        surl += "/";
                        //sonname = "<a href='" + article_url + "_1_" + stype + "' title='" + sonname + "'>" + sonname + "</a>";
                        sonname = "<a href='" + surl + "' title='" + sonname + "'>" + sonname + "</a>";
                        sbr.AppendFormat("<span>{2}</span><i>></i><span>{0}</span><i>></i><span>{1}</span><i>></i><span>正文</span>", navname, sonname, hrenh_navname);
                    }
                }
                else
                {
                    sbr.AppendFormat("<span>{1}</span><i>></i><span>{0}</span><i>></i><span>正文</span>", navname, hrenh_navname);
                }
                
            }

            return sbr.ToString();
        }

        /// <summary>
        /// 获取列表页热点文章
        /// </summary>
        /// <returns></returns>
        public static string Get_Hot_Articel()
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(0, " pageid=2100 and cid=1001 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 1, 0);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string show_title = StringHelper.ReturnNumStr(title,3, 20);


                    sbr.AppendFormat("<li>");

                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">{1}</a>", title, show_title);
                    }
                    else
                    {
                        if (httpurl.ToLower().Contains("http://"))
                        {
                            sbr.AppendFormat("<a  {1} target='_blank' title=\"{0}\">{2}</a>", title, Return_HttpURL.Return_Url(httpurl, openstyle), show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a  href='{1}' target='_blank' title=\"{0}\">{2}</a>", title, Return_HttpURL.Return_Url(httpurl, openstyle), show_title);
                        }
                    }

                    sbr.AppendFormat("</li>");
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
        public static string Get_Tj_Img(int top,int pageid,int cid)
        {
            sbr.Clear();


            ds = recommend_bll.GetList_NewIndex(top, " pageid=" + pageid + " and cid=" + cid + " ");

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

                    if (cid == 1002 || cid==1003)
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
                        sbr.AppendFormat("<a title=\"{0}\" class=\"white\" {1}><img width=\"320\" height=\"250\" border=\"0\" alt=\"{0}\" src=\"{2}\"><em class=\"fcut\"><span>{3}</span></em></a>",title,httpurl,imgurl,show_title);
                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("</div>");
                    }
                    else if (pageid == 2300 && cid == 1001)
                    {
                        sbr.AppendFormat("<div>");

                        httpurl = Return_HttpURL.Return_Url(httpurl, openstyle);

                        sbr.AppendFormat("<a title=\"{0}\" {1}><img alt=\"{0}\" src=\"{2}\"></a>", title, httpurl, imgurl);
                        sbr.AppendFormat("</div>");
                    }
                    else if (cid == 1004)
                    {
                        string curhref = " href='javascript:;' ";

                        if (httpurl != "")
                        {
                            curhref = " href='" + httpurl + "' target='_blank' ";
                        }

                        string red_title =title;

                        if (title == "羽绒服" || title == "呢子大衣" || title == "卫衣")
                        {
                            red_title = "<font style='font-weight:bold;'>" + title + "</font>";
                        }

                        sbr.AppendFormat("<a {1} title=\"{0}\">{2}</a><span style=\"margin-left:10px;margin-right:10px;\">/</span>",title,curhref,red_title);
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
                        sbr.AppendFormat("<a {0} title=\"{1}\"><img src=\"{2}\" alt=\"{1}\"></a>", httpurl,title,imgurl);
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
                        sbr.AppendFormat("<dt><a {0} title=\"{1}\"><img src=\"{2}\" alt=\"{1}\"></a></dt>",httpurl,title,imgurl);
                        sbr.AppendFormat("<dd><a {0} title=\"{1}\">{2}</a></dd>",httpurl,title,show_title);
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
                        sbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>",httpurl,title,show_title);
                        sbr.AppendFormat("<span>{0}</span>",desc);
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

        /// <summary>
        /// 获取指定数目的最新排行
        /// </summary>
        /// <param name="top">具体的数目，可为0</param>
        /// <returns></returns>
        public static string Get_Zxph(int top)
        {
            sbr.Clear();
 
            //ds = DbHelperSQL.Query("select top "+top+" id,title from hrenh_article where IsNewPh=1 and (types in(1,4) or types_pid in(1,4)) and isend=0 and pub_state=0 order by istop desc,edittime desc");


            string sql = "select top " + top + " id,title,types from hrenh_article where isend=0 and pub_state=0";

            sql += " and (types in(42) or types_pid in(42)) ";

            sql += " order by istop desc,edittime desc";

            ds = DbHelperSQL.Query(sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int cnt = 0;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    cnt++;

                    string clsname = "";

                    if (cnt == ds.Tables[0].Rows.Count)
                    {
                        clsname = "class=\"linenone\" ";
                    }

                    sbr.AppendFormat("<li {4}><i>{0}</i><a href=\"/zixun/detail_{1}.html\" title=\"{2}\" target=\"_blank\">{3}</a></li>", cnt, dr["id"].ToString(), StringHelper.ReturnNumStr(dr["title"].ToString(), 1, 0), StringHelper.ReturnNumStr(dr["title"].ToString(), 1, 16), clsname);
                }
            }


            return sbr.ToString();
        }


        /// <summary>
        /// 根据新资讯重写地址获取类型ID
        /// </summary>
        /// <param name="prname">新资讯重写地址</param>
        /// <param name="type">0:第一子级，1：其它子级</param>
        /// <returns></returns>
        public static int Get_Dianpu_Type_Id(string prname,int type)
        {
            int typeid = 0;

            try
            {
                DataSet ds = DbHelperSQL.Query("select id,pid from HRENH_ARTICLE_TYPE where TypeRename='"+prname+"'");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int id = Convert.ToInt32(dr["id"].ToString());
                        int pid = Convert.ToInt32(dr["pid"].ToString());

                        if (type == 0)
                        {
                            if (pid == 0)
                            {
                                typeid = id;
                            }
                            else
                            {
                                Model.HRENH_ARTICLE_TYPE tmodel = new BLL.HRENH_ARTICLE_TYPE().GetModel(pid);

                                if (tmodel != null)
                                {
                                    typeid = tmodel.PID;
                                }
                                else
                                {
                                    typeid = pid;
                                }
                            }
                        }
                        else
                        {
                            typeid = id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return typeid;
        }


        /// <summary>
        /// 根据新资讯重写地址获取类型最顶级重写地址
        /// </summary>
        /// <param name="type">文章类型</param>
        /// <returns></returns>
        public static string Get_Type_Rname(int type)
        {
            DataSet type_ds = DbHelperSQL.Query("select TypeRename from hrenh_article_type where id=" + type + "");

            string nav_http_name = "";

            if (type_ds != null && type_ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < type_ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = type_ds.Tables[0].Rows[i];

                    nav_http_name = dr["TypeRename"].ToString();
                }
            }

            return nav_http_name;
        }

    }
}