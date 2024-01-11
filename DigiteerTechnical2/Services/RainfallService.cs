using DigiteerTechnical2.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

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

        public async Task<RainfallReadingResponse?> GetRainfallReadingsAsync(string id, int count, string filters)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/flood-monitoring/id/stations/{id}/readings?{filters}&_limit={count}");
            var jsonResult = await response.Content.ReadAsStringAsync();
            var rainfallResult = JsonConvert.DeserializeObject<Rainfall>(jsonResult);

            if (rainfallResult == null || rainfallResult.items == null)
            {
                return null;
            }

            var rainfallReadings = new List<RainfallReading>();

            foreach (var item in rainfallResult.items)
            {
                var rainfallReading = new RainfallReading();
                rainfallReading.dateMeasured = item.dateTime;
                rainfallReading.amountMeasured = item.value;

                rainfallReadings.Add(rainfallReading);
            }

            var rainfallReadingResponse = new RainfallReadingResponse();
            rainfallReadingResponse.readings = rainfallReadings;

            return rainfallReadingResponse;
        }

        //public Task<RainfallReadingResponse> GetRainfallReadingsFull(int count)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<RainfallReadingResponse> GetRainfallReadingsLatestAsync(string id, int count)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
