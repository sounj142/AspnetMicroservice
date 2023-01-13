using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;

namespace AspnetRunBasics.Helpers
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var result = await response.Content.ReadFromJsonAsync<T>();
            return result!;
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(
            this HttpClient client, string url, T data)
        {
            var jsonData = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                Application.Json);
            return await client.PostAsync(url, jsonData);
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(
            this HttpClient client, string url, T data)
        {
            var jsonData = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                Application.Json);
            return await client.PutAsync(url, jsonData);
        }
    }
}