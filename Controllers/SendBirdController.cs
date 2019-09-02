using SendBird;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace App2.Controllers
{
    public class SendBirdController : Controller
    {
        
        // GET: SendBird
        public ActionResult Index()
        {
            string APP_ID = "75DC34C9-03AE-4573-A69A-63FD75B7300E";
            //SendBirdClient.Init(APP_ID);
            //var result = "";
            string USE_ID = "T_tin2";
            //string ACCESS_TOKEN = "c4a2132e3b709eb947cb00b7a1c4c3326d66853c";
            SendBirdClient.Init(APP_ID);
            SendBirdClient.ChannelHandler channelHandler = new SendBirdClient.ChannelHandler();

            channelHandler.OnMessageReceived = (BaseChannel baseChannel, BaseMessage baseMessage) => {
                //baseMessage.MessageId.ToString();
                // message received
            };
            SendBirdClient.AddChannelHandler("testchannel1", channelHandler);

            SendBirdClient.Connect(USE_ID, (User user, SendBirdException connectException) => {
                // get channel
                GroupChannel.GetChannel("sendbird_group_channel_137287723_07e33e8b58e8f3d71f7a7cccb829ccc3ffbfa96c",
                  (GroupChannel groupChannel, SendBirdException getChannelException) => {
                      // send message
                      //groupChannel.SendUserMessage("you welcome.",
                      //  (UserMessage userMessage, SendBirdException sendMessageException) => {
                      //      // message sent
                      //  });
                  });
            });
            return View();
        }

        //private static readonly HttpClient client = new HttpClient();
        public object Get()
        {          
            var request = (HttpWebRequest)WebRequest.Create("https://api-75DC34C9-03AE-4573-A69A-63FD75B7300E.sendbird.com/v3/users");
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["Api-Token"]= "ab806d7ddb5d0b849d558affe95e343a22d93ebb";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    user_id = "den",
                    nickname = "den",
                    profile_url = "https://developer.android.com/guide/practices/ui_guidelines/images/NB_Icon_Mask_Shapes_Ext_02.gif"
                });

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return  result;
            }            
        }

    }
}