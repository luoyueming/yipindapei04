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
    /// FindPwdHandle 的摘要说明
    /// </summary>
    public class FindPwdHandle : Base.BasePage, IHttpHandler, IRequiresSessionState
    {
        Model.RED_USER user_model = new Model.RED_USER();
        BLL.RED_USER user_bll = new BLL.RED_USER();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            string action = context.Request["action"].ToString();

            //禁止浏览器直接访问ajax页面
            if (HttpContext.Current.Request.UrlReferrer.Host != HttpContext.Current.Request.Url.Host)
            {
                HttpContext.Current.Response.Redirect("/404");
            }
            else
            {
                switch (action)
                {
                    case "ValidatePhone"://验证当前手机是否被注册过
                        ValidatePhone(context);
                        break;
                    case "SendPhoneVerifyCode"://发送手机验证码
                        SendPhoneVerifyCode(context);
                        break;
                    case "VerifyCodeIsExists"://判断验证手机验证码是否正确
                        VerifyCodeIsExists(context);
                        break;
                    case "Update_User_Pwd"://修改用户的密码
                        Update_User_Pwd(context);
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
                int t = 0;
                try
                {
                    string sql = "select top 1 userid from THINK_SNS_DB.dbo.HRENH_THINK_USER where tel=@tel order by userid desc";
                    SqlParameter[] para = { new SqlParameter("@tel", SqlDbType.NVarChar, 50) };
                    para[0].Value = tel;

                    t = Convert.ToInt32(DbHelperSQL.GetSingle(sql, para).ToString());
                }
                catch (Exception ex)
                {

                }
                if (t <= 0)
                {
                    result = "nouser";
                }
                else
                {
                    CookieManage_HongRenHui.AddCookie("FINDRED_HREN_USERID", t.ToString(), 1, false);
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

        #region  发送手机验证码

        private void SendPhoneVerifyCode(HttpContext context)
        {
            int uid = CheckIsLogin_HongRenHui.GetUserID("FINDRED_HREN_USERID");

            string phone = "";

            user_model = user_bll.GetModel(uid);

            if (user_model != null)
            {
                phone = user_model.TEL;
            }

            if (phone != "")
            {
                Random rd = new Random();
                string code = rd.Next(100000, 999999).ToString();



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
                                    context.Response.Write("error_yzm");
                                }
                                else
                                {
                                    try
                                    {
                                        if (HttpContext.Current.Session["PhoneVerifyCode"] == null)
                                        {
                                            HttpContext.Current.Session["PhoneVerifyCode"] = code + "-" + phone;
                                        }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(HttpContext.Current.Session["PhoneVerifyCode"].ToString()))
                                            {
                                                HttpContext.Current.Session["PhoneVerifyCode"] = code + "-" + phone;
                                            }
                                            else
                                            {
                                                string strSess = HttpContext.Current.Session["PhoneVerifyCode"].ToString();
                                                HttpContext.Current.Session["PhoneVerifyCode"] = strSess + "," + code + "-" + phone;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        HttpContext.Current.Session["PhoneVerifyCode"] = code + "-" + phone;
                                    }
                                    finally
                                    {
                                        //判断当前用户是否已经绑定过手机
                                        #region  发送短信
                                        string shortMessageWord = "衣品搭配密码找回验证码：" + code + "，牢记密码，小主记得常来哦。";
                                        string result = Send_Short_Message.Send_Message_Short(shortMessageWord, phone);
                                        //string result = "ok";
                                        //result = result + code;

                                        context.Response.Write(result);
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                context.Response.Write("noyzm");
                            }
                        }
                        else
                        {
                            context.Response.Write("noyzm");
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    context.Response.Write("noyzm");
                }
            }
            else
            {
                context.Response.Write("nologin");
            }


        }

        #endregion

        #region  判断验证手机验证码是否正确

        private void VerifyCodeIsExists(HttpContext context)
        {
            int uid = CheckIsLogin_HongRenHui.GetUserID("FINDRED_HREN_USERID");

            string phone = "";

            user_model = user_bll.GetModel(uid);

            if (user_model != null)
            {
                phone = user_model.TEL;
            }

            if (phone != "")
            {
                try
                {
                    string verifycode = context.Request["Vc"];

                    string phone2 = "", verifycode2 = "";

                    string[] items = null;

                    if (HttpContext.Current.Session["PhoneVerifyCode"] != null)
                    {
                        string PhoneVerifyCode = "";
                        PhoneVerifyCode = HttpContext.Current.Session["PhoneVerifyCode"].ToString();

                        if (PhoneVerifyCode.IndexOf(',') != -1)//有多个手机号和验证码，因为短信有可能延迟用户就会收到多个验证码，应该保证每个验证码是正确的。
                        {
                            items = PhoneVerifyCode.Split(',');
                        }
                        else
                        {
                            items = new string[1];
                            items[0] = PhoneVerifyCode;
                        }
                    }

                    bool isOk = false;

                    for (int i = 0; i < items.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(items[i]))
                        {
                            phone2 = items[i].Split('-')[1];
                            verifycode2 = items[i].Split('-')[0];

                            if (phone == phone2 && verifycode == verifycode2)
                            {
                                isOk = true;
                                break;
                            }
                        }
                    }

                    if (isOk)
                    {
                        CookieManage_HongRenHui.AddCookie("FINDTEL_HREN_STATE", "1", 1, false);
                        context.Response.Write("1");
                    }
                    else
                    {
                        context.Response.Write("0");
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write("0");
                }
            }
            else
            {
                context.Response.Write("nologin");
            }
            context.Response.End();
        }
        #endregion

        #region  修改用户的密码
        private void Update_User_Pwd(HttpContext context)
        {
            string result = "ok";

            int uid = CheckIsLogin_HongRenHui.GetUserID("FINDRED_HREN_USERID");

            string phone = "";

            user_model = user_bll.GetModel(uid);

            if (user_model != null)
            {
                if (CookieManage_HongRenHui.GetCookieValue("FINDTEL_HREN_STATE") != "1")
                {
                    result = "notel";
                }
                else
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
            }
            else
            {
                result = "nologin";
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