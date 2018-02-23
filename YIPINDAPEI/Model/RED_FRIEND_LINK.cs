using System;
namespace NewXzc.Model
{
    /// <summary>
    /// RED_FRIEND_LINK:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RED_FRIEND_LINK
    {
        public RED_FRIEND_LINK()
        { }
        #region Model
        private int _id;
        private string _link_name;
        private string _link_url;
        private DateTime? _addtime;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 链接名称
        /// </summary>
        public string LINK_NAME
        {
            set { _link_name = value; }
            get { return _link_name; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LINK_URL
        {
            set { _link_url = value; }
            get { return _link_url; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? ADDTIME
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        #endregion Model

    }
}

