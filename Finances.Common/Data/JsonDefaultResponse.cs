namespace Finances.Common.Data
{
    public class JsonDefaultResponse<T>
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }
    }
}