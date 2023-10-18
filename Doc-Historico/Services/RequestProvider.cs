using System;
using System.Net.Http.Headers;
using Doc_Historico.Interfaces;
using Doc_Historico.Models;
using Newtonsoft.Json;

namespace Doc_Historico.Services
{
    public class RequestProvider : IRequestProvider
    {
        public RequestProvider()
        {
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) 
            {
                string content = await response.Content.ReadAsStringAsync();

                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}. Content: {content}");
            }
        }


        private readonly Lazy<HttpClient> _httpClient =
         new Lazy<HttpClient>(
         () =>
         {
             var httpClient = new HttpClient();
             httpClient.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));
             return httpClient;
         },
         LazyThreadSafetyMode.ExecutionAndPublication);


        private HttpClient GetOrCreateHttpClient()
        {
            var httpClient = _httpClient.Value;
            
                httpClient.DefaultRequestHeaders.Authorization = null;
            return httpClient;
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            HttpClient httpClient = GetOrCreateHttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = JsonConvert.DeserializeObject<TResult>(serialized);
            return result;
        }

        public async Task<bool> DeleteAsync(string uri)
        {
            HttpClient httpClient = GetOrCreateHttpClient();
            var result = await httpClient.DeleteAsync(uri);
            return result.IsSuccessStatusCode;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, object patient = null)
        {
            HttpClient httpClient = GetOrCreateHttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(patient));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync(uri, content);
            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = JsonConvert.DeserializeObject<TResult>(serialized);
            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, object patient)
        {
            HttpClient httpClient = GetOrCreateHttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(patient));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync(uri, content);
            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = JsonConvert.DeserializeObject<TResult>(serialized);
            return result;
        }
    }
 }

