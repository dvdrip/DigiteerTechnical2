using DigiteerTechnical2.Models;
using DigiteerTechnical2.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

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
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RainfallReadingResponse))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        //public async Task<RainfallReadingResponse?> Get()
        //{
        //    string id = "3680";
        //    int defaultCount = 10;
        //    return await _rainfallService.GetRainfallReadingsAsync(id, defaultCount);
        //}

        // GET api/<RainfallController>/5
        [HttpGet("/rainfall/id/{id}/readings")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RainfallReadingResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult?> Get(string id, [FromQuery][Range(1, 100)] int count, [FromQuery] bool today, [FromQuery] bool sorted, [FromQuery] DateTime? date, [FromQuery] DateTime? startdate, [FromQuery] DateTime? enddate, [FromQuery] DateTime? since)
        {
            try
            {
                StringBuilder filtersBuilder = new StringBuilder();

                if (today)
                {
                    filtersBuilder.Append($"date={DateTime.Now.ToString("yyyy-MM-dd")}&");
                }

                if (date.HasValue)
                {
                    filtersBuilder.Append($"date={date.Value.ToString("yyyy-MM-dd")}&");
                }

                if (startdate.HasValue && enddate.HasValue)
                {
                    filtersBuilder.Append($"startdate={startdate.Value.ToString("yyyy-MM-dd")}&enddate={enddate.Value.ToString("yyyy-MM-dd")}&");
                }

                if (since.HasValue)
                {
                    filtersBuilder.Append($"since={since.Value.ToString("yyyy-MM-ddTHH:mm:ss")}&");
                }

                var filters = filtersBuilder.ToString();
                
                var rainfallParsedResult = await _rainfallService.GetRainfallReadingsAsync(id, count, filters);

                if (rainfallParsedResult == null)
                {
                    var notFoundResponse = new ErrorResponse();
                    notFoundResponse.message = "Resource not found.";

                    var errorDetailList = new List<ErrorDetail>();
                    var errorDetail = new ErrorDetail();

                    errorDetail.propertyName = "id";
                    errorDetail.message = $"No data found for station with ID {id}";
                    errorDetailList.Add(errorDetail);

                    notFoundResponse.detail = errorDetailList;

                    return NotFound(notFoundResponse);

                }

                return Ok(rainfallParsedResult);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse();
                errorResponse.message = "Internal Server Error";

                var errorDetails = new List<ErrorDetail>();
                var errorDetail = new ErrorDetail();

                errorDetails.Add(errorDetail);
                errorDetail.message = ex.Message;

                errorResponse.detail = errorDetails;


                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
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
