using System;
namespace NewXzc.Model
{
    /// <summary>
    /// RED_RECOMMEND:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RED_RECOMMEND
    {
        public RED_RECOMMEND()
        { }
        #region Model
        private int _id;
        private int? _pageid;
        private int? _cid;
        private int? _sort;
        private string _url;
        private string _v1;
        private string _v2;
        private string _v3;
        private string _v4 = "";
        private DateTime? _addtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 页面编号
        /// </summary>
        public int? PageId
        {
            set { _pageid = value; }
            get { return _pageid; }
        }
        /// <summary>
        /// 页面内容模块编号
        /// </summary>
        public int? Cid
        {
            set { _cid = value; }
            get { return _cid; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string V1
        {
            set { _v1 = value; }
            get { return _v1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string V2
        {
            set { _v2 = value; }
            get { return _v2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string V3
        {
            set { _v3 = value; }
            get { return _v3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string v4
        {
            set { _v4 = value; }
            get { return _v4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        #endregion Model

    }
}

