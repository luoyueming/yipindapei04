using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewXzc.Common
{
    public class ThirdLoginStyle
    {
        /// <summary>
        /// 获取当前第三登录的方式对应的AppKey，新浪（sina），人人（renren），腾讯（qq）
        /// </summary>
        /// <param name="type"></param>
        public static string ReturnAppKey(string type)
        {
            string appkey = "";
            if (type == "sina")//新浪
            {
                appkey = "2698985467";//"3704397640";
            }
            else if (type == "renren")//人人
            {
                appkey = "29e4e5cdddfe4bb2ab573a36964e2efe";
            }
            else//腾讯
            {
                appkey = "1101073928";
            }

            return appkey;
        }

        /// <summary>
        /// 获取当前第三登录的方式对应的，新浪（sina），人人（renren），腾讯（qq）
        /// </summary>
        /// <param name="type"></param>
        public static string ReturnAppSecret(string type)
        {
            string AppSecret = "";
            if (type == "sina")//新浪
            {
                AppSecret = "3fc144c173d1fb8f575198396b66d83d";//"765940dc746469e5ff1431a9d155cab1";
            }
            else if (type == "renren")//人人
            {
                AppSecret = "530359ef661d48a9b76263cca627d44c";
            }
            else//腾讯
            {
                AppSecret = "DlVTvoVm6XV02t89";
            }

            return AppSecret;
        }

        /// <summary>
        /// 获取当前第三登录的方式对应的回调地址，新浪（sina），人人（renren），腾讯（qq）
        /// </summary>
        /// <param name="type"></param>
        public static string GetReturnUrl(string type)
        {
            string return_url = "";
            if (type == "sina")//新浪
            {
                return_url = "http://www.xzhichang.com/ThirdLogin.aspx";
            }
            else if (type == "renren")//人人
            {
                return_url = "http://192.168.1.3/ThirdLogin_Renren.aspx";
            }
            else//腾讯
            {
                return_url = "http://192.168.1.3/ThirdLogin_QQ.aspx";
            }

            return return_url;
        }
    }
}
