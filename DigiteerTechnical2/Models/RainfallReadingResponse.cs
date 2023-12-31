﻿namespace DigiteerTechnical2.Models
{
    public class RainfallReading
    {
        public DateTime dateMeasured { get; set; }
        public double amountMeasured { get; set; }
    }

    public class RainfallReadingResponse
    {
        public List<RainfallReading>? readings { get; set; }
    }

}
