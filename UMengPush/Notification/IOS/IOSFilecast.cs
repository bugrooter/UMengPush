using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMengPush.Notification.Core;

namespace UMengPush.Notification.IOS
{
    public class IOSFilecast : IOSNotification
    {
        public IOSFilecast(String appkey, String appMasterSecret)
        {
            setAppMasterSecret(appMasterSecret);
            setPredefinedKeyValue("appkey", appkey);
            this.setPredefinedKeyValue("type", "filecast");
        }

        public void setFileId(String fileId)
        {
            setPredefinedKeyValue("file_id", fileId);
        }
    }
}
