using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PoleDisplayWeatherForecast
{
    public sealed class API
    {
        private static HttpClient client = null;
        #region Singleton
        private static API __instance;
        public static API Instance() => __instance ?? (__instance = new API());
        #endregion Singleton

        static API()
        {
            if (client == null)
            {
                string ApiBaseUrl = ConfigurationManager.AppSettings["APIEndpoint"].ToString();
                if (Uri.TryCreate(ApiBaseUrl, UriKind.RelativeOrAbsolute, out var BaseUri) == false)
                    throw new InvalidOperationException("`api_base` cannot be parsed as a valid endpoint");
                client = new HttpClient()
                {
                    BaseAddress = BaseUri
                };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("Machine", Environment.MachineName);
            }
        }

        public async Task<U> SendPostRequestAsync<T, U>(T objectToPost, string endpoint)
        {
            try
            {
                string serializedObject = JsonConvert.SerializeObject(objectToPost);
                StringContent content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<U>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Sending Request To Server. Please try again.\n{ex}");
                return JsonConvert.DeserializeObject<U>("{[]}");
            }
        }

        public async Task<U> SendAndGetResponseAsync<U>(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<U>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Sending Request To Server. Please try again.\n{ex}");
                return JsonConvert.DeserializeObject<U>("{[]}");
            }
        }
    }
}
