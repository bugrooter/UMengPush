using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMengPush.Notification.Android;
using UMengPush.Notification.IOS;
using static UMengPush.Notification.Core.AndroidNotification;

namespace UMengPush
{
    public class Push
    {
        private String appkey = null;
        private String appMasterSecret = null;
        private PushClient client = new PushClient();

        public Push(String key, String secret)
        {
            try
            {
                appkey = key;
                appMasterSecret = secret;
            }
            catch (Exception e)
            {

            }
        }

        #region Android
        public void sendAndroidBroadcast()
        {
            AndroidBroadcast broadcast = new AndroidBroadcast(appkey, appMasterSecret);
            broadcast.setTicker("Android broadcast ticker");
            broadcast.setTitle("中文的title");
            broadcast.setText("Android broadcast text");
            broadcast.setStartTime(null+"");
            broadcast.goAppAfterOpen();
            broadcast.setDisplayType(DisplayType.NOTIFICATION);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            broadcast.setProductionMode();
            // Set customized fields
            broadcast.setExtraField("test", "helloworld");
            client.send(broadcast);
        }

        public void sendAndroidUnicast()
        {
            AndroidUnicast unicast = new AndroidUnicast(appkey, appMasterSecret);
            // TODO Set your device token
            unicast.setDeviceToken("Au4-bW9SEEVf5P0jf7uCjcooyTJQIvIcL07zVak8FShU");
            unicast.setTicker("新版升级");
            unicast.setTitle("悠骑单车升级");
            unicast.setText("有新版本可下载");
            unicast.setStartTime("");
            //unicast.goAppAfterOpen();
            //unicast.setCustomField("aaaa");
            unicast.goUrlAfterOpen("http://qr27.cn/CvuDSO");
            unicast.setDisplayType(DisplayType.NOTIFICATION);
            //unicast.setStartTime();
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            unicast.setProductionMode();
            // Set customized fields
            //unicast.setExtraField("test", "helloworld");
            client.send(unicast);
        }

        public void sendAndroidGroupcast()
        {
            AndroidGroupcast groupcast = new AndroidGroupcast(appkey, appMasterSecret);
            /*  TODO
             *  Construct the filter condition:
             *  "where": 
             *	{
             *		"and": 
             *		[
             *			{"tag":"test"},
             *			{"tag":"Test"}
             *		]
             *	}
             */
            JObject filterJson = new JObject();
            JObject whereJson = new JObject();
            JArray tagArray = new JArray();
            JObject testTag = new JObject();
            JObject TestTag = new JObject();
            testTag.Add("tag", "test");
            TestTag.Add("tag", "Test");
            tagArray.Add(testTag);
            tagArray.Add(TestTag);
            whereJson.Add("and", tagArray);
            filterJson.Add("where", whereJson);
            Console.WriteLine(filterJson);

            groupcast.setFilter(filterJson);
            groupcast.setTicker("Android groupcast ticker");
            groupcast.setTitle("中文的title");
            groupcast.setText("Android groupcast text");
            groupcast.goAppAfterOpen();
            groupcast.setDisplayType(DisplayType.NOTIFICATION);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            groupcast.setProductionMode();
            client.send(groupcast);
        }

        public void sendAndroidCustomizedcast()
        {
            AndroidCustomizedcast customizedcast = new AndroidCustomizedcast(appkey, appMasterSecret);
            // TODO Set your alias here, and use comma to split them if there are multiple alias.
            // And if you have many alias, you can also upload a file containing these alias, then 
            // use file_id to send customized notification.
            customizedcast.setAlias("alias", "alias_type");
            customizedcast.setTicker("Android customizedcast ticker");
            customizedcast.setTitle("中文的title");
            customizedcast.setText("Android customizedcast text");
            customizedcast.goAppAfterOpen();
            customizedcast.setDisplayType(DisplayType.NOTIFICATION);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            customizedcast.setProductionMode();
            client.send(customizedcast);
        }

        public void sendAndroidCustomizedcastFile()
        {
            AndroidCustomizedcast customizedcast = new AndroidCustomizedcast(appkey, appMasterSecret);
            // TODO Set your alias here, and use comma to split them if there are multiple alias.
            // And if you have many alias, you can also upload a file containing these alias, then 
            // use file_id to send customized notification.
            String fileId = client.uploadContents(appkey, appMasterSecret, "aa" + "\n" + "bb" + "\n" + "alias");
            customizedcast.setFileId(fileId, "alias_type");
            customizedcast.setTicker("Android customizedcast ticker");
            customizedcast.setTitle("中文的title");
            customizedcast.setText("Android customizedcast text");
            customizedcast.goAppAfterOpen();
            customizedcast.setDisplayType(DisplayType.NOTIFICATION);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            customizedcast.setProductionMode();
            client.send(customizedcast);
        }

