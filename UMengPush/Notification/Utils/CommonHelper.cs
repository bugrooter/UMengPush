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
            //{"appkey":"59a40c13310c931fdb00007d","production_mode":"true","description":"评论提醒-UID:123","type":"broadcast","payload":{"display_type":"notification","body":{"title":"您的评论有回复","ticker":"您的评论有回复","text":"您的评论有回复咯。。。。。","custom":"comment-notify","after_open":"go_custom","play_vibrate":"false","play_sound":"false","play_lights":"false","builder_id":0}},"policy":{"expire_time":"2017-09-02 10:45:31"}}
            //{"appkey":"59a40c13310c931fdb00007d","type":"broadcast","payload":{"body":{"ticker":"Android broadcast ticker","title":"中文的title","text":"Android broadcast text","after_open":"go_app"},"display_type":"notification","extra":{"test":"helloworld"}},"production_mode":"True","timestamp":"1504061831"}
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