using System;
namespace NewXzc.Model
{
    /// <summary>
    /// HRENH_ARTICLE_REVIEW_REPLY:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class HRENH_ARTICLE_REVIEW_REPLY
    {
        public HRENH_ARTICLE_REVIEW_REPLY()
        { }
        #region Model
        private int _id;
        private int? _userid;
        private int? _reply_id;
        private int? _pub_userid;
        private int? _review_pid;
        private string _contents;
        private int? _islike;
        private int? _types;
        private int? _article_id;
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
        /// 被回复的评论的ID
        /// </summary>
        public int? REPLY_ID
        {
            set { _reply_id = value; }
            get { return _reply_id; }
        }
        /// <summary>
        /// 被评论或回复的用户的ID
        /// </summary>
        public int? PUB_USERID
        {
            set { _pub_userid = value; }
            get { return _pub_userid; }
        }
        /// <summary>
        /// 回复是属于哪个评论的，当前回复最顶级的评论ID
        /// </summary>
        public int? REVIEW_PID
        {
            set { _review_pid = value; }
            get { return _review_pid; }
        }
        /// <summary>
        /// 评价或回复的内容
        /// </summary>
        public string CONTENTS
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 是否喜欢该评论或回复，0：否，1：是
        /// </summary>
        public int? ISLIKE
        {
            set { _islike = value; }
            get { return _islike; }
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

