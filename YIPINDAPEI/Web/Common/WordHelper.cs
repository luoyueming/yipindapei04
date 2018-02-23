using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;

namespace NewXzc.Web.Common
{
    public class WordHelper
    {
        private Word.Document wDoc = null;
        private Word.Application wApp = null;
        public Word.Document Document
        {
            get { return wDoc; }
            set { wDoc = value; }
        }

        public Word.Application Application
        {
            get { return wApp; }
            set { wApp = value; }
        }

        #region 从模板创建新的Word文档
        /// <summary>  
        /// 从模板创建新的Word文档  
        /// </summary>  
        /// <param name="templateName">模板文件名</param>  
        /// <returns></returns>  
        public bool CreateNewWordDocument(string templateName)
        {
            try
            {
                return CreateNewWordDocument(templateName, ref wDoc, ref wApp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 从模板创建新的Word文档,并且返回对象Document,Application
        /// <summary>  
        /// 从模板创建新的Word文档，  
        /// </summary>  
        /// <param name="templateName">模板文件名</param>  
        /// <param name="wDoc">返回的Word.Document对象</param>  
        /// <param name="WApp">返回的Word.Application对象</param>  
        /// <returns></returns>  
        public static bool CreateNewWordDocument(string templateName, ref Word.Document wDoc, ref  Word.Application WApp)
        {
            Word.Document thisDocument = null;
            Word.Application thisApplication = new Word.Application();
            thisApplication.Visible = false;
            thisApplication.Caption = "";
            thisApplication.Options.CheckSpellingAsYouType = false;
            thisApplication.Options.CheckGrammarAsYouType = false;

            Object Template = templateName;
            Object NewTemplate = false;
            Object DocumentType = Word.WdNewDocumentType.wdNewBlankDocument;
            Object Visible = true;

            try
            {
                Word.Document wordDoc = thisApplication.Documents.Add(ref Template, ref NewTemplate, ref DocumentType, ref Visible);

                thisDocument = wordDoc;
                wDoc = wordDoc;
                WApp = thisApplication;

                //wordDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape; //设置页面为纵向
                //wordDoc.PageSetup.PageHeight = WApp.CentimetersToPoints(21F);//高度
                //wordDoc.PageSetup.PageWidth = WApp.CentimetersToPoints(29.7F);//宽度

                wordDoc.PageSetup.TopMargin = 20; //设置上边距
                wordDoc.PageSetup.BottomMargin = 20;//设置下边距
                wordDoc.PageSetup.LeftMargin = 20;//设置左边距
                wordDoc.PageSetup.RightMargin = 20;//设置右边距

                return true;
            }
            catch (Exception ex)
            {
                string err = string.Format("创建Word文档出错，错误原因：{0}", ex.Message);
                throw new Exception(err, ex);
            }
        }
        #endregion

        #region 文档另存为其他文件名
        /// <summary>  
        /// 文档另存为其他文件名  
        /// </summary>  
        /// <param name="fileName">文件名</param>  
        /// <param name="wDoc">Document对象</param>  
        public bool SaveAs(string fileName)
        {
            try
            {
                return SaveAs(fileName, wDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 文档另存为其他文件名
        /// <summary>  
        /// 文档另存为其他文件名  
        /// </summary>  
        /// <param name="fileName">文件名</param>  
        /// <param name="wDoc">Document对象</param>  
        public static bool SaveAs(string fileName, Word.Document wDoc)
        {
            Object FileName = fileName; // 文档的名称。默认值是当前文件夹名和文件名。如果文档在以前没有保存过，则使用默认名称（例如，Doc1.doc）。如果已经存在具有指定文件名的文档，则会在不先提示用户的情况下改写文档。  
            Object FileFormat = Word.WdSaveFormat.wdFormatDocument; // 文档的保存格式。可以是任何 WdSaveFormat 值。要以另一种格式保存文档，请为 SaveFormat 属性指定适当的值。  
            Object LockComments = false; // 如果为 true，则锁定文档以进行注释。默认值为 false。  
            Object Password = System.Type.Missing; // 用来打开文档的密码字符串。（请参见下面的备注。）  
            Object AddToRecentFiles = false; // 如果为 true，则将该文档添加到“文件”菜单上最近使用的文件列表中。默认值为 true。  
            Object WritePassword = System.Type.Missing; // 用来保存对文件所做更改的密码字符串。（请参见下面的备注。）  
            Object ReadOnlyRecommended = false; // 如果为 true，则让 Microsoft Office Word 在打开文档时建议只读状态。默认值为 false。  
            Object EmbedTrueTypeFonts = false; //如果为 true，则将 TrueType 字体随文档一起保存。如果省略的话，则 EmbedTrueTypeFonts 参数假定 EmbedTrueTypeFonts 属性的值。  
            Object SaveNativePictureFormat = true; // 如果图形是从另一个平台（例如，Macintosh）导入的，则 true 表示仅保存导入图形的 Windows 版本。  
            Object SaveFormsData = false; // 如果为 true，则将用户在窗体中输入的数据另存为数据记录。  
            Object SaveAsAOCELetter = false; // 如果文档附加了邮件程序，则 true 表示会将文档另存为 AOCE 信函（邮件程序会进行保存）。  
            Object Encoding = System.Type.Missing; // MsoEncoding。要用于另存为编码文本文件的文档的代码页或字符集。默认值是系统代码页。  
            Object InsertLineBreaks = true; // 如果文档另存为文本文件，则 true 表示在每行文本末尾插入分行符。  
            Object AllowSubstitutions = false; //如果文档另存为文本文件，则 true 允许 Word 将某些符号替换为外观与之类似的文本。例如，将版权符号显示为 (c)。默认值为 false。  
            Object LineEnding = Word.WdLineEndingType.wdCRLF;// Word 在另存为文本文件的文档中标记分行符和换段符。可以是任何 WdLineEndingType 值。  
            Object AddBiDiMarks = true;//如果为 true，则向输出文件添加控制字符，以便保留原始文档中文本的双向布局。  
            try
            {
                wDoc.SaveAs(ref FileName, ref FileFormat, ref LockComments, ref Password, ref AddToRecentFiles, ref WritePassword
                        , ref ReadOnlyRecommended, ref EmbedTrueTypeFonts, ref SaveNativePictureFormat
                        , ref SaveFormsData, ref SaveAsAOCELetter, ref Encoding, ref InsertLineBreaks, ref AllowSubstitutions
                        , ref LineEnding, ref AddBiDiMarks);
                return true;
            }
            catch (Exception ex)
            {
                string err = string.Format("另存文件出错，错误原因：{0}", ex.Message);
                throw new Exception(err, ex);
            }
        }
        #endregion

        #region 关闭文档
        /// <summary>  
        /// 关闭文档  
        /// </summary>  
        public void Close()
        {
            Close(wDoc, wApp);
            wDoc = null;
            wApp = null;
        }
        #endregion

        #region 关闭文档
        /// <summary>  
        /// 关闭文档  
        /// </summary>  
        /// <param name="wDoc">Document对象</param>  
        /// <param name="WApp">Application对象</param>  
        public static void Close(Word.Document wDoc, Word.Application WApp)
        {
            Object SaveChanges = Word.WdSaveOptions.wdSaveChanges;// 指定文档的保存操作。可以是下列 WdSaveOptions 值之一：wdDoNotSaveChanges、wdPromptToSaveChanges 或 wdSaveChanges。  
            Object OriginalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;// 指定文档的保存格式。可以是下列 WdOriginalFormat 值之一：wdOriginalDocumentFormat、wdPromptUser 或 wdWordDocument。  
            Object RouteDocument = false;// 如果为 true，则将文档传送给下一个收件人。如果没有为文档附加传送名单，则忽略此参数。  
            try
            {
                if (wDoc != null) wDoc.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
                if (WApp != null) WApp.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 填充书签
        /// <summary>  
        /// 填充书签  
        /// </summary>  
        /// <param name="bookmark">书签</param>  
        /// <param name="value">值</param>  
        public void Replace(string bookmark, string value)
        {
            try
            {
                object bkObj = bookmark;
                if (wApp.ActiveDocument.Bookmarks.Exists(bookmark) == true)
                {
                    wApp.ActiveDocument.Bookmarks.get_Item(ref bkObj).Select();
                }
                else return;
                wApp.Selection.TypeText(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 找到指定标签位置
        /// <summary>
        /// 找到指定标签位置
        /// </summary>
        /// <param name="txt"></param>
        public void FindPosition(string txt)
        {
            if (wApp.ActiveDocument.Bookmarks.Exists(txt))
            {
                wApp.ActiveDocument.Bookmarks.get_Item(txt).Select();
            }
        }
        #endregion

        #region 在已经找到标签位置处插入文本
        /// <summary>
        /// 在已经找到标签位置处插入文本
        /// </summary>
        /// <param name="txt">插入文本内容</param>
        /// <param name="txtColor">字体颜色</param>
        /// <param name="txtSize">字体大小</param>
        /// <param name="txtBold">0不加粗1加粗</param>
        /// <param name="txtName">字体样式</param>
        /// <param name="lineSpace">行间距</param>
        public void FindPositionText(string txt, Word.WdColor txtColor, float txtSize, int txtBold, string txtName, float lineSpace)
        {
            wApp.Selection.ParagraphFormat.LineSpacing = lineSpace; //行间距
            wApp.Selection.Font.Color = txtColor;//字体颜色
            wApp.Selection.Font.Size = txtSize;//字体大小
            wApp.Selection.Font.Bold = txtBold;//0不加粗1加粗
            wApp.Selection.Font.Name = txtName;//字体样式
            wApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft; //对齐方式
            wApp.Selection.TypeText(txt);   // 插入文本

        }
        #endregion

        #region 在已经找到标签位置处换行
        /// <summary>
        /// 在已经找到标签位置处换行
        /// </summary>
        public void FindPositionBr()
        {
            try
            {
                wApp.Selection.TypeParagraph();//插入段落
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 在已经找到标签位置处添加图片
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="imgPath">图片绝对路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="x">X轴</param>
        /// <param name="y">Y轴</param>
        public void AddPic(string imgPath, float width, float height, float x, float y)
        {
            object LinkToFile = false;
            object SaveWithDocument = true;
            object Anchor = wApp.Selection.Range;
            wApp.ActiveDocument.InlineShapes.AddPicture(imgPath, ref LinkToFile, ref SaveWithDocument, ref Anchor);
            wApp.ActiveDocument.InlineShapes[1].Width = width;//图片宽度
            wApp.ActiveDocument.InlineShapes[1].Height = height;//图片高度
            //将图片设置为四周环绕型
            Shape s = wApp.ActiveDocument.InlineShapes[1].ConvertToShape();
            s.WrapFormat.Type = WdWrapType.wdWrapSquare;
            s.Top = y;
            s.Left = x;
        }
        #endregion

        #region 空格数量
        /// <summary>
        /// 空格数量
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string kongGe(int num)
        {
            string str = "";
            for (int i = 1; i <= num; i++)
            {
                str += " ";
            }
            return str;
        }
        #endregion

        #region 判断字符串的字符长度
        /// <summary>
        /// 判断字符串的字符长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int CharNum(string str)
        {
            int n = 0;
            foreach (char ch in str)
            {
                n += System.Text.Encoding.Default.GetByteCount(ch.ToString());
            }
            return n;
        }
        #endregion

        
    }
}