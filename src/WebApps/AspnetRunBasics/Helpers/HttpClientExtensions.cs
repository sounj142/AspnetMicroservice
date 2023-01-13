using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

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
    }
}