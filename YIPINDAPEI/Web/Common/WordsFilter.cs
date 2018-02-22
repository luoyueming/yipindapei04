using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Collections;


namespace Maticsoft.Web.Common
{
    //过滤敏感关键字 
    public class WordsFilter
    {

        /// <summary>
        /// 查看是否包含关键字
        /// </summary>
        /// <param name="txtFile">txt文本文件服务器路径</param>
        /// <param name="word">需要验证的词</param>
        /// <returns>有TRUE  没有FALSE</returns>
        public static bool CheckWords(string txtFileServerPath,string word)
        {
            bool retVal = false;

            if (File.Exists(txtFileServerPath))
            {
                StreamReader sReader = null;

                try
                {
                    string nextLine = string.Empty;

                    sReader = File.OpenText(txtFileServerPath);
                    //读取下一行
                    while ((nextLine = sReader.ReadLine()) != null)
                    {
                        //判断当前行和word是否相等
                        if (nextLine.Trim() == word.Trim())
                        {
                            retVal = true;
                            break;
                        }
                    }

                    sReader.Close();
                }
                catch (Exception ex)
                {
                    if(sReader!=null)
                        sReader.Close();
                    string[] paramers={""};
                    NewXzc.Web.Common.Message.WriteError(ex.Message, "CheckWords", "Web/Common/WordsFilter.cs", paramers);  
                }
            }

            return retVal;
        }

        /// <summary>
        /// 查看是否包含关键字
        /// </summary>
        /// <param name="txtFile">txt文本文件服务器路径</param>
        /// <param name="word">需要验证的词</param>
        /// <returns>有TRUE  没有FALSE</returns>
        public static bool CheckFilter(string txtFileServerPath,string content)
        {
            string[] badwords = null;
            if (File.Exists(txtFileServerPath))
            {
                badwords = File.ReadAllLines(txtFileServerPath); 
            }
            
            Dictionary<string, object> hash = new Dictionary<string, object>();
            BitArray firstCharCheck = new BitArray(char.MaxValue);
            BitArray allCharCheck = new BitArray(char.MaxValue);      
            int maxLength = 0;
            StringBuilder str = new StringBuilder();

            foreach (string word in badwords)
            {
                if (!hash.ContainsKey(word))
                {
                    hash.Add(word, null);
                    maxLength = Math.Max(maxLength, word.Length);
                    firstCharCheck[word[0]] = true;

                    foreach (char c in word)
                    {
                        allCharCheck[c] = true;
                    }
                }
            }

            int index = 0;
            while (index < content.Length)
            {
                if (!firstCharCheck[content[index]])
                {
                    while (index < content.Length - 1 && !firstCharCheck[content[++index]]) ;
                }

                for (int j = 1; j <= Math.Min(maxLength, content.Length - index); j++)
                {
                    if (!allCharCheck[content[index + j - 1]])
                    {
                        break;
                    }

                    str.Append(content.Substring(index, j));
                    

                    if (hash.ContainsKey(str.ToString()))
                    {
                        return true;
                    }
                    str.Clear();
                }

                index++;
            }

            return false;
        }
    }
}