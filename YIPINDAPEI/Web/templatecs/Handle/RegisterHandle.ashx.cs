﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;
using NewXzc.Common;
using NewXzc.Model;
using NewXzc.BLL;
using System.Web.SessionState;
using NewXzc.DBUtility;

namespace NewXzc.Web.templatecs.Handle
{
    /// <summary>
    /// RegisterHandle 的摘要说明
    /// </summary>
    public class RegisterHandle : Base.BasePage, IHttpHandler, IRequiresSessionState
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
                    case "Is_Register"://注册操作
                        Is_Register(context);
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
                if (user_bll.GetRecordCount(" tel='" + tel + "' ") > 0)
                {
                    result = "no_repeat";
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
            string phone = String_Manage.Return_Request_Str("Phone");

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
                                if (phone != "")
                                {
                                    Random rd = new Random();
                                    string code = rd.Next(100000, 999999).ToString();

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
                                        var istel = ValidatePhone_Function(context, phone);
                                        if (istel != "ok")
                                        {
                                            context.Response.Write(istel);
                                        }
                                        else
                                        {
                                            #region  发送短信
                                            string shortMessageWord = "衣品搭配注册验证码：" + code + "，欢迎加入衣品搭配。";
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
                                    context.Response.Write("notel");
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

        #endregion

        #region  判断验证手机验证码是否正确

        private void VerifyCodeIsExists(HttpContext context)
        {
            string phone = String_Manage.Return_Request_Str("Phone");

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
                        //判断当前用户是否已经绑定过手机
                        var istel = ValidatePhone_Function(context, phone);
                        if (istel != "ok")
                        {
                            context.Response.Write(istel);
                            return;
                        }

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
                context.Response.Write("notel");
            }
            context.Response.End();
        }
        #endregion

        #region  注册操作

        private void Is_Register(HttpContext context)
        {
            string result = "ok";

            string tel = String_Manage.Return_Request_Str("tel");
            string pwd = String_Manage.Return_Request_Str("upwd");
            string uname = String_Manage.Return_Request_Str("uname");

            

            var istel = ValidatePhone_Function(context, tel);
            if (istel != "ok")
            {
                result = istel;
            }
            else
            {
                if (pwd == "")
                {
                    result = "nopwd";
                }
                else if (uname == "")
                {
                    result = "nouser";
                }
                else
                {
                    pwd = NewXzc.Common.DEncrypt.DESEncrypt.Encrypt(pwd);//给密码加密

                    int userguid = user_bll.GetMaxId();

                    //user_model.USERID = user_bll.GetMaxId();
                    //user_model.TEL = tel;
                    //user_model.TEL_STATE = 1;
                    //user_model.USER_HEAD = "default.jpg";
                    //user_model.PWD = pwd;
                    //user_model.NICKNAME = uname;
                    //user_model.IP = RequestHelper.GetIP();
                    //user_model.ADDTIME = DateTime.Now;
                    //user_model.REMARK = "";

                    string empty = "";
                    int cnt_0 = 0;
                    int cnt_1 = 1;
                    int utype = 1;

                    user_model.USERID = userguid;
                    user_model.USERNAME = uname;
                    user_model.PWD = pwd;
                    user_model.TEL = tel;
                    user_model.TEL_STATE = cnt_1;
                    user_model.USER_HEAD = "default.jpg";
                    user_model.SEX = cnt_1;
                    user_model.EMAIL = empty;
                    user_model.USER_TYPE = utype;
                    user_model.ISCOMPLETE = cnt_0;
                    user_model.PROVINCE = empty;
                    user_model.CITY = empty;
                    user_model.AREA = empty;
                    user_model.PEOPLE_IDENTITY = empty;
                    user_model.OCCUPATION = empty;
                    user_model.PERSONALITY = empty;
                    user_model.INTRODUCE = empty;
                    user_model.IDENTIFICATION_STATE = cnt_0;
                    user_model.SAME_HOBBY_PEOPLE = empty;
                    user_model.EXP = cnt_0;
                    user_model.SCORE = cnt_0;
                    user_model.REALM_NAME = empty;
                    user_model.ADDTIME = DateTime.Now;
                    user_model.IS_RED = cnt_0;
                    user_model.STATE = cnt_0;
                    user_model.REMARK = empty;
                    user_model.Person_Desc = empty;
                    user_model.Person_NickName = empty;
                    user_model.Porder = cnt_0;
                    user_model.UpdateTime = DateTime.Now;
                    user_model.PLAT_VAL = empty;
                    user_model.SPECIALTY_VAL = empty;
                    user_model.IPURL = empty;


                    if (user_bll.GetRecordCount(" USERNAME='" + uname + "' and userid<>" + userguid + " ") <= 0)
                    {

                        if (!user_bll.Add(user_model))
                        {
                            result = "no";
                        }
                        else
                        {
                            CookieManage_HongRenHui.AddCookie("RED_HREN_USERID", userguid.ToString(), 1, false);
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