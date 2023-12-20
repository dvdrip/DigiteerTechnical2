using DigiteerTechnical2.Models;

namespace DigiteerTechnical2.Services
{
    public interface IRainfallService
    {
        public Task<Rainfall?> GetRainfallsAsync(string id, int count);

    }
}
