using DigiteerTechnical2.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DigiteerTechnical2.Services
{
    public class RainfallService : IRainfallService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _baseUrl;

        public RainfallService(HttpClient httpClient, IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.DefaultConnection;
        }

        public async Task<Rainfall?> GetRainfallsAsync(string id, int count)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/flood-monitoring/id/stations/{id}/readings?_sorted&_limit={count}");
            var jsonResult = await response.Content.ReadAsStringAsync();
            var rainfallResult = JsonConvert.DeserializeObject<Rainfall>(jsonResult);

            return rainfallResult;
        }
    }
}
