using System;
using System.Threading.Tasks;
using HttpClientNative = System.Net.Http.HttpClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Hanssens.Net;
using Newtonsoft.Json;
using Finances.Common.Data;
using Microsoft.Extensions.Options;

namespace Finances.Common.Helpers
{
    public class HttpClient
    {
        private string baseUrl;
        private LocalStorage Storage;
        private AppSettings AppSettings;
        private readonly HttpClientNative client;

        public HttpClient(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
            baseUrl = AppSettings.ApiUrl;
            client = new HttpClientNative();
            Storage = LocalStorageSingleton.Instance;

            ConfigureClient();
        }

        public void ConfigureClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (Storage.Keys().Count > 0)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Storage.Get<string>("UserJwt"));
        }

        public async Task<JsonDefaultResponse<T>> Get<T>(string endpoint)
        {
            string response = await client.GetStringAsync(endpoint);

            var a = JsonConvert.DeserializeObject<JsonDefaultResponse<T>>(response);
            return a;
        }

        public async Task<JsonDefaultResponse<T>> Delete<T>(string endpoint)
        {
            HttpResponseMessage message = await client.DeleteAsync(endpoint);
            string response = message.Content.ReadAsStringAsync().Result;
            return JsonTransformer.Deserialize<JsonDefaultResponse<T>>(response);
        }

        public async Task<JsonDefaultResponse<T>> Post<T>(string endpoint, object content)
        {
            StringContent stringContent = new StringContent(JsonTransformer.Serialize(content), Encoding.UTF8, "application/json");
            HttpResponseMessage request = await client.PostAsync(endpoint, stringContent);
            string response = request.Content.ReadAsStringAsync().Result;
            return JsonTransformer.Deserialize<JsonDefaultResponse<T>>(response);
        }

        public async Task<JsonDefaultResponse<T>> Put<T>(string endpoint, object content)
        {
            StringContent stringContent = new StringContent(JsonTransformer.Serialize(content), Encoding.UTF8, "application/json");
            HttpResponseMessage request = await client.PutAsync(endpoint, stringContent);
            string response = request.Content.ReadAsStringAsync().Result;
            return JsonTransformer.Deserialize<JsonDefaultResponse<T>>(response);
        }

        public async Task<JsonDefaultResponse<T>> Patch<T>(string endpoint, object content)
        {
            StringContent stringContent = new StringContent(JsonTransformer.Serialize(content), Encoding.UTF8, "application/json");
            HttpRequestMessage requestContent = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint)
            {
                Content = stringContent
            };
            HttpResponseMessage request = await client.SendAsync(requestContent);
            string response = request.Content.ReadAsStringAsync().Result;
            return JsonTransformer.Deserialize<JsonDefaultResponse<T>>(response);
        }
    }
}
