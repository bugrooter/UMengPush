using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace UMengPush.Notification.Core
{
    public abstract class AndroidNotification : UmengNotification
    {
        // Keys can be set in the payload level
        protected static HashSet<String> PAYLOAD_KEYS = new HashSet<String>(new String[]{
            "display_type"});

        // Keys can be set in the body level
        protected static HashSet<String> BODY_KEYS = new HashSet<String>(new String[]{
            "ticker", "title", "text", "builder_id", "icon", "largeIcon", "img", "play_vibrate", "play_lights", "play_sound",
            "sound", "after_open", "url", "activity", "custom"});

        public enum DisplayType
        {
            NOTIFICATION,///通知:消息送达到用户设备后，由友盟SDK接管处理并在通知栏上显示通知内容。
            MESSAGE
        };///消息:消息送达到用户设备后，消息内容透传给应用自身进行解析处理。

        public string GetDisplayTypeValue(DisplayType type)
        {
            switch (type)
            {
                case DisplayType.NOTIFICATION:
                    return "notification";
                    break;
                case DisplayType.MESSAGE:
                    return "message";
                    break;
                default:
                    return "";
                    break;
            }
        }

        public enum AfterOpenAction
        {
            go_app,//打开应用
            go_url,//跳转到URL
            go_activity,//打开特定的activity
            go_custom//用户自定义内容。
        }
        // Set key/value in the rootJson, for the keys can be set please see ROOT_KEYS, PAYLOAD_KEYS, 
        // BODY_KEYS and POLICY_KEYS.
        public override bool setPredefinedKeyValue(String key, Object value)
        {
            if (ROOT_KEYS.Contains(key))
            {
                // This key should be in the root level
                rootJson.Add(new JProperty(key, value));
            }
            else if (PAYLOAD_KEYS.Contains(key))
            {
                // This key should be in the payload level
                JObject payloadJson = null;
                if (rootJson.Property("payload") != null)
                {
                    payloadJson = (JObject)rootJson.Property("payload").Value;
                }
                else
                {
                    payloadJson = new JObject();
                    rootJson.Add("payload", payloadJson);
                }
                //payloadJson.Add(key, (JToken)value);
                payloadJson.Add(new JProperty(key, value));
            }
            else if (BODY_KEYS.Contains(key))
            {
                // This key should be in the body level
                JObject bodyJson = null;
                JObject payloadJson = null;
                // 'body' is under 'payload', so build a payload if it doesn't exist
                if (rootJson.Property("payload") != null)
                {
                    payloadJson = (JObject)rootJson.Property("payload").Value;
                }
                else
                {
                    payloadJson = new JObject();
                    rootJson.Add("payload", payloadJson);
                }
                // Get body Newtonsoft.Json.JsonToken, generate one if not existed
                if (payloadJson.Property("body") != null)
                {
                    bodyJson = (JObject)payloadJson.Property("body").Value;
                }
                else
                {
                    bodyJson = new JObject();
                    payloadJson.Add("body", bodyJson);
                }
                //bodyJson.Add(key, (JToken)value);
                bodyJson.Add(new JProperty(key, value));
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
                //policyJson.Add(key, (JToken)value);
                policyJson.Add(new JProperty(key, value));
            }
            else
            {
                if (key == "payload" || key == "body" || key == "policy" || key == "extra")
                {
                    throw new Exception("You don't need to set value for " + key + " , just set values for the sub keys in it.");
                }
                else
                {
                    throw new Exception("Unknown key: " + key);
                }
            }
            return true;
        }

        // Set extra key/value for Android notification
        public bool setExtraField(String key, String value)
        {
            JObject payloadJson = null;
            JObject extraJson = null;
            if (rootJson.Property("payload") != null)
            {
                payloadJson = rootJson.Property("payload").Value as JObject;
            }
            else
            {
                payloadJson = new JObject();
                rootJson.Add("payload", payloadJson);
            }

            if (payloadJson.Property("extra") != null)
            {
                extraJson = payloadJson.Property("extra").Value as JObject;
            }
            else
            {
                extraJson = new JObject();
                payloadJson.Add("extra", extraJson);
            }
            extraJson.Add(key, value);
            //extraJson.Add(new JProperty(key, value));
            return true;
        }

        //
        public void setDisplayType(DisplayType d)
        {
            setPredefinedKeyValue("display_type", GetDisplayTypeValue(d));
        }
        ///通知栏提示文字
        public void setTicker(String ticker)
        {
            setPredefinedKeyValue("ticker", ticker);
        }
        ///通知标题
        public void setTitle(String title)
        {
            setPredefinedKeyValue("title", title);
        }
        ///通知文字描述
        public void setText(String text)
        {
            setPredefinedKeyValue("text", text);
        }
        ///用于标识该通知采用的样式。使用该参数时, 必须在SDK里面实现自定义通知栏样式。
        public void setBuilderId(int builder_id)
        {
            setPredefinedKeyValue("builder_id", builder_id);
        }
        ///状态栏图标ID, R.drawable.[smallIcon],如果没有, 默认使用应用图标。
        public void setIcon(String icon)
        {
            setPredefinedKeyValue("icon", icon);
        }
        ///通知栏拉开后左侧图标ID
        public void setLargeIcon(String largeIcon)
        {
            setPredefinedKeyValue("largeIcon", largeIcon);
        }
        ///通知栏大图标的URL链接。该字段的优先级大于largeIcon。该字段要求以http或者https开头。
        public void setImg(String img)
        {
            setPredefinedKeyValue("img", img);
        }
        ///收到通知是否震动,默认为"true"
        public void setPlayVibrate(Boolean play_vibrate)
        {
            setPredefinedKeyValue("play_vibrate", play_vibrate.ToString().ToLower());
        }
        ///收到通知是否闪灯,默认为"true"
        public void setPlayLights(Boolean play_lights)
        {
            setPredefinedKeyValue("play_lights", play_lights.ToString().ToLower());
        }
        ///收到通知是否发出声音,默认为"true"
        public void setPlaySound(Boolean play_sound)
        {
            setPredefinedKeyValue("play_sound", play_sound.ToString().ToLower());
        }
        ///通知声音，R.raw.[sound]. 如果该字段为空，采用SDK默认的声音
        public void setSound(String sound)
        {
            setPredefinedKeyValue("sound", sound);
        }
        ///收到通知后播放指定的声音文件
        public void setPlaySound(String sound)
        {
            setPlaySound(true);
            setSound(sound);
        }

        ///点击"通知"的后续行为，默认为打开app。
        public void goAppAfterOpen()
        {
            setAfterOpenAction(AfterOpenAction.go_app);
        }
        public void goUrlAfterOpen(String url)
        {
            setAfterOpenAction(AfterOpenAction.go_url);
            setUrl(url);
        }
        public void goActivityAfterOpen(String activity)
        {
            setAfterOpenAction(AfterOpenAction.go_activity);
            setActivity(activity);
        }
        public void goCustomAfterOpen(String custom)
        {
            setAfterOpenAction(AfterOpenAction.go_custom);
            setCustomField(custom);
        }
        public void goCustomAfterOpen(JObject custom)
        {
            setAfterOpenAction(AfterOpenAction.go_custom);
            setCustomField(custom);
        }

        ///点击"通知"的后续行为，默认为打开app。原始接口
        public void setAfterOpenAction(AfterOpenAction action)
        {
            setPredefinedKeyValue("after_open", action.ToString());
        }
        public void setUrl(String url)
        {
            setPredefinedKeyValue("url", url);
        }
        public void setActivity(String activity)
        {
            setPredefinedKeyValue("activity", activity);
        }
        ///can be a string of json
        public void setCustomField(String custom)
        {
            setPredefinedKeyValue("custom", custom);
        }
        public void setCustomField(JObject custom)
        {
            setPredefinedKeyValue("custom", custom);
        }

    }

}