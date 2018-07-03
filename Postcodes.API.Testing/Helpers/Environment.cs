using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Postcodes.API.Testing.Helpers
{
    public class Environment
    {

        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://postcodes.io/"); //could put this in app.config file
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);

            return client;
        }

    }
}
