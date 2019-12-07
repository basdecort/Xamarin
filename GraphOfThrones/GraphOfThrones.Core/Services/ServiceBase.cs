using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GraphOfThrones.Core.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAll();
    }

    public class ServiceBase<T>
    {
        internal T CachedResult;
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
                if (CachedResult == null)
                {
                    var json = await _httpClient.GetStringAsync(_jsonUrl);

                    CachedResult = JsonConvert.DeserializeObject<T>(json);
                }
                return CachedResult;
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }
    }
}
