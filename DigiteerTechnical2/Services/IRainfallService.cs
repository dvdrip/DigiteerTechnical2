using DigiteerTechnical2.Models;

namespace DigiteerTechnical2.Services
{
    public interface IRainfallService
    {
        public Task<RainfallReadingResponse?> GetRainfallReadingsAsync(string id, int count, string filters);

        //public Task<RainfallReadingResponse> GetRainfallReadingsLatestAsync(string id, int count);

        //public Task<RainfallReadingResponse> GetRainfallReadingsFull(int count);



    }
}
