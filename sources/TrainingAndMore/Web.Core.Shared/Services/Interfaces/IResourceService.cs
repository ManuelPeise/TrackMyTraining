namespace Web.Core.Shared.Services.Interfaces
{
    public interface IResourceService<T> where T : class
    {
        string GetResource(string key);
    }
}
