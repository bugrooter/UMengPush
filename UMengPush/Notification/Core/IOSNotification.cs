using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMengPush.Notification.Core
{
    public class IOSNotification : UmengNotification
    {
        protected static HashSet<String> APS_KEYS = new HashSet<String>(new String[]{
            "alert", "badge", "sound", "content-available"
        });

        public override bool setPredefinedKeyValue(String key, Object value)
        {
            if (string.IsNullOrEmpty(value + ""))
            {
                return false;
            }
            if (ROOT_KEYS.Contains(key))
            {
                // This key should be in the root level
                //rootJson.put(key, value);
                rootJson.Add(new JProperty(key, value));
            }
            else if (APS_KEYS.Contains(key))
            {
                // This key should be in the aps level
                JObject apsJson = null;
                JObject payloadJson = null;
                if (rootJson.Property("payload") != null)
                {
                    payloadJson = rootJson.Property("payload").Value as JObject;
                }
                else
                {
                    payloadJson = new JObject();
                    rootJson.Add("payload", payloadJson);
                }
                if (payloadJson.Property("aps") != null)
                {
                    apsJson = payloadJson.Property("aps").Value as JObject;
                }
                else
                {
                    apsJson = new JObject();
                    payloadJson.Add("aps", apsJson);
                }
                //apsJson.put(key, value);
                apsJson.Add(new JProperty(key, value));
            }
            else if (POLICY_KEYS.Contains(key))
            {
                // This key should be in the body level
                JObject policyJson = null;
                if (rootJson.Property("policy") != null)
                {
                    policyJson = rootJson.Property("policy").Value as JObject;
                }
                else
                {
                    policyJson = new JObject();
                    rootJson.Add("policy", policyJson);
                }
                //policyJson.put(key, value);
                policyJson.Add(new JProperty(key, value));
            }
            else
            {
                if (key == "payload" || key == "aps" || key == "policy")
                {
                    throw new Exception("You don't need to set value for " + key + " , just set values for the sub keys in it.");
                }
                else
                {
                    throw new Exception("Unknownd key: " + key);
                }
            }

            return true;
        }
        // Set customized key/value for IOS notification
        public bool setCustomizedField(String key, String value)
        {
            //rootJson.put(key, value);
            JObject payloadJson = null;
            if (rootJson.Property("payload") != null)
            {
                payloadJson = rootJson.Property("payload").Value as JObject;
            }
            else
            {
                payloadJson = new JObject();
                rootJson.Add("payload", payloadJson);
            }
            payloadJson.Add(key, value);
            return true;
        }

        public void setAlert(String token)
        {
            setPredefinedKeyValue("alert", token);
        }

        public void setBadge(int badge)
        {
            setPredefinedKeyValue("badge", badge);
        }

        public void setSound(String sound)
        {
            setPredefinedKeyValue("sound", sound);
        }

        public void setContentAvailable(int contentAvailable)
        {
            setPredefinedKeyValue("content-available", contentAvailable);
        }
    }
}
