using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Postcodes.API.Testing.Helpers
{
    public class Executors
    {

        private Environment _environmentConfig;

        public Executors()
        {
            _environmentConfig = new Environment();
        }


        public async Task<string> GETPostcodeAsync(string path)
        {
            string postcodeData = string.Empty;

            using (var client = _environmentConfig.GetClient())
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    postcodeData = await response.Content.ReadAsStringAsync();
                }
            }

            return postcodeData;
        }

        public async Task<string> POSTPostcodeAsync(string path, string jsonBodyData)
        {
            string responsedata = string.Empty;

            var content = new StringContent(jsonBodyData, Encoding.UTF8, "application/json");

            using (var client = _environmentConfig.GetClient())
            {
                HttpResponseMessage response = await client.PostAsync(path, content);
                if (response.IsSuccessStatusCode)
                {
                    responsedata = await response.Content.ReadAsStringAsync();
                }
            }

            return responsedata;
        }

    }
}
