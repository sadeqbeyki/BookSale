using Microsoft.AspNetCore.Http;

namespace AppFramework.Application
{
    public interface IFileUploader
    {
        string Upload(IFormFile file,string path);
    }
}
