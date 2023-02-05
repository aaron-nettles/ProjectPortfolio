using System;
using System.IO;
using System.Net.Http;
using Lift.iOS;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(JsonLoader))]
namespace Lift.iOS
{
    public class JsonLoader : IJsonLoader
    {
        public string LoadJson()
        {
            var json = string.Empty;
            using (var httpClient = new HttpClient())
            {
                json = httpClient.GetStringAsync("https://lift-3795b-default-rtdb.firebaseio.com/").Result;
            }
            return json;
        }
    }
}



