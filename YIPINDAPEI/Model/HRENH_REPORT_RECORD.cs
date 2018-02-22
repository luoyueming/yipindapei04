using System;
namespace NewXzc.Model
{
    /// <summary>
    /// HRENH_REPORT_RECORD:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class HRENH_REPORT_RECORD
    {
        public HRENH_REPORT_RECORD()
        { }
        #region Model
        private int _id;
        private int? _userid;
        private int? _review_reply_id;
        private string _report_type;
        private string _report_content;
        private int? _types;
        private int? _article_id;
        private int? _check_state;
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
        /// 操作人ID
        /// </summary>
        public int? USERID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 被举报的评论或回复的ID
        /// </summary>
        public int? REVIEW_REPLY_ID
        {
            set { _review_reply_id = value; }
            get { return _review_reply_id; }
        }
        /// <summary>
        /// 被举报的原因，以","间隔
        /// </summary>
        public string REPORT_TYPE
        {
            set { _report_type = value; }
            get { return _report_type; }
        }
        /// <summary>
        /// 举报的内容
        /// </summary>
        public string REPORT_CONTENT
        {
            set { _report_content = value; }
            get { return _report_content; }
        }
        /// <summary>
        /// 评论或回复的类型，对应类型列表中的ID
        /// </summary>
        public int? TYPES
        {
            set { _types = value; }
            get { return _types; }
        }
        /// <summary>
        /// 被评论或回复的文章的ID
        /// </summary>
        public int? ARTICLE_ID
        {
            set { _article_id = value; }
            get { return _article_id; }
        }
        /// <summary>
        /// 审核状态，0：审核中，1：审核失败，2：审核通过
        /// </summary>
        public int? CHECK_STATE
        {
            set { _check_state = value; }
            get { return _check_state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ADDTIME
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        #endregion Model

    }
}

