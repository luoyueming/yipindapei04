using System;
using System.Collections.Generic;
using System.Text;

namespace NewXzc.Common
{
    public class TimeParser
    {
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));           
        }

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >=1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

        #region 返回时间差，精确到分钟（用于计算2014-08-05早晨0点到6点）
        /// <summary>
        /// 计算两个时间的差，DateTime1是开始时间，DateTime2是结束时间
        /// </summary>
        /// <param name="DateTime1">是开始时间</param>
        /// <param name="DateTime2">是结束时间</param>
        /// <returns></returns>
        public static bool DateDiff_0_6(DateTime DateTime1, DateTime DateTime2)
        {
            bool f = false;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;

                if (ts.Days == 0)
                {
                    if (ts.Hours>=0 && ts.Hours<=6)
                    {
                        if (ts.Hours == 6)
                        {
                            if (ts.Minutes <= 0)
                            {
                                if (ts.Seconds >= 0)
                                {
                                    f = true;
                                }
                            }
                        }
                        else
                        {
                            f = true;
                        }
                    }
                }
            }
            catch
            { }
            return f;
        }
        #endregion

        /// <summary>
        /// 判断当前用户登录的时间是否在7天以内，用DateTime2-DateTime1
        /// </summary>
        /// <param name="DateTime1">当前登录时间</param>
        /// <param name="DateTime2">系统时间</param>
        /// <returns></returns>
        public static bool IsMoreSevenDays(DateTime DateTime1, DateTime DateTime2)
        {
            bool flag = false;
            try
            {
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 0 && ts.Days<=7)
                {
                    flag = true;
                }
            }
            catch
            { }
            return flag;
        }

        /// <summary>
        /// 返回指定格式的时间
        /// </summary>
        /// <param name="times">需要重新设置格式的时间变量</param>
        /// <param name="type">重新设置的的格式类型,0，如：2013-04-15；1，如：2013-04-15 12：08：30；2，如：2013年4月15日；3，如：2013年4月15日 12时8分30秒；4，如：2013.04.15；5，如：2013.04.15 12:00:00；6，如：2013.04.15 12:00；7，如：04月15日 12时8分；8，如：2013年4月15日 12时8分</param>
        /// <returns></returns>
        public static string ReturnCurTime(string times, int type)
        {
            string curtime = times;
            DateTime dt = Convert.ToDateTime(times);
            if (type == 0)//如：2013-04-15
            {
                curtime = dt.ToString("yyyy-MM-dd");
            }
            else if (type == 1)//如：2013-04-15 12：08：30
            {
                curtime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (type == 2)//如：2013年4月15日
            {
                curtime = dt.ToString("yyyy年MM月dd日");

                string[] trr = curtime.Split('年');
                string[] mrr = trr[1].Split('月');

                if (mrr[0].Length == 2)
                {
                    if (mrr[0].IndexOf("0") == 0)
                    {
                        mrr[0] = mrr[0].Substring(1);
                    }
                }

                if (mrr[1].Length >= 2)
                {
                    if (mrr[1].IndexOf("0") == 0)
                    {
                        mrr[1] = mrr[1].Substring(1);
                    }
                }

                curtime = trr[0] + "年" + mrr[0] + "月" + mrr[1];
            }
            else if (type == 3)//如：2013年4月15日 12时8分30秒
            {
                curtime = dt.ToString("yyyy年MM月dd日 HH时mm分ss秒");


                string[] trr = curtime.Split('年');
                string[] mrr = trr[1].Split('月');

                if (mrr[0].Length == 2)
                {
                    if (mrr[0].IndexOf("0") == 0)
                    {
                        mrr[0] = mrr[0].Substring(1);
                    }
                }


                if (mrr[1].Length >= 2)
                {
                    if (mrr[1].IndexOf("0") == 0)
                    {
                        mrr[1] = mrr[1].Substring(1);
                    }
                }

                curtime = trr[0] + "年" + mrr[0] + "月" + mrr[1];
            }
            else if (type == 4)//如：2013.04.15
            {
                curtime = dt.ToString("yyyy.MM.dd");
            }
            else if (type == 5)//如：2013.04.15 12:00:00
            {
                curtime = dt.ToString("yyyy.MM.dd HH:mm:ss");
            }
            else if (type == 6)//如：2013-04-15 12：08
            {
                curtime = dt.ToString("yyyy-MM-dd HH:mm");
            }
            else if (type == 7)//如：4月15日 12时8分
            {
                curtime = dt.ToString("MM月dd日 HH:mm");
            }
            else if (type == 8)//如：2013年4月15日 12时8分
            {
                curtime = dt.ToString("yyyy年MM月dd日 HH:mm");


                string[] trr = curtime.Split('年');
                string[] mrr = trr[1].Split('月');

                if (mrr[0].Length == 2)
                {
                    if (mrr[0].IndexOf("0") == 0)
                    {
                        mrr[0] = mrr[0].Substring(1);
                    }
                }


                if (mrr[1].Length >= 2)
                {
                    if (mrr[1].IndexOf("0") == 0)
                    {
                        mrr[1] = mrr[1].Substring(1);
                    }
                }

                curtime = trr[0] + "年" + mrr[0] + "月" + mrr[1];
            }
            return curtime;
        }

        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToShortDateString();
            }
            else if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else if (span.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }

        /// <summary>
        /// 至今
        /// </summary>
        /// <returns></returns>
        public static DateTime ReturnToDodayTime()
        {
            return Convert.ToDateTime("1900-01-01");
        }


        /// <summary>
        /// 获取所传日期是星期几
        /// </summary>
        /// <param name="dt">所传日期</param>
        /// <returns></returns>
        public static string GetWeekDay(DateTime dt)
        {
            string day = dt.DayOfWeek.ToString();

            string day_name = "";

            switch (day)
            {
                case "Monday":
                    day_name = "星期一";
                    break;
                case "Tuesday":
                    day_name = "星期二";
                    break;
                case "Wednesday":
                    day_name = "星期三";
                    break;
                case "Thursday":
                    day_name = "星期四";
                    break;
                case "Friday":
                    day_name = "星期五";
                    break;
                case "Saturday":
                    day_name = "星期六";
                    break;
                case "Sunday":
                    day_name = "星期日";
                    break;
            }

            return day_name;
        }
    }
}
