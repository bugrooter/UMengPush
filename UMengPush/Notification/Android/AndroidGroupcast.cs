using System;
using Newtonsoft.Json.Linq;
using UMengPush.Notification.Core;

namespace UMengPush.Notification.Android
{
    public class AndroidGroupcast : AndroidNotification
    {
        public AndroidGroupcast(String appkey, String appMasterSecret)
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
            this.setPredefinedKeyValue("type", "groupcast");
        }

        public void setFilter(JObject filter)
        {
            setPredefinedKeyValue("filter", filter);
        }
    }
}
