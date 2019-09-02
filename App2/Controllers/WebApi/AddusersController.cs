using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;


namespace App2.Controllers.WebApi
{
    public class AddusersController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
       // [System.Web.Http.HttpGet]
        public async System.Threading.Tasks.Task<object> AddusersAsync()
        {
            var values = new Dictionary<string, string>
                        {
                           { "user_id", "den" },
                           { "nickname", "den" },
                           { "profile_url", "https://developer.android.com/guide/practices/ui_guidelines/images/NB_Icon_Mask_Shapes_Ext_02.gif" }
                        };

            var content = new FormUrlEncodedContent(values);
            var header = content.Headers;
            header.Add("APi-Token", "ab806d7ddb5d0b849d558affe95e343a22d93ebb");
            var response = await client.PostAsync("https://api-75DC34C9-03AE-4573-A69A-63FD75B7300E.sendbird.com/v3/users", content);

            var responseString =  await response.Content.ReadAsStringAsync();
            return Json(responseString);
        }
    }
}
