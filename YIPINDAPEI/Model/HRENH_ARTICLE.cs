using System;
namespace NewXzc.Model
{
    /// <summary>
    /// HRENH_ARTICLE:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class HRENH_ARTICLE
    {
        public HRENH_ARTICLE()
        { }
        #region Model
        private int _id;
        private string _title;
        private int? _isimg;
        private string _img_url;
        private string _contents;
        private int? _types;
        private int _types_pid = 0;
        private int _subject_id = 0;
        private int _zp_zx_id = -1;
        private int? _read_cnt;
        private int? _falsh_read_cnt = 0;
        private int? _ishot;
        private int? _isend;
        private DateTime? _edittime;
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
        /// 标题
        /// </summary>
        public string TITLE
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 是否需要配图，0：否，1：是
        /// </summary>
        public int? ISIMG
        {
            set { _isimg = value; }
            get { return _isimg; }
        }
        /// <summary>
        /// 配图地址
        /// </summary>
        public string IMG_URL
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string CONTENTS
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 类型，对应类型列表中的ID
        /// </summary>
        public int? TYPES
        {
            set { _types = value; }
            get { return _types; }
        }
        /// <summary>
        /// 存储当前类型的父级ID
        /// </summary>
        public int TYPES_PID
        {
            set { _types_pid = value; }
            get { return _types_pid; }
        }
        /// <summary>
        /// 专题ID
        /// </summary>
        public int SUBJECT_ID
        {
            set { _subject_id = value; }
            get { return _subject_id; }
        }
        /// <summary>
        /// 当前文章属于作品，还是资讯，0：作品，1：资讯
        /// </summary>
        public int ZP_ZX_ID
        {
            set { _zp_zx_id = value; }
            get { return _zp_zx_id; }
        }
        /// <summary>
        /// 被阅读的次数
        /// </summary>
        public int? READ_CNT
        {
            set { _read_cnt = value; }
            get { return _read_cnt; }
        }
        /// <summary>
        /// 虚拟的阅读数量（在2000-5000之间随机出来的）
        /// </summary>
        public int? FALSH_READ_CNT
        {
            set { _falsh_read_cnt = value; }
            get { return _falsh_read_cnt; }
        }
        /// <summary>
        /// 是否是热门文章，0：否，1：是
        /// </summary>
        public int? ISHOT
        {
            set { _ishot = value; }
            get { return _ishot; }
        }
        /// <summary>
        /// 当前文章是否已结束，0：未结束，1：已结束
        /// </summary>
        public int? ISEND
        {
            set { _isend = value; }
            get { return _isend; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? EDITTIME
        {
            set { _edittime = value; }
            get { return _edittime; }
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

