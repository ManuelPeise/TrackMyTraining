namespace BusinessLogic.Shared.Interfaces
{
    public interface IHttpService
    {
        public HttpClient Client { get; }
        Task<T?> SendGetRequest<T>(string url, string? token = null);
        Task<T?> SendPostRequest<T>(string url, string body, string? token = null);
    }
}
