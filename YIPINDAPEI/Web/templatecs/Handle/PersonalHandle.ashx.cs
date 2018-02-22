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
    /// PersonalHandle 的摘要说明
    /// </summary>
    public class PersonalHandle : Base.BasePage, IHttpHandler, IRequiresSessionState
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
                    case "ValidateOldPwd"://验证当前用户所填原密码是否正确
                        ValidateOldPwd(context);
                        break;
                    case "Update_User_Pwd"://修改用户的密码
                        Update_User_Pwd(context);
                        break;
                    case "Update_User_Info"://修改用户信息（头像和昵称）
                        Update_User_Info(context);
                        break;
                }
            }

        }

        #region  具体操作

        #region  验证当前用户所填原密码是否正确
        private void ValidateOldPwd(HttpContext context)
        {
            string result = "ok";

            user_model = user_bll.GetModel(UserID_HongRenHui);

            if (user_model != null)
            {
                string pwd = String_Manage.Return_Request_Str("pwd");
                if (pwd == "")
                {
                    result = "noold";
                }
                else
                {
                    pwd = NewXzc.Common.DEncrypt.DESEncrypt.Encrypt(pwd);//给密码加密

                    if (user_model.PWD != pwd)
                    {
                        result = "no";
                    }

                }
            }
            else
            {
                result = "nologin";
            }

            context.Response.Write(result);
        }
        #endregion

        #region  修改用户的密码
        private void Update_User_Pwd(HttpContext context)
        {
            string result = "ok";

            user_model = user_bll.GetModel(UserID_HongRenHui);

            if (user_model != null)
            {
                string pwd = String_Manage.Return_Request_Str("pwd");
                if (pwd == "")
                {
                    result = "nopwd";
                }
                else
                {
                    pwd = NewXzc.Common.DEncrypt.DESEncrypt.Encrypt(pwd);//给密码加密
                    user_model.PWD = pwd;

                    if (!user_bll.Update(user_model,2))
                    {
                        result = "no";
                    }
                }
            }
            else
            {
                result = "nologin";
            }

            context.Response.Write(result);
        }
        #endregion

        #region  修改用户信息（头像和昵称）
        private void Update_User_Info(HttpContext context)
        {
            string result = "ok";

            if (UserID_HongRenHui <= 0)
            {
                result = "nologin";
            }
            else
            {
                string user_head = String_Manage.Return_Request_Str("user_logo");
                string uname = String_Manage.Return_Request_Str("uname");

                if (user_head == "")
                {
                    result = "nohead";
                }
                else if (uname == "")
                {
                    result = "noname";
                }
                else
                {
                    if (user_bll.GetRecordCount(" USERNAME='" + uname + "' and tel<>'" + user_bll.GetModel(UserID_HongRenHui).TEL + "' ") <= 0)
                    {
                        user_model = user_bll.GetModel(UserID_HongRenHui);
                        if (user_model != null)
                        {
                            user_model.USER_HEAD = user_head;
                            user_model.USERNAME = uname;

                            user_bll.Update(user_model,1);
                        }
                        else
                        {
                            result = "nouser";
                        }
                    }
                    else
                    {
                        result = "repname";
                    }
                }
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