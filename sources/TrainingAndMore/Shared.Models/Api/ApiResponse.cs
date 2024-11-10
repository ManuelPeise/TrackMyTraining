namespace Shared.Models.Api
{
    public class ApiResponse<T> where T : class
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
    }
}
