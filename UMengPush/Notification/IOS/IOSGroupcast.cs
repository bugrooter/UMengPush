using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMengPush.Notification.Core;

namespace UMengPush.Notification.IOS
{
    public class IOSGroupcast : IOSNotification
    {
        public IOSGroupcast(String appkey, String appMasterSecret)
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
