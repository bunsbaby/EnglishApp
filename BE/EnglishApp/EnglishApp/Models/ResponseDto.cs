namespace EnglishApp.Models
{
    public class ResponseDto<T>
    {
        public bool Status { get; set; } = true;
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
