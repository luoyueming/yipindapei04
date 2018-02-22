using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewXzc.Web.Common
{
    public class PlaceHelper
    {
        /// <summary>
        /// 根据显示方式，0：“-”（北京-朝阳），1：“ ”（北京 朝阳），2：“,”（北京,朝阳），获取省、市、县的集合
        /// </summary>
        /// <param name="province">省</param>
        /// <param name="city">市</param>
        /// <param name="area">县</param>
        /// <param name="showtype">显示方式，0：“-”，1：“ ”，2：“,”</param>
        public static string GetPlace(string province, string city, string area, int showtype)
        {
            string place = "";

            string p = "";
            if (!string.IsNullOrEmpty(province))
            {
                p = province;
            }
            string c = "";
            if (!string.IsNullOrEmpty(city))
            {
                c = city;
            }
            string a = "";
            if (!string.IsNullOrEmpty(area))
            {
                a = area;
            }

            if (showtype == 0)
            {
                if (p != "")
                {
                    place = p;
                }
                if (c != "")
                {
                    place += "-" + c;
                }
                if (a != "")
                {
                    place += "-" + a;
                }
            }
            else if (showtype == 1)
            {
                if (p != "")
                {
                    place = p;
                }
                if (c != "")
                {
                    place += " " + c;
                }
                if (a != "")
                {
                    place += " " + a;
                }
            }
            else if (showtype == 2)
            {
                if (p != "")
                {
                    place = p;
                }
                if (c != "")
                {
                    place += "," + c;
                }
                if (a != "")
                {
                    place += "," + a;
                }
            }

            return place;

        }
    }
}