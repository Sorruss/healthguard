using Azure.Storage.Blobs;
using healthguard.Interfaces;
using healthguard.Models;
using System.IO;

namespace healthguard.Repository
{
    public class FileService : IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public FileService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task Upload(FileModel fileModel)
        {
            var container = _blobServiceClient.GetBlobContainerClient("avatars");
            var blob = container.GetBlobClient(fileModel.ImageFile.FileName);
            await blob.UploadAsync(fileModel.ImageFile.OpenReadStream());
        }

        public async Task<Stream> Get(string fileName)
        {
            var container = _blobServiceClient.GetBlobContainerClient("avatars");
            var blob = container.GetBlobClient(fileName);
            var content = await blob.DownloadAsync();

            return content.Value.Content;
        }
    }
}
