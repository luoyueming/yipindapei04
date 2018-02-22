using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Common;
using NewXzc.DBUtility;

namespace NewXzc.Web.Common
{
    public class Send_Short_Message
    {
        public static string Send_Message_Short(string msg, string phone, int type = 0)
        {

            string result = "ok";

            //禁止浏览器直接访问ajax页面
            if (HttpContext.Current.Request.UrlReferrer.Host != HttpContext.Current.Request.Url.Host)
            {
                HttpContext.Current.Response.Redirect("/404");
            }
            else
            {

                string send_type = ConfigHelper.GetConfigString("Short_Message");


                if (type == 1)
                {
                    send_type = "zhiqingwangluo";
                }

                //志晴网络
                if (send_type == "zhiqingwangluo")
                {
                    #region  志晴网络短信
                    ShortMessageService sms = new ShortMessageService();

                    result = sms.sendmessage(msg + "【X职 场】", phone);

                    result = result.Replace("</int>", "");
                    result = result.Substring(result.LastIndexOf(">") + 1);

                    if (result == "0")
                    {
                        result = "ok";
                    }
                    else
                    {
                        result = "1";
                    }

                    #endregion
                }
                else//华亿无线
                {
                    #region  华亿无线短信
                    //shortMessageWord = "您的验证码是：" + code + "。请不要把验证码泄露给其他人。";//shortMessageWord;

                    sms sms1 = new sms();

                    DateTime dtn = DateTime.Now;

                    int minute_one = 0;
                    int cnt = 0;
                    int cntip = 0;
                    //是否在做活动，0：否，1：是
                    int IsActive_Now = ConfigHelper.GetConfigInt("IsActive_Now");
                    //当天发送的总记录数
                    int send_total_cnt = 0;


                    #region  验证是否1分钟之内重复发送

                    try
                    {
                        minute_one = Convert.ToInt32(DbHelperSQL.GetSingle("select count(*) as cnt from HRENH_SEND_TEL_CODE where tel='" + phone + "' and datediff(ss,addtime,'" + dtn + "')<=60 and datediff(ss,addtime,'" + dtn + "')>=0").ToString());
                    }
                    catch (Exception ex)
                    {

                    }
                    #endregion

                    if (minute_one > 0)
                    {
                        result = "one";
                    }
                    else
                    {

                        //验证手机号格式是否正确
                        //(13[0-9]{9})|(15[0-9]{9})|(170[0-9]{8})|(176[0-9]{8})|(177[0-9]{8})|(178[0-9]{8})|(18[0-9]{9})
                        bool f = System.Text.RegularExpressions.Regex.IsMatch(phone, @"^(13[0-9]{9})|(15[0-9]{9})|(170[0-9]{8})|(176[0-9]{8})|(177[0-9]{8})|(178[0-9]{8})|(18[0-9]{9})");

                        if (!f)
                        {
                            result = "1";
                        }
                        else
                        {
                            #region  添加发送记录并验证是否已经发送超过5次
                            try
                            {
                                DbHelperSQL.ExecuteSql("insert into HRENH_SEND_TEL_CODE(tel,num,addtime,ip) values('" + phone + "',1,'" + dtn + "','" + RequestHelper.GetIP() + "')");
                            }
                            catch (Exception ex)
                            {

                            }


                            try
                            {
                                cnt = Convert.ToInt32(DbHelperSQL.GetSingle("select (isnull((select count(*) from THINK_SNS_DB.DBO.HRENH_SEND_TEL_CODE where tel='" + phone + "'),0)+isnull((select count(*) from Reds_Parliament.DBO.HRENH_SEND_TEL_CODE where tel='" + phone + "'),0)) as cnt").ToString());
                            }
                            catch (Exception ex)
                            {
                                cnt = 0;
                            }


                            try
                            {
                                send_total_cnt = Convert.ToInt32(DbHelperSQL.GetSingle("select (isnull((select count(*) from THINK_SNS_DB.DBO.HRENH_SEND_TEL_CODE),0)+isnull((select count(*) from Reds_Parliament.DBO.HRENH_SEND_TEL_CODE),0)) as cnt").ToString());
                            }
                            catch (Exception ex)
                            {

                            }


                            #region  同一个IP每天只能发送10次记录
                            try
                            {
                                cntip = Convert.ToInt32(DbHelperSQL.GetSingle("select (isnull((select count(*) from THINK_SNS_DB.DBO.HRENH_SEND_TEL_CODE where ip='" + RequestHelper.GetIP() + "'),0)+isnull((select count(*) from Reds_Parliament.DBO.HRENH_SEND_TEL_CODE where ip='" + RequestHelper.GetIP() + "'),0)) as cnt").ToString());
                            }
                            catch (Exception ex)
                            {

                            }

                            if (cntip <= 10)
                            {
                                #region  添加发送记录并验证是否已经发送超过5次
                                if (cnt > 5)
                                {
                                    result = "5";
                                }
                                else
                                {
                                    #region  发送验证码

                                    if (IsActive_Now == 0)
                                    {
                                        if (send_total_cnt <= 1000)
                                        {

                                            SubmitResult SubmitResult1 = sms1.Submit("", "", phone, msg);

                                            result = SubmitResult1.code.ToString();

                                            string result_msg = SubmitResult1.msg;

                                            if (result == "2")
                                            {
                                                result = "ok";
                                            }
                                            else if (result_msg.IndexOf("5") >= 0)
                                            {
                                                result = "5";
                                            }
                                            else
                                            {
                                                result = "1";
                                            }
                                        }
                                        else
                                        {

                                            #region  当天发送的短信的条数大于1000则发送通知给孙传、子龙和我

                                            try
                                            {
                                                if (send_total_cnt == 1001)
                                                {

                                                    string more_100 = "衣品搭配系统提示今日：衣品搭配发送短信数已超过1000条，请即时查看。";

                                                    //孙伟
                                                    SubmitResult SubmitResult1000 = sms1.Submit("", "", "18600863778", more_100);

                                                    //子龙
                                                    SubmitResult1000 = sms1.Submit("", "", "15831601607", more_100);

                                                    //我
                                                    SubmitResult1000 = sms1.Submit("", "", "13426021774", more_100);
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                            }

                                            #endregion

                                            result = "1";
                                        }
                                    }
                                    else
                                    {
                                        SubmitResult SubmitResult1 = sms1.Submit("", "", phone, msg);

                                        result = SubmitResult1.code.ToString();

                                        string result_msg = SubmitResult1.msg;

                                        if (result == "2")
                                        {
                                            result = "ok";
                                        }
                                        else if (result_msg.IndexOf("5") >= 0)
                                        {
                                            result = "5";
                                        }
                                        else
                                        {
                                            result = "1";
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                result = "10";
                            }
                            #endregion


                            #endregion


                            #region  添加发送记录，生成txt文档

                            string[] error_msg_arr = { "1分钟："+minute_one,"发送次数："+cnt,"同一个IP的发送次数："+cntip,"返回结果（result）："+result };

                            Message.WriteError(phone, "发送短信验证码，IP:" + RequestHelper.GetIP(), "register_PC", error_msg_arr);

                            #endregion




                        }

                    }


                    #endregion
                }

            }

            return result;

        }
    }
}