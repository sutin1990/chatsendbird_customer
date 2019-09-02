using App2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace App2.Controllers
{
    public class UserSendbirdController : Controller
    {
        
        public object FindUser(string userid)
        {
            if (userid == null || userid == "")
            {
                string msgerror = new JavaScriptSerializer().Serialize(new
                {
                    code = 400201,
                    error = true,
                    message = "User not found."
                });
                return msgerror;
            }
            else
            {
                var URI = "https://api-75DC34C9-03AE-4573-A69A-63FD75B7300E.sendbird.com/v3/users/" + userid;
                //HttpClient httpClient = new HttpClient();
                //HttpRequestMessage requestMessage = new HttpRequestMessage
                //   {
                //    Method = HttpMethod.Get,
                //    RequestUri = new Uri(URI),
                //    Headers = {
                //        { HttpRequestHeader.ContentType.ToString(), "application/json" },
                //        {"Api-Token", "ab806d7ddb5d0b849d558affe95e343a22d93ebb" }
                //        }
                //};                
                //var response = httpClient.SendAsync(requestMessage).Result;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Api-Token", "ab806d7ddb5d0b849d558affe95e343a22d93ebb");

                //string message = JSONSerializer.Serialize(jobRequest);
                //message = message.Insert(1, "\"@type\": \"job\",");
                //byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
                //var content = new ByteArrayContent(messageBytes);
                //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = client.GetAsync(URI).Result;
                var resultfinduser = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                   var createmetadata = CreateMetadataUser(userid);
                    var resultjson = new []
                    {
                        new {finduser = resultfinduser,
                        metadata = createmetadata,
                        }
                    };

                    return resultfinduser;
                }
                else
                {
                    string msgerror = new JavaScriptSerializer().Serialize(new
                    {
                        code = 400201,
                        error = true,
                        message = "User not found."
                    });
                    return msgerror;
                }
               
            }            
            
        }
        
        public object CreateMetadataUser(string userid)
        {
            try
            {
                if (userid == null || userid == "")
            {
                string msgerror = new JavaScriptSerializer().Serialize(new
                {
                    code = 400201,
                    error = true,
                    message = "User not found."
                });
                return msgerror;
            }
            else
            {
                    var URI = "https://api-75DC34C9-03AE-4573-A69A-63FD75B7300E.sendbird.com/v3/users/" + userid + "/metadata";
                    HttpClient httpClient = new HttpClient();                   

                    var strcontent = "{\"metadata\":{\"userrole\":\"user\"}}";

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Api-Token", "ab806d7ddb5d0b849d558affe95e343a22d93ebb");


                    //var multipartContent = new MultipartFormDataContent();
                    //multipartContent.Add(
                    //        new StringContent(JsonConvert.SerializeObject(strcontent), Encoding.UTF8, "application/json"),
                    //        "multipartcontent"
                    //        );
                    var httpContent = new StringContent(JsonConvert.SerializeObject(strcontent), Encoding.UTF8, "application/json");

                    var response = client.PutAsync(new Uri(URI), new StringContent(strcontent, Encoding.UTF8, "application/json")).Result;
                    var result = response.Content.ReadAsStringAsync().Result;

                    return result;
                    //======================================================


                    //var URI = "https://api-75DC34C9-03AE-4573-A69A-63FD75B7300E.sendbird.com/v3/users/" + userid + "/metadata";
                    //HttpClient httpClient = new HttpClient();
                    //var data_meta = new
                    //{
                    //    metadata = new[] {
                    //            new {userrole = "user" , venuename = ""}
                    //    }
                    //};

                    //string data = new JavaScriptSerializer().Serialize(new
                    //{
                    //    metadata = new[] { new { userrole = "user", venuename = "user" } }
                    //});


                    //HttpRequestMessage requestMessage = new HttpRequestMessage
                    //{
                    //    Method = HttpMethod.Post,
                    //    RequestUri = new Uri(URI),
                    //    Headers = {
                    //    { HttpRequestHeader.ContentType.ToString(), "application/json" },
                    //    {"Api-Token", "ab806d7ddb5d0b849d558affe95e343a22d93ebb" }
                    //    },
                    //    // Content = new StringContent("{metadata:{\"userrole\":\"user\"}}", Encoding.UTF8, "application/json")
                    //    Content = new StringContent(data_meta.ToString(), Encoding.UTF8, "application/json")
                    //};
                    //var response = httpClient.SendAsync(requestMessage).Result;

                    //var result = response.Content.ReadAsStringAsync().Result;
                    //return result;


                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return msg;
            }
        }
    }
}
