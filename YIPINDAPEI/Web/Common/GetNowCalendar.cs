using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Common;

namespace NewXzc.Web.Common
{
    /**/
    /// <summary>
    /// 中国常用农历日期时间类
    /// zj53hao@qq.com   http://hi.csdn.net/zj53hao
    /// </summary>
    public class GetNowCalendar
    {


        private int year, month, dayOfMonth;
        private bool isLeap;
        public DateTime time;

        /**/
        /// <summary>
        /// 获取当前日期的农历年
        /// </summary>
        public int Year
        {
            get { return year; }
        }

        /**/
        /// <summary>
        /// 获取当前日期的农历月份
        /// </summary>
        public int Month
        {
            get { return month; }
        }

        /**/
        /// <summary>
        /// 获取当前日期的农历月中天数
        /// </summary>
        public int DayOfMonth
        {
            get { return dayOfMonth; }
        }

        /**/
        /// <summary>
        /// 获取当前日期是否为闰月中的日期
        /// </summary>
        public bool IsLeap
        {
            get { return isLeap; }
        }

        System.Globalization.ChineseLunisolarCalendar cc;
        /**/
        /// <summary>
        /// 返回指定公历日期的阴历时间
        /// </summary>
        /// <param name="time"></param>
        public GetNowCalendar(DateTime time)
        {
            cc = new System.Globalization.ChineseLunisolarCalendar();

            if (time > cc.MaxSupportedDateTime || time < cc.MinSupportedDateTime)
                throw new Exception("参数日期时间不在支持的范围内,支持范围：" + cc.MinSupportedDateTime.ToShortDateString() + "到" + cc.MaxSupportedDateTime.ToShortDateString());
            year = cc.GetYear(time);
            month = cc.GetMonth(time);
            dayOfMonth = cc.GetDayOfMonth(time);
            isLeap = cc.IsLeapMonth(year, month);
            if (isLeap) month -= 1;
            this.time = time;

        }

        /**/
        /// <summary>
        /// 返回当前日前的农历日期。
        /// </summary>
        public static GetNowCalendar Now
        {
            get { return new GetNowCalendar(DateTime.Now); }
        }

        /**/
        /// <summary>
        /// 返回指定农历年，月，日，是否为闰月的农历日期时间
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="DayOfMonth"></param>
        /// <param name="IsLeap"></param>
        public GetNowCalendar(int Year, int Month, int DayOfMonth, bool IsLeap)
        {
            if (Year >= cc.MaxSupportedDateTime.Year || Year <= cc.MinSupportedDateTime.Year)
                throw new Exception("参数年份时间不在支持的范围内,支持范围：" + cc.MinSupportedDateTime.ToShortDateString() + "到" + cc.MaxSupportedDateTime.ToShortDateString());

            if (Month < 1 || Month > 12)
                throw new Exception("月份必须在1~12范围");
            cc = new System.Globalization.ChineseLunisolarCalendar();

            if (cc.GetLeapMonth(Year) != Month && IsLeap)
                throw new Exception("指定的月份不是当年的闰月");
            if (cc.GetDaysInMonth(Year, IsLeap ? Month + 1 : Month) < DayOfMonth || DayOfMonth < 1)
                throw new Exception("指定的月中的天数不在当前月天数有效范围");
            year = Year;
            month = Month;
            dayOfMonth = DayOfMonth;
            isLeap = IsLeap;
            time = DateTime.Now;
        }

        /**/
        /// <summary>
        /// 获取当前农历日期的公历时间
        /// </summary>
        public DateTime ToDateTime()
        {
            return cc.ToDateTime(year, isLeap ? month + 1 : month, dayOfMonth, time.Hour, time.Minute, time.Second, time.Millisecond);
        }

        /**/
        /// <summary>
        /// 获取指定农历时间对应的公历时间
        /// </summary>
        /// <param name="CnTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(GetNowCalendar CnTime)
        {
            return CnTime.ToDateTime();
        }

        /**/
        /// <summary>
        /// 获取指定公历时间转换为农历时间
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        public static GetNowCalendar ToGetNowCalendar(DateTime Time)
        {
            return new GetNowCalendar(Time);
        }


        #region  加载当前日期及农历日期
        /// <summary>
        /// 获取所传日期是星期几
        /// </summary>
        /// <param name="dt">所传日期</param>
        /// <returns></returns>
        public static string GetWeekDay(DateTime dt)
        {
            GetNowCalendar g = GetNowCalendar.Now;

            string[] cnDay ={ "", "初一", "初二", "初三", "初四", "初五", "初六", "初七"
                , "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五", "十六"
                , "十七", "十八", "十九", "二十", "廿一", "廿二", "廿三", "廿四", "廿五"
                , "廿六", "廿七", "廿八", "廿九", "三十" };

            string[] cnMonth ={ "", "正", "二", "三", "四", "五", "六", "七"
                , "八", "九", "十", "十一", "十二"};


            string calendar = TimeParser.ReturnCurTime(DateTime.Now.ToString(), 2) + "，" + TimeParser.GetWeekDay(DateTime.Now) + "，农历" + g.Year + "年" + cnMonth[g.Month-1].ToString() + "月" + cnDay[g.DayOfMonth].ToString();// + "日";

            

            return calendar;
        }
        #endregion

    }
    /**/
    /// <summary>
}