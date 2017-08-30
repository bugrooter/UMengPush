using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMengPush.Notification.Core;

namespace UMengPush.Notification.IOS
{
    public class IOSUnicast : IOSNotification
    {
        public IOSUnicast(String appkey, String appMasterSecret)
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
            this.setPredefinedKeyValue("type", "unicast");
        }

        public void setDeviceToken(String token)
        {
            setPredefinedKeyValue("device_tokens", token);
        }
    }
}
