using DigiteerTechnical2.Models;
using DigiteerTechnical2.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigiteerTechnical2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        private readonly RainfallService _rainfallService;
        public RainfallController(RainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }

        // GET: api/<RainfallController>
        [HttpGet]
        public async Task<Rainfall?> Get()
        {
            string id = "3680";
            int defaultCount = 10;
            return await _rainfallService.GetRainfallsAsync(id, defaultCount);
        }

        // GET api/<RainfallController>/5
        [HttpGet("{id}")]
        public async Task<Rainfall?> Get(string id, int count)
        {
            return await _rainfallService.GetRainfallsAsync(id, count);
        }

        //// POST api/<RainfallController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RainfallController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RainfallController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
