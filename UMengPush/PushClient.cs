using System;
using UMengPush.Notification.Utils;
using UMengPush.Notification.Core;
using Newtonsoft.Json.Linq;

namespace UMengPush
{
    public class PushClient
    {
        // The user agent
        protected String USER_AGENT = "Mozilla/5.0";

        // The host
        protected static String host = "https://msgapi.umeng.com";//"https://msg.umeng.com";

        // The upload path
        protected static String uploadPath = "/upload";

        // The post path
        protected static String postPath = "/api/send";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgcode">成功时返回msg_id,失败时返回error_code</param>
        /// <returns></returns>
        public bool send(UmengNotification msg, out string msgcode)
        {
            msgcode = "";
            try
            {
                String timestamp = CommonHelper.DateTimeToUnix(DateTime.Now).ToString();
                msg.setPredefinedKeyValue("timestamp", timestamp);
                String url = host + postPath;
                String postBody = msg.getPostBody();
                String sign = CommonHelper.GetMD5(("POST" + url + postBody + msg.getAppMasterSecret()));
                url = url + "?sign=" + sign;
                string result = CommonHelper.HttpPost(url, postBody);
                JObject o = JObject.Parse(result);
                if (o["ret"].ToString().ToUpper() == "SUCCESS")
                {
                    msgcode = o["data"]["msg_id"] + "";
                    return true;
                }
                msgcode = o["data"]["error_code"] + "";
                //{\"ret\":\"SUCCESS\",\"data\":{\"msg_id\":\"uulpdgx150407757019600\"}}
                return false;
            }
            catch (Exception ex)
            {
                msgcode = ex.Message;
                return false;
            }
        }

        public string send(UmengNotification msg)
        {
            try
            {
                String timestamp = CommonHelper.DateTimeToUnix(DateTime.Now).ToString();
                msg.setPredefinedKeyValue("timestamp", timestamp);
                String url = host + postPath;
                String postBody = msg.getPostBody();
                String sign = CommonHelper.GetMD5("POST" + url + postBody + msg.getAppMasterSecret());
                url = url + "?sign=" + sign;
                string result = CommonHelper.HttpPost(url, postBody);
                //{"appkey":"59a40c13310c931fdb00007d","type":"broadcast","payload":{"aps":{"alert":"IOS 广播测试","badge":0,"sound":"default"},"test":"helloworld"},"production_mode":"false","timestamp":"1504150488"}
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Upload file with device_tokens to Umeng
        public String uploadContents(String appkey, String appMasterSecret, String contents)
        {
            // Construct the json string
            JObject uploadJson = new JObject();
            uploadJson.Add("appkey", appkey);
            String timestamp = CommonHelper.DateTimeToUnix(DateTime.Now).ToString();
            uploadJson.Add("timestamp", timestamp);
            uploadJson.Add("content", contents);
            // Construct the request
            String url = host + uploadPath;
            String postBody = Newtonsoft.Json.JsonConvert.SerializeObject(uploadJson);
            String sign = CommonHelper.GetMD5(("POST" + url + postBody + appMasterSecret));
            url = url + "?sign=" + sign;
            string result = CommonHelper.HttpPost(url, postBody);

            // Decode response string and get file_id from it
            JObject respJson = new JObject(result);
            String ret = respJson.Property("ret") + "";
            if (!ret.Equals("SUCCESS"))
            {
                throw new Exception("Failed to upload file");
            }
            JObject data = respJson.Property("data").Value as JObject;
            String fileId = data.Property("file_id").Value + "";
            // Set file_id into rootJson using setPredefinedKeyValue

            return fileId;
        }

    }
}