        public void sendAndroidFilecast()
        {
            AndroidFilecast filecast = new AndroidFilecast(appkey, appMasterSecret);
            // TODO upload your device tokens, and use '\n' to split them if there are multiple tokens 
            String fileId = client.uploadContents(appkey, appMasterSecret, "aa" + "\n" + "bb");
            filecast.setFileId(fileId);
            filecast.setTicker("Android filecast ticker");
            filecast.setTitle("中文的title");
            filecast.setText("Android filecast text");
            filecast.goAppAfterOpen();
            filecast.setDisplayType(DisplayType.NOTIFICATION);
            client.send(filecast);
        }

        #endregion

        #region IOS
        public void sendIOSBroadcast()
        {
            IOSBroadcast broadcast = new IOSBroadcast(appkey, appMasterSecret);

            broadcast.setAlert("IOS 广播测试");
            broadcast.setBadge(0);
            broadcast.setSound("default");
            // TODO set 'production_mode' to 'true' if your app is under production mode
            broadcast.setTestMode();
            // Set customized fields
            broadcast.setCustomizedField("test", "helloworld");
            client.send(broadcast);
        }

        public void sendIOSUnicast()
        {
            IOSUnicast unicast = new IOSUnicast(appkey, appMasterSecret);
            // TODO Set your device token
            unicast.setDeviceToken("xx");
            unicast.setAlert("IOS 单播测试");
            unicast.setBadge(0);
            unicast.setSound("default");
            // TODO set 'production_mode' to 'true' if your app is under production mode
            unicast.setTestMode();
            // Set customized fields
            unicast.setCustomizedField("test", "helloworld");
            client.send(unicast);
        }

        public void sendIOSGroupcast()
        {
            IOSGroupcast groupcast = new IOSGroupcast(appkey, appMasterSecret);
            /*  TODO
             *  Construct the filter condition:
             *  "where": 
             *	{
             *		"and": 
             *		[
             *			{"tag":"iostest"}
             *		]
             *	}
             */
            JObject filterJson = new JObject();
            JObject whereJson = new JObject();
            JArray tagArray = new JArray();
            JObject testTag = new JObject();
            testTag.Add("tag", "iostest");
            tagArray.Add(testTag);
            whereJson.Add("and", tagArray);
            filterJson.Add("where", whereJson);

            // Set filter condition into rootJson
            groupcast.setFilter(filterJson);
            groupcast.setAlert("IOS 组播测试");
            groupcast.setBadge(0);
            groupcast.setSound("default");
            // TODO set 'production_mode' to 'true' if your app is under production mode
            groupcast.setTestMode();
            client.send(groupcast);
        }

        public void sendIOSCustomizedcast()
        {
            IOSCustomizedcast customizedcast = new IOSCustomizedcast(appkey, appMasterSecret);
            // TODO Set your alias and alias_type here, and use comma to split them if there are multiple alias.
            // And if you have many alias, you can also upload a file containing these alias, then 
            // use file_id to send customized notification.
            customizedcast.setAlias("alias", "alias_type");
            customizedcast.setAlert("IOS 个性化测试");
            customizedcast.setBadge(0);
            customizedcast.setSound("default");
            // TODO set 'production_mode' to 'true' if your app is under production mode
            customizedcast.setTestMode();
            client.send(customizedcast);
        }

        public void sendIOSFilecast()
        {
            IOSFilecast filecast = new IOSFilecast(appkey, appMasterSecret);
            // TODO upload your device tokens, and use '\n' to split them if there are multiple tokens 
            String fileId = client.uploadContents(appkey, appMasterSecret, "aa" + "\n" + "bb");
            filecast.setFileId(fileId);
            filecast.setAlert("IOS 文件播测试");
            filecast.setBadge(0);
            filecast.setSound("default");
            // TODO set 'production_mode' to 'true' if your app is under production mode
            filecast.setTestMode();
            client.send(filecast);
        }
        #endregion
    }
}
