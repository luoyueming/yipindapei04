using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewXzc.Common.Model
{
    [Serializable]
    public class ImgInfo
    {
        public ImgInfo()
        { }

        #region(_savePath:图片保存路径,_thumbPath:缩略图保存路径,_imgWidth:图片宽度,_imgHeight:图片高度,_status:0,原图,1,缩略图)
        private string _savePath;

        public string SavePath
        {
            get { return _savePath; }
            set { _savePath = value; }
        }

        private string _thumbPath;

        public string ThumbPath
        {
            get { return _thumbPath; }
            set { _thumbPath = value; }
        }

        private int _imgWidth;

        public int ImgWidth
        {
            get { return _imgWidth; }
            set { _imgWidth = value; }
        }
        private int _imgHeight;

        public int ImgHeight
        {
            get { return _imgHeight; }
            set { _imgHeight = value; }
        }
        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion

    }
}
