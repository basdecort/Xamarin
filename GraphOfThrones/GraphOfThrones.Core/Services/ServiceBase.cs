using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GraphOfThrones.Core.Services
{
    public class ServiceBase<T>
    {
        private T _cachedResult;
        private readonly string _jsonUrl;
        private HttpClient _httpClient;

        public ServiceBase(string jsonUrl)
        {
            _jsonUrl = jsonUrl;
            _httpClient = new HttpClient();
        }

        public async Task<T> Get()
        {
            try
            {
                if (_cachedResult == null)
                {
                    var json = await _httpClient.GetStringAsync(_jsonUrl);

                    _cachedResult = JsonConvert.DeserializeObject<T>(json);
                }
                return _cachedResult;
            }
            catch(Exception ex)
            {
                var a = ex;
                return default(T);
            }
        }
    }
}
