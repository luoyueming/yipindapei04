using System;
namespace NewXzc.Model
{
    /// <summary>
    /// HRENH_ARTICLE_TYPE:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class HRENH_ARTICLE_TYPE
    {
        public HRENH_ARTICLE_TYPE()
        { }
        #region Model
        private int _id;
        private string _typename;
        private int? _typeorder;
        private int _pid = 0;
        private int _state;
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
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        /// <summary>
        /// 类型序号
        /// </summary>
        public int? TypeOrder
        {
            set { _typeorder = value; }
            get { return _typeorder; }
        }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int PID
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 状态，0：未启用，1：已启用
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? Addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        #endregion Model

    }
}

