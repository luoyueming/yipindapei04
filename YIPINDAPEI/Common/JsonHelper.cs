using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Json;

namespace NewXzc.Common
{
    #region json序列化
    /// <summary>
    /// json帮助
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            MemoryStream ms = new MemoryStream();

            ser.WriteObject(ms, t);

            string jsonString = Encoding.UTF8.GetString(ms.ToArray());

            ms.Close();

            //替换Json的Date字符串

            string p = @"\\/Date\((\d+)\+\d+\)\\/";

            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);

            Regex reg = new Regex(p);

            jsonString = reg.Replace(jsonString, matchEvaluator);

            return jsonString;

        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {

            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式

           
            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";

            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);

            Regex reg = new Regex(p);

            jsonString = reg.Replace(jsonString, matchEvaluator);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            T obj = (T)ser.ReadObject(ms);
            ms.Close();

            return obj;






        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {

            string result = string.Empty;

            DateTime dt = new DateTime(1970, 1, 1);

            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));

            dt = dt.ToLocalTime();

            result = dt.ToString("yyyy-MM-dd HH:mm:ss");

            return result;

        }


        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {

            string result = string.Empty;

            DateTime dt = DateTime.Parse(m.Groups[0].Value);

            dt = dt.ToUniversalTime();

            TimeSpan ts = dt - DateTime.Parse("1970-01-01");

            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);

            return result;

        }
    }
    #endregion


    #region 实体
    public class JsonEntityArticleList
    {
        public string articleList { get; set; }
        public int articleCount { get; set; }
        public string pageHtml { get; set; }
    }
    /// <summary>
    /// 系统功能
    /// </summary>
    public class JsonSysFun
    {
        public string SysFunInfo { get; set; }
        public string PageHtml { get; set; }
    }
    /// <summary>
    /// 通用列表分页HTML
    /// </summary>
    public class JsonList
    {
        public String PageListHTML { get; set; }
        public String DataListHTML { get; set; }
    }

    /// <summary>
    /// 管理员管理
    /// </summary>
    public class JsonManageList
    {
        public String PageListHTML { get; set; }
        public String DataListHTML { get; set; }
        public int ListCount { get; set; }
    }

    /// <summary>
    /// 校园代理
    /// </summary>
    public class JsonSchoolAgentList
    {
        public String PageListHTML { get; set; }
        public String DataListHTML { get; set; }
        public String DataUserHTML { get; set; }
        public int ListCount { get; set; }
    }

    /// <summary>
    /// 处理结果
    /// </summary>
    public class JsonResult
    {
        public string result { get; set; }
    }

    /// <summary>
    /// 上传图片（图片名称，图片描述）
    /// </summary>
    public class JsonEntityPic
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        public string imgUrl { get; set; }
        /// <summary>
        /// 图片描述
        /// </summary>
        public string imgDiscribe { get; set; }

    }

    /// <summary>
    /// 上传图片（图片集标题，图片集描述）
    /// </summary>
    public class JsonEntityAblum
    {
        public string AblumTitle { get; set; }
        public string AblumDiscribe { get; set; }

    }


    /// <summary>
    /// 内容
    /// </summary>
    public class JsonEntityContent
    {
        public string msg { get; set; }
    }

    /// <summary>
    /// 文章发布
    /// </summary>
    public class JsonEntityPublishArticle
    {
        public string msg { get; set; }
        public int article_id { get; set; }
    }

    /// <summary>
    /// 订阅
    /// </summary>
    public class JsonEntitySubscribe
    {
        public string articleList { get; set; }
        public string typeList { get; set; }
        public string tagList { get; set; }
    }

    /// <summary>
    /// 操作记录，发布新职位
    /// </summary>
    public class JsonEntityOperateJob
    {
        public string job_title { get; set; }
        public string job_discribe { get; set; }
    }


    /// <summary>
    /// 禁言与注销
    /// </summary>
    public class JsonStopInfo
    {
        public string stopMessage { get; set; }
        public string stopTime { get; set; }
        public int isLogin { get; set; }
        public int isMyself { get; set; }
    }

    #endregion
}
