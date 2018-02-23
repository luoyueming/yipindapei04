using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace NewXzc.Web.Common
{
    public class TelHelper
    {
        /// <summary>
        /// 正则验证当前手机号格式是否正确
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public static bool IsTel(string tel)
        {
            bool f = true;

            string result = Regex.Match(tel, "(13[0-9]{9})|(15[0-9]{9})|(170[0-9]{8})|(176[0-9]{8})|(177[0-9]{8})|(178[0-9]{8})|(18[0-9]{9})").Value;

            if (result.Length !=11)
            {
                f = false;
            }

            return f;
        }
    }
}