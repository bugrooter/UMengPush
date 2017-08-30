using System;
using UMengPush.Notification.Core;

namespace UMengPush.Notification.Android
{
    public class AndroidUnicast : AndroidNotification
    {
        public AndroidUnicast(String appkey, String appMasterSecret)
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
