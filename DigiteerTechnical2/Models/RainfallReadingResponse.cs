namespace DigiteerTechnical2.Models
{
    public class RainfallReading
    {
        public DateTime dateMeasured { get; set; }
        public int amountMeasured { get; set; }
    }

    public class RainfallReadingResponse
    {
        public List<RainfallReading> readings { get; set; }
    }


}
