using Azure.Storage.Blobs;
using healthguard.Models;

namespace healthguard.Interfaces
{
    public interface IFileService
    {
        Task Upload(FileModel fileModel);
        Task<Stream> Get(string fileName);
    }
}
