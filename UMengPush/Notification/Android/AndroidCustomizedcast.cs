using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMengPush.Notification.Core;

namespace UMengPush.Notification.Android
{
    public class AndroidCustomizedcast : AndroidNotification
    {
        public AndroidCustomizedcast(String appkey, String appMasterSecret)
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
            this.setPredefinedKeyValue("type", "customizedcast");
        }

        public void setAlias(String alias, String aliasType)
        {
            setPredefinedKeyValue("alias", alias);
            setPredefinedKeyValue("alias_type", aliasType);
        }

        public void setFileId(String fileId, String aliasType)
        {
            setPredefinedKeyValue("file_id", fileId);
            setPredefinedKeyValue("alias_type", aliasType);
        }

    }
}
