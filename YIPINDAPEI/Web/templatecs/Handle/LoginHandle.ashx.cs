using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;
using System.Data.SqlClient;
using System.Data;
using NewXzc.DBUtility;
using System.Web.SessionState;

namespace NewXzc.Web.templatecs.Handle
{
    /// <summary>
    /// LoginHandle 的摘要说明
    /// </summary>
    public class LoginHandle : Base.BasePage, IHttpHandler, IRequiresSessionState
    {
        Model.RED_USER user_model = new Model.RED_USER();
        BLL.RED_USER user_bll = new BLL.RED_USER();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            
            //禁止浏览器直接访问ajax页面
            if (HttpContext.Current.Request.UrlReferrer.Host != HttpContext.Current.Request.Url.Host)
            {
                HttpContext.Current.Response.Redirect("/404");
            }
            else
            {

                string action = context.Request["action"].ToString();

                switch (action)
                {
                    case "ValidatePhone"://验证当前手机是否被注册过
                        ValidatePhone(context);
                        break;
                    case "Is_Login"://验证是否登录成功
                        Is_Login(context);
                        break;
                    case "GetLoginState"://验证当前用户是否登录成功
                        GetLoginState(context);
                        break;
                }
            }

        }

        #region  具体操作

        #region  添加验证手机是否被注册过的方法

        private string ValidatePhone_Function(HttpContext context, string tel)
        {
            string result = "ok";

            if (tel != "")
            {
                if (user_bll.GetRecordCount(" tel='" + tel + "' ") <= 0)
                {
                    result = "nouser";
                }
            }
            else
            {
                result = "notel";
            }

            return result;
        }

        #endregion

        #region  验证当前手机是否被注册过

        private void ValidatePhone(HttpContext context)
        {
            string tel = String_Manage.Return_Request_Str("tel");

            string result = ValidatePhone_Function(context, tel);

            context.Response.Write(result);
        }

        #endregion

        #region  验证是否登录成功

        private void Is_Login(HttpContext context)
        {
            string result = "ok";

            string tel = String_Manage.Return_Request_Str("tel");
            string pwd = String_Manage.Return_Request_Str("upwd");

            pwd = NewXzc.Common.DEncrypt.DESEncrypt.Encrypt(pwd);//给密码加密

            int remmber = String_Manage.Return_Request_Int("remmber", 0);

            var istel = ValidatePhone_Function(context, tel);
            if (istel != "ok")
            {
                result = istel;
            }
            else
            {

                //前台生成的验证码，用于点击发送验证码前验证用
                string send_code_yzm = String_Manage.Return_Request_Str("send_code_yzm");

                if (send_code_yzm != "")
                {
                    try
                    {
                        if (HttpContext.Current.Session["code"] != null)
                        {
                            if (!string.IsNullOrEmpty(HttpContext.Current.Session["code"].ToString()))
                            {
                                string str_yzm = HttpContext.Current.Session["code"].ToString();
                                if (send_code_yzm != str_yzm)
                                {
                                    result = "error_yzm";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (result == "ok")
                {

                    string sql = "select top 1 userid from THINK_SNS_DB.dbo.HRENH_THINK_USER where tel=@tel and pwd=@pwd order by userid asc";

                    SqlParameter[] para = {
                                      new SqlParameter("@tel",SqlDbType.NVarChar,50),
                                      new SqlParameter("@pwd",SqlDbType.NVarChar,50)
                                      };
                    para[0].Value = tel;
                    para[1].Value = pwd;

                    int t = 0;
                    try
                    {
                        t = Convert.ToInt32(DbHelperSQL.GetSingle(sql, para).ToString());
                    }
                    catch (Exception ex)
                    {

                    }

                    if (t <= 0)
                    {
                        result = "no";
                    }
                    else
                    {
                        result = CheckIsLogin_HongRenHui.Get_User_CenterUrl_HongRenHui();

                        if (remmber == 0)
                        {
                            CookieManage_HongRenHui.AddCookie("RED_HREN_USERID", t.ToString(), 1, false);
                        }
                        else
                        {
                            CookieManage_HongRenHui.AddCookie("RED_HREN_USERID", t.ToString(), 7, false);
                        }
                    }
                }
            }


            context.Response.Write(result);
        }

        #endregion

        #region  验证当前用户是否登录成功

        private void GetLoginState(HttpContext context)
        {
            string result = "ok";

            if (!CheckIsLogin_HongRenHui.IsLogin("RED_HREN_USERID"))
            {
                result = "/login";
            }

            context.Response.Write(result);
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