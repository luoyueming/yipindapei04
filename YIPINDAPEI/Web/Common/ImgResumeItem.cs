using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewXzc.Web.Common
{
    /// <summary>
    /// 图形化简历
    /// </summary>
    public class ImgResumeItem
    {
        //开始日期
        public DateTime BeginDate { get; set; }
        //开始年
        public int BeginYear { get; set; }
        //开始月        
        public int BeginMonth { get; set; }
        //结束日期
        public DateTime EndDate { get; set; }
        //结束年
        public int EndYear { get; set; }
        //结束月
        public int EndMonth { get; set; }
        //时间长度月单位
        public int MonthCount { get; set; }
        //名称
        public string ShowName { get; set; }
        //名称2
        public string ShowName2 { get; set; }
        //类型
        public int Type { get; set; }
    }
}