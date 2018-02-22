using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewXzc.Web.Common.uhelper
{
    public class Return_HttpURL
    {
        /// <summary>
        /// 根据链接打开方式来获取A标签所对应的跳转链接，1：在本页面打，2：在新页面打开
        /// </summary>
        /// <param name="httpurl">A标签所对应的跳转链接</param>
        /// <param name="openstyle">链接打开方式，1：在本页面打，2：在新页面打开</param>
        /// <returns></returns>
        public static string Return_Url(string httpurl,int openstyle)
        {
            string result = "";
            if (openstyle == 1)
            {
                result = " href='" + httpurl + "' ";
            }
            else if (openstyle == 2)
            {
                result = " href='" + httpurl + "' target='_blank' ";
            }
            else
            {
                result = " href='javascript:;' ";
            }

            return result;
        }
    }
}