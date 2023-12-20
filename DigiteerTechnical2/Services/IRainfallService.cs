using DigiteerTechnical2.Models;

namespace DigiteerTechnical2.Services
{
    public interface IRainfallService
    {
        public Task<RainfallReadingResponse?> GetRainfallReadingsAsync(string id, int count);

    }
}
