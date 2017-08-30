using System;
using System.Collections.Generic;

namespace UMengPush.Notification.Core
{
    public abstract class UmengNotification
    {
        // This JSONObject is used for constructing the whole request string.
        protected Newtonsoft.Json.Linq.JObject rootJson = new Newtonsoft.Json.Linq.JObject();

        // The app master secret
        protected String appMasterSecret;

        // Keys can be set in the root level
        protected static HashSet<String> ROOT_KEYS = new HashSet<String>(new String[]{
            "appkey", "timestamp", "type", "device_tokens", "alias", "alias_type", "file_id",
            "filter", "production_mode", "feedback", "description", "thirdparty_id"});

        // Keys can be set in the policy level
        protected static HashSet<String> POLICY_KEYS = new HashSet<String>(new String[]{
            "start_time", "expire_time", "max_send_num"
        });

        // Set predefined keys in the rootJson, for extra keys(Android) or customized keys(IOS) please 
        // refer to corresponding methods in the subclass.
        public abstract bool setPredefinedKeyValue(String key, Object value);

        public void setAppMasterSecret(String secret)
        {
            appMasterSecret = secret;
        }

        public String getPostBody()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(rootJson);
        }

        public String getAppMasterSecret()
        {
            return appMasterSecret;
        }

        public void setProductionMode(Boolean prod)
        {
            setPredefinedKeyValue("production_mode", prod.ToString().ToLower());
        }

        ///正式模式
        public void setProductionMode()
        {
            setProductionMode(true);
        }

        ///测试模式
        public void setTestMode()
        {
            setProductionMode(false);
        }

        ///发送消息描述，建议填写。
        public void setDescription(String description)
        {
            setPredefinedKeyValue("description", description);
        }

        ///定时发送时间，若不填写表示立即发送。格式: "YYYY-MM-DD hh:mm:ss"。
        public void setStartTime(String startTime)
        {
            setPredefinedKeyValue("start_time", startTime);
        }
        ///消息过期时间,格式: "YYYY-MM-DD hh:mm:ss"。
        public void setExpireTime(String expireTime)
        {
            setPredefinedKeyValue("expire_time", expireTime);
        }
        ///发送限速，每秒发送的最大条数。
        public void setMaxSendNum(int num)
        {
            setPredefinedKeyValue("max_send_num", num);
        }

    }
}
