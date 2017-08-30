using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UMengPush.Notification.Utils
{
    public class CommonHelper
    {
        public static Int64 DateTimeToUnix(DateTime date)
        {
            DateTime DateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            return Convert.ToInt32((date - DateStart).TotalSeconds);
        }

        public static string GetMD5(string source)
        {
            return GetMD5(source, "");
        }

        public static string GetMD5(string source, string saltcode)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(source + saltcode);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="posturl">The posturl.</param>
        /// <param name="postData">The post data.</param>
        /// <returns>System.String.</returns>
        public static string HttpPost(string posturl, string postData)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(posturl);
                request.Method = "POST";
                request.Timeout = 10000;
                request.ContentType = "application/json";
                request.ContentLength = Encoding.UTF8.GetByteCount(postData);
                byte[] data = Encoding.UTF8.GetBytes(postData);
                Stream myRequestStream = request.GetRequestStream();
                myRequestStream.Write(data, 0, data.Length);
                myRequestStream.Flush();
                myRequestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}