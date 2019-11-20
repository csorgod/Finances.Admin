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
        private readonly string ContentType = "application/json";

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
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

            if (Storage.Keys().Count > 0)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Storage.Get<string>("UserJwt"));
        }

        public async Task<JsonDefaultResponse<T>> Get<T>(string endpoint)
        {
            string response = await client.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<JsonDefaultResponse<T>>(response);
        }

        public async Task<JsonDefaultResponse<T>> Delete<T>(string endpoint)
        {
            HttpResponseMessage message = await client.DeleteAsync(endpoint);
            string response = message.Content.ReadAsStringAsync().Result;
            return JsonTransformer.Deserialize<JsonDefaultResponse<T>>(response);
        }

        public async Task<JsonDefaultResponse<T>> Post<T>(string endpoint, object content)
        {
            StringContent stringContent = new StringContent(JsonTransformer.Serialize(content), Encoding.UTF8, ContentType);
            HttpResponseMessage request = await client.PostAsync(endpoint, stringContent);
            return ReadAndReturn<T>(request);
        }

        public async Task<JsonDefaultResponse<T>> Put<T>(string endpoint, object content)
        {
            StringContent stringContent = new StringContent(JsonTransformer.Serialize(content), Encoding.UTF8, ContentType);
            HttpResponseMessage request = await client.PutAsync(endpoint, stringContent);
            return ReadAndReturn<T>(request);
        }

        public async Task<JsonDefaultResponse<T>> Patch<T>(string endpoint, object content)
        {
            StringContent stringContent = new StringContent(JsonTransformer.Serialize(content), Encoding.UTF8, ContentType);
            HttpRequestMessage requestContent = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint)
            {
                Content = stringContent
            };
            HttpResponseMessage request = await client.SendAsync(requestContent);
            return ReadAndReturn<T>(request);
        }
        //TODO: Tratar response corretamente. O retorno em caso de falha na validação do Fluent Validation é:
        /*
         {"errors":{"TaxNumber":["'Tax Number' deve ter no máximo 11 caracteres. Você digitou 14 caracteres."]},"title":"One or more validation errors occurred.","status":400,"traceId":"80000023-0003-dc00-b63f-84710c7967bb"}
        */
        private JsonDefaultResponse<T> ReadAndReturn<T>(HttpResponseMessage request)
        {
            try
            {
                var response = request.Content.ReadAsStringAsync().Result;
                if(string.IsNullOrEmpty(response))
                    return new JsonDefaultResponse<T>
                    {
                        Success = false,
                        Error = "Houve um erro ao receber uma resposta do servidor. Por favor, tente novamente mais tarde"
                    };

                return JsonTransformer.Deserialize<JsonDefaultResponse<T>>(response);
            }
            catch (Exception ex)
            {
                return new JsonDefaultResponse<T>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }
    }
}
