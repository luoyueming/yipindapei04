using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Common;
using System.Data;
using NewXzc.DBUtility;
using System.Text;
using NewXzc.Web.Common;

namespace NewXzc.Web.templatecs
{
    public class Head_Article : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);
        }

        /// <summary>
        /// 让头部导航当前栏目高亮显示，要高亮显示的栏目下标，1：首页，2：红人资讯，3：图片写真，4：红人作品，5：红人专访，6：红人专题，7：红人爱品，8：红人求报道，10：网络红人
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cur">要高亮显示的栏目下标，1：首页，2：红人资讯，3：图片写真，4：红人作品，5：红人专访，6：红人专题，7：红人爱品，8：红人求报道，10：网络红人</param>
        public void Init_Head(NVelocity.VelocityContext context, int cur)
        {
            context.Put("item", cur);

            if (cur > 1)
            {
                cur--;
            }

            #region  加载导航名称
            //DataSet type_ds = DbHelperSQL.Query("select top 5 id,typename from hrenh_article_type where state=1 order by id asc");
            //StringBuilder type_sbr = new StringBuilder();

            //if (type_ds != null && type_ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < type_ds.Tables[0].Rows.Count; i++)
            //    {
            //        DataRow dr = type_ds.Tables[0].Rows[i];

            //        int typeids = String_Manage.Return_Int(dr["id"].ToString(), 0);

            //        string nav_http_url = "/article_" + typeids;
            //        string nav_http_name = dr["typename"].ToString();

            //        nav_http_url = "/" + ArticleHelper.Get_Nav_Type_Name(typeids);
            //        int curtid=Convert.ToInt32(dr["id"].ToString());


            //        //if (String_Manage.Return_Request_Int("type",0) == Convert.ToInt32(dr["id"].ToString()))
            //        //{
            //        //    type_sbr.AppendFormat("<li>");
            //        //    type_sbr.AppendFormat("<span>");
            //        //    type_sbr.AppendFormat("<a href=\"{0}\" class=\"active\" title=\"{1}\">{1}</a>", nav_http_url, nav_http_name);
            //        //    type_sbr.AppendFormat("</span>");
            //        //    type_sbr.AppendFormat("</li>");
            //        //}
            //        //else
            //        //{
            //        //    type_sbr.AppendFormat("<li>");
            //        //    type_sbr.AppendFormat("<span>");
            //        //    type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", nav_http_url, nav_http_name);
            //        //    type_sbr.AppendFormat("</span>");
            //        //    type_sbr.AppendFormat("</li>");
            //        //}

            //        if (cur==curtid)
            //        {
            //            type_sbr.AppendFormat("<li>");
            //            type_sbr.AppendFormat("<a href=\"{0}\" class=\"active\" title=\"{1}\">{1}</a>", nav_http_url, nav_http_name);
            //            type_sbr.AppendFormat("</li>");
            //        }
            //        else
            //        {
            //            type_sbr.AppendFormat("<li>");
            //            type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", nav_http_url, nav_http_name);
            //            type_sbr.AppendFormat("</li>");
            //        }

            //    }
            //}

            //context.Put("nav_list", type_sbr.ToString());


            ////读取导航
            //context.Put("nav_list", Get_Top_Nav(context,cur));
            #endregion

            #region  登录状态
            //StringBuilder head_html =new StringBuilder();
            //context.Put("cluid",UserID_HongRenHui);

            //if (UserID_HongRenHui > 0)
            //{
                
            //    NewXzc.Model.RED_USER user_model_head = new BLL.RED_USER().GetModel(UserID_HongRenHui);

            //    string nickname = user_model_head.USERNAME;

            //    head_html.AppendFormat("<div class=\"opera-box login-box\">");
            //    head_html.AppendFormat("<a class=\"user-center\" href=\"/people_c\" title=\"{0}\">", nickname);
            //    head_html.AppendFormat("<span>");
            //    head_html.AppendFormat("<img src=\"{0}\" height=\"28\" width=\"28\" alt=\"{1}\">", ImgHelper.Return_User_Head(user_model_head.USER_HEAD, 3), nickname);
            //    head_html.AppendFormat("</span>");
            //    head_html.AppendFormat("<em>{0}</em>", StringHelper.ReturnNumStr(nickname, 0, 7));
            //    head_html.AppendFormat("</a>");
            //    head_html.AppendFormat("<a href=\"/logout\" title=\"退出\">退出</a>");

            //    head_html.AppendFormat("<span class=\"f-share-box\">");
            //    head_html.AppendFormat("<a class=\"weibo\" href=\"http://weibo.com/p/1006065656739545/home?from=page_100606&mod=TAB#place\" title=\"红人汇\" target=\"_blank\"></a>");
            //    head_html.AppendFormat("<a class=\"wechat\" href=\"javascript:void(0)\" title=\"\">");
            //    head_html.AppendFormat("<div class=\"wechat-imgBox\">");
            //    head_html.AppendFormat("<img src=\"/template/img/information/qrcode.jpg\" alt=\"红人汇\">");
            //    head_html.AppendFormat("</div>");
            //    head_html.AppendFormat("</a>");
            //    head_html.AppendFormat("</span>");

            //    head_html.AppendFormat("</div>");

            //}
            //else
            //{
            //    head_html.AppendFormat("<a href=\"/login\" title=\"登录\">登录</a>");
            //    //head_html.AppendFormat("<a href=\"javascript:;\" title=\"登录\" onclick=\"returnlogin()\">登录</a>");
            //    head_html.AppendFormat("<i>|</i>");
            //    head_html.AppendFormat("<a href=\"/register\" title=\"注册\">注册</a>");
            //}

            //context.Put("lstate", head_html.ToString());
            #endregion

            #region 记录用户的登录和时间

            if (UserID_HongRenHui > 0)
            {
                NewXzc.Model.RED_USER_LOGIN_RECORD model_logintime = new Model.RED_USER_LOGIN_RECORD();
                NewXzc.BLL.RED_USER_LOGIN_RECORD login_bll = new NewXzc.BLL.RED_USER_LOGIN_RECORD();


                int logintype = 1;

                string ipAddress = RequestHelper.GetIP();
                int isLogin = login_bll.GetRecordCount(" userid=" + UserID_HongRenHui + " and datediff(day,Login_Time,getdate())=0 and Login_Type=" + logintype + " ");

                string sessoinid = "0";
                try
                {
                    sessoinid = HttpContext.Current.Session.SessionID;
                }
                catch (Exception ex)
                {

                }

                try
                {
                    if (isLogin <= 0)
                    {
                        model_logintime.USERID = UserID_HongRenHui;
                        model_logintime.Login_IP = ipAddress;
                        model_logintime.SessionID = sessoinid;
                        model_logintime.Login_Time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        model_logintime.Remark = "";


                        model_logintime.Login_Type = logintype;

                        login_bll.Add(model_logintime);
                    }
                    else
                    {

                        model_logintime = login_bll.GetModel(UserID_HongRenHui, logintype);

                        if (model_logintime != null)
                        {
                            model_logintime.Login_IP = ipAddress;
                            model_logintime.Login_Time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            model_logintime.Remark = "";


                            model_logintime.Login_Type = logintype;

                            login_bll.Update(model_logintime);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            #endregion

        }

        /// <summary>
        /// 加载头部导航
        /// </summary>
        /// <param name="context"></param>
        private string Get_Top_Nav(NVelocity.VelocityContext context,int cur)
        {
            StringBuilder weiba_sbr = new StringBuilder();

            int topnum = 3;//5

            #region  加载导航名称
            string sql = "select top "+topnum+" id,pid,typename from hrenh_article_type where state=1 order by id asc";//旧版本
            sql = "select * from (";
            sql += "select top " + topnum + " id,pid,typename from hrenh_article_type where state=1 ";
            sql += "union ";
            //sql += "select id,pid,typename from hrenh_article_type where (id=35 or pid=35) and state=1";
            sql += "select id,pid,typename from hrenh_article_type where (id=44 or pid=44) and state=1";
            sql += ") a ";

            DataSet type_ds = DbHelperSQL.Query(sql);
            StringBuilder type_sbr = new StringBuilder();

            //type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f20\">{1}</a>", "http://www.ypindapei.com", "首页");
            //type_sbr.AppendFormat("<span></span>");

            int stype = String_Manage.Return_Request_Int("stype", 0);

            if (type_ds != null && type_ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < type_ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = type_ds.Tables[0].Rows[i];

                    int typeids = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    int pid = String_Manage.Return_Int(dr["pid"].ToString(), 0);

                    string nav_http_url = "/article_" + typeids;
                    string nav_http_name = dr["typename"].ToString();

                    nav_http_url = "/" + ArticleHelper.Get_Nav_Type_Name(typeids);
                    int curtid = Convert.ToInt32(dr["id"].ToString());

                    if (i > topnum)
                    {
                        nav_http_url += "_1_" + curtid;
                    }


                    if (i < topnum)
                    {
                        nav_http_name = nav_http_name.Replace("红人", "");
                    }

                    if (i != topnum)
                    {
                        if (cur == typeids && pid==0)
                        {
                            type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f18 active\">{1}</a>", nav_http_url, nav_http_name);
                        }
                        else
                        {
                            if (pid == 35)
                            {
                                switch(typeids)
                                {
                                    case 36:
                                        nav_http_url = "http://weiba.ypindapei.com/found_148";
                                        break;
                                    case 37:
                                        nav_http_url = "http://weiba.ypindapei.com/found_143";
                                        break;
                                    case 38:
                                        nav_http_url = "http://weiba.ypindapei.com/found_142";
                                        break;
                                    case 39:
                                        nav_http_url = "http://weiba.ypindapei.com/found_129";
                                        break;
                                }
                                if (stype == typeids)
                                {
                                    weiba_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f18 active\" target=\"_blank\">{1}</a>", nav_http_url, nav_http_name);
                                }
                                else
                                {
                                    weiba_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f18\" target=\"_blank\">{1}</a>", nav_http_url, nav_http_name);
                                }
                            }
                            else
                            {
                                type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f18\">{1}</a>", nav_http_url, nav_http_name);
                            }
                        }
                    }

                    //if (i == topnum)
                    //{
                    //    weiba_sbr.AppendFormat("<span></span>");

                    //    nav_http_url = "http://weiba.ypindapei.com";

                    //    if (cur==7)
                    //    {
                    //        weiba_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f22 active\" target=\"_blank\">{1}</a>", nav_http_url, nav_http_name);
                    //    }
                    //    else
                    //    {
                    //        weiba_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f22\" target=\"_blank\">{1}</a>", nav_http_url, nav_http_name);
                    //    }
                        
                    //}

                }
            }
            //type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f20\" target=\"_blank\">{1}</a>", "http://weiba.ypindapei.com", "红人微吧");
            //type_sbr.AppendFormat("<span></span>");
            //type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f20\">{1}</a>", "/hongren", "网络红人");


            #endregion

            context.Put("weiba", weiba_sbr.ToString());

            return type_sbr.ToString();
        }

    }
}