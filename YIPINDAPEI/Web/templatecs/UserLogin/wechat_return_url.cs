using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;
using NewXzc.Common;
using NewXzc.DBUtility;

namespace NewXzc.Web.templatecs.UserLogin
{
    public class wechat_return_url:Base.BasePage
    {

        Model.RED_USER user_model = new Model.RED_USER();
        BLL.RED_USER user_bll = new BLL.RED_USER();

        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            Wchat_Register(context);
        }


        #region  微信注册

        private void Wchat_Register(NVelocity.VelocityContext context)
        {
            string result = "no";

            #region  获取微信登录的信息

            string wcode = String_Manage.Return_Request_Str("code");

            if (wcode != "")
            {
                // 获取用户的access_token和openid
                wcode = Get_Http_Url_Content.GetPostContent("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx2fbe800be068f5ec&secret=592edb2d54ec68eb935b3b731441cbf8&code=" + wcode + "&grant_type=authorization_code");
                NewXzc.Web.templatecs.Handle.ThirdRegisterHandle.Wchat wchat_access_token = JsonHelper.JsonDeserialize<NewXzc.Web.templatecs.Handle.ThirdRegisterHandle.Wchat>(wcode);

                if (!string.IsNullOrEmpty(wchat_access_token.access_token))
                {
                    // 获取用户信息
                    wcode = Get_Http_Url_Content.GetPostContent("https://api.weixin.qq.com/sns/userinfo?access_token=" + wchat_access_token.access_token + "&openid=" + wchat_access_token.openid + "");

                    NewXzc.Web.templatecs.Handle.ThirdRegisterHandle.GetUser_Wchat wchat_userinfo = JsonHelper.JsonDeserialize<NewXzc.Web.templatecs.Handle.ThirdRegisterHandle.GetUser_Wchat>(wcode);

                    if (!string.IsNullOrEmpty(wchat_userinfo.openid))
                    {
                        result = Is_Register_Wchat(wchat_userinfo.nickname, wchat_userinfo.sex, wchat_userinfo.headimgurl, 3, wchat_userinfo.openid);
                    }
                }

            }

            context.Put("resultss",result);

            if (result.Contains("people"))
            {
                context.Put("redirecturl", "http://www.ypindapei.com");
            }

            #endregion
        }

        #endregion


        #region  注册操作，带参数

        /// <summary>
        /// 执行微信登录，并注册信息操作
        /// </summary>
        /// <param name="uname">用户名称或昵称</param>
        /// <param name="sex_val">性别值</param>
        /// <param name="user_head">用户头像地址</param>
        /// <param name="third_logintype">第三方登录类型，0：默认，1：QQ，2：Sina，3：微信</param>
        /// <param name="third_uopenid">第三方登录账号的唯一标识ID</param>
        /// <returns></returns>
        private string Is_Register_Wchat(string uname, string sex_val, string user_head, int third_logintype, string third_uopenid)
        {
            string result = "ok";

            string tel = "123456";
            string pwd = "123456";
            int sex = 1;

            //登录途径，0：红人汇PC端，1：红人汇wap端，2：微吧PC端，3：微吧wap端
            int login_type = Convert.ToInt32(CookieManage_HongRenHui.GetCookieValue("RED_HREN_Login_Type"));


            pwd = NewXzc.Common.DEncrypt.DESEncrypt.Encrypt(pwd);//给密码加密

            if (sex_val == "1" || sex_val == "man" || sex_val == "男")
            {
                sex = 1;
            }
            else
            {
                sex = 2;
            }

            //int third_old_logintype = Convert.ToInt32(CookieManage_HongRenHui.GetCookieValue("RED_HREN_Third_Login"));


            if (user_bll.GetRecordCount(" third_openid='" + third_uopenid + "' ") <= 0)
            {

                if (third_uopenid != "")// && uname != "qzuser"  未授权(uname == "qzuser")
                {

                    //string[] uhead = user_head.Split(';');

                    //if (third_logintype == 1)
                    //{
                    //    //大头像
                    //    user_head = Save_Img_Http.Save_Img_WebRequest(uhead[0].ToString(), "jpg", 1);
                    //    ////小头像
                    //    //Save_Img_Http.Save_Img_WebRequest(uhead[1].ToString(), "jpg", 3);
                    //}
                    //else
                    //{
                    //    //大头像
                    //    user_head = Save_Img_Http.Save_Img_WebRequest(user_head, "jpg", 1);
                    //}

                    //大头像
                    int imgpre = 0;
                    if (third_logintype < 3)
                    {
                        imgpre = 1;
                    }
                    user_head = Save_Img_Http.Save_Img_WebRequest(user_head, "jpg", imgpre);

                    user_head = user_head.Replace("large_", "").Replace(ImgHelper.GetCofigShowUrl(), "");

                    int userguid = user_bll.GetMaxId();

                    string empty = "";
                    int cnt_0 = 0;
                    int utype = login_type + 1;//用户注册途径，0：红人议会，1：红人汇，2：红人汇手机端，3：微吧，4：微吧手机端，5：红人爱品

                    user_model.USERID = userguid;
                    user_model.USERNAME = uname;
                    user_model.PWD = pwd;
                    user_model.TEL = tel;
                    user_model.TEL_STATE = cnt_0;
                    user_model.USER_HEAD = user_head;
                    user_model.SEX = sex;
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



                    if (!user_bll.Add(user_model))
                    {
                        result = "no";
                    }
                    else
                    {
                        try
                        {
                            DbHelperSQL.ExecuteSql("update THINK_SNS_DB.dbo.hrenh_think_user set third_utype=" + third_logintype + ",third_openid='" + third_uopenid + "' where userid=" + userguid + " ");
                        }
                        catch (Exception ex)
                        {

                        }
                        CookieManage_HongRenHui.AddCookie("RED_HREN_USERID", userguid.ToString(), 7, false);
                        CookieManage_HongRenHui.AddCookie("RED_HREN_Third_Login", third_logintype.ToString(), 7, false);
                    }
                }
                else
                {
                    result = "noautho";
                }
            }
            else
            {
                #region  更新用户信息

                ////大头像
                //int imgpre = 0;
                //if (third_logintype < 3)
                //{
                //    imgpre = 1;
                //}
                //user_head = Save_Img_Http.Save_Img_WebRequest(user_head, "jpg", imgpre);

                //user_head = user_head.Replace("large_", "").Replace(ImgHelper.GetCofigShowUrl(), "");


                //try
                //{
                //    DbHelperSQL.ExecuteSql("update THINK_SNS_DB.dbo.hrenh_think_user set username='" + uname + "',USER_HEAD='" + user_head + "' where third_openid='" + third_uopenid + "' ");
                //}
                //catch (Exception ex)
                //{

                //}

                #endregion

                CookieManage_HongRenHui.AddCookie("RED_HREN_USERID", DbHelperSQL.GetSingle("select top 1 userid from THINK_SNS_DB.dbo.hrenh_think_user where third_openid='" + third_uopenid + "' ").ToString(), 7, false);
                CookieManage_HongRenHui.AddCookie("RED_HREN_Third_Login", third_logintype.ToString(), 7, false);
            }

            if (result == "ok")
            {
                ////登录途径，0：红人汇PC端，1：红人汇wap端，2：微吧PC端，3：微吧wap端
                //switch (login_type)
                //{
                //    case 0:
                //        result = "http://www.ypindapei.com";
                //        break;
                //    case 1:
                //        result = "http://m.ypindapei.com";
                //        break;
                //    case 2:
                //        result = "http://weiba.ypindapei.com";
                //        break;
                //    case 3:
                //        result = "http://w.ypindapei.com";
                //        break;
                //    default:
                //        result = "http://www.ypindapei.com";
                //        break;
                //}
                //result = "/UserLogin/Login_Third.aspx";

                string urls = CookieManage_HongRenHui.GetCookieValue("RED_HREN_Login_Urls");

                if (urls != "" && urls != "0")
                {
                    result = urls;
                }
                else
                {
                    result = CheckIsLogin_HongRenHui.Get_User_CenterUrl_HongRenHui();
                }

                //登录微吧
                //Get_Http_Url_Content.GetPostContent("http://weiba.ypindapei.com/login?uid=" + NewXzc.Common.DEncrypt.DESEncrypt.Encrypt(CookieManage_HongRenHui.GetCookieValue("RED_HREN_USERID")));

            }

            return result;
        }

        #endregion

    }
}