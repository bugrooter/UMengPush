using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMengPush.Notification.Core;

namespace UMengPush.Notification.Android
{
    public class AndroidBroadcast: AndroidNotification
    {
        public AndroidBroadcast(String appkey, String appMasterSecret)
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
			this.setPredefinedKeyValue("type", "broadcast");
        }
    }
}
