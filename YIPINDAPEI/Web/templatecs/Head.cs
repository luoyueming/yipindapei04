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
    public class Head : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);
        }

        /// <summary>
        /// 让头部导航当前栏目高亮显示，要高亮显示的栏目下标，1：首页，2：红人新闻，3：图片写真，4：影视作品，5：红人背后，6：红人观，7：红人爱品，8：红人求报道，10：网络红人
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cur">要高亮显示的栏目下标，1：首页，2：红人新闻，3：图片写真，4：影视作品，5：红人背后，6：红人观，7：红人爱品，8：红人求报道，10：网络红人</param>
        public void Init_Head(NVelocity.VelocityContext context, int cur)
        {
            context.Put("item", cur);

            #region  加载导航名称
            DataSet type_ds = DbHelperSQL.Query("select top 5 id,typename from hrenh_article_type where state=1 order by id asc");
            StringBuilder type_sbr = new StringBuilder();

            if (type_ds != null && type_ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < type_ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = type_ds.Tables[0].Rows[i];

                    int typeids=String_Manage.Return_Int(dr["id"].ToString(),0);

                    string nav_http_url = "/article_" + typeids;
                    string nav_http_name = dr["typename"].ToString();

                    nav_http_url ="/"+ ArticleHelper.Get_Nav_Type_Name(typeids);


                    //if (String_Manage.Return_Request_Int("type",0) == Convert.ToInt32(dr["id"].ToString()))
                    //{
                    //    type_sbr.AppendFormat("<li>");
                    //    type_sbr.AppendFormat("<span>");
                    //    type_sbr.AppendFormat("<a href=\"{0}\" class=\"active\" title=\"{1}\">{1}</a>", nav_http_url, nav_http_name);
                    //    type_sbr.AppendFormat("</span>");
                    //    type_sbr.AppendFormat("</li>");
                    //}
                    //else
                    //{
                    //    type_sbr.AppendFormat("<li>");
                    //    type_sbr.AppendFormat("<span>");
                    //    type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", nav_http_url, nav_http_name);
                    //    type_sbr.AppendFormat("</span>");
                    //    type_sbr.AppendFormat("</li>");
                    //}

                    if (String_Manage.Return_Request_Int("type", 0) == Convert.ToInt32(dr["id"].ToString()))
                    {
                        type_sbr.AppendFormat("<li>");
                        type_sbr.AppendFormat("<a href=\"{0}\" class=\"active\" title=\"{1}\">{1}</a>", nav_http_url, nav_http_name);
                        type_sbr.AppendFormat("</li>");
                    }
                    else
                    {
                        type_sbr.AppendFormat("<li>");
                        type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", nav_http_url, nav_http_name);
                        type_sbr.AppendFormat("</li>");
                    }

                }
            }

            context.Put("nav_list",type_sbr.ToString());
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
    }
}