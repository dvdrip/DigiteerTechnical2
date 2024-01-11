namespace DigiteerTechnical2.Models
{
    public class ErrorDetail
    {
        public string? propertyName { get; set; }
        public string? message { get; set; }
    }

    public class ErrorResponse
    {
        public string? message { get; set; }
        public List<ErrorDetail> detail { get; set; }
    }
}
