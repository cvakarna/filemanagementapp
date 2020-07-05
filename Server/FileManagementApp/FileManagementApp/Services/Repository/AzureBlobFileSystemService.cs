using FileManagementApp.AppExceptions;
using FileManagementApp.Helpers;
using FileManagementApp.Models.Configuration;
using FileManagementApp.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Services.Repository
{
    public class AzureBlobFileSystemService : IFileService
    {
        private readonly AzureBlobSettings _blobSettings;
        public AzureBlobFileSystemService(IOptions<AzureBlobSettings> blobSettings)
        {
            this._blobSettings = blobSettings.Value;
        }
        public async Task<FileDownloadResponse> DownloadAsync(FileDownloadRequest filedownloadRequest)
        {
            CloudBlobContainer cloudBlobContainer = GetBlobContainerConfig(filedownloadRequest.UserId);
            bool isContainerExists = await cloudBlobContainer.ExistsAsync();
            if (!isContainerExists)
            {
                throw new ResourceNotExistsException($"userId: {filedownloadRequest.FileName} does not exists");
            }
            //Get blob reference
            var blobReference = cloudBlobContainer.GetBlockBlobReference(filedownloadRequest.FileName);
            bool isExists = await blobReference.ExistsAsync();
            if (!isExists)
            {
                throw new ResourceNotExistsException($"Provided File {filedownloadRequest.FileName} does not exists");
            }
            var stream = new MemoryStream();
            await blobReference.DownloadToStreamAsync(stream);
            stream.Position = 0;
            FileDownloadResponse response = new FileDownloadResponse { ContentType = ContentTypeHelper.GetContentType(blobReference.Name), MemoryStream = stream };
            return response;
            
        }

        public async Task<List<FilesResponse>> FilesAsync(string userId)
        {
            List<FilesResponse> filesList = new List<FilesResponse>();
            CloudBlobContainer cloudBlobContainer = GetBlobContainerConfig(userId);
            bool isContainerExists = await cloudBlobContainer.ExistsAsync();
            BlobContinuationToken blobContinuationToken = null; //cancelation token
            if (isContainerExists)
            {
                var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                int count = 0;
                foreach(CloudBlockBlob result in results.Results)
                {
                    string fileName = result.Name;
                    var modifiedDatetime = result.Properties.LastModified.Value.LocalDateTime;
                    var createdDate = result.Properties.Created.Value.LocalDateTime;
                    var response = new FilesResponse { Name = fileName, Position = ++count, CreatedDateTime = createdDate,ModifiedDateTime=modifiedDatetime };
                    filesList.Add(response);
                }
            }
            return filesList;

        }

        public async Task UploadAsync(IFormFile formfile, string userId)
        {
            CloudBlobContainer cloudBlobContainer = GetBlobContainerConfig(userId);
            bool isContainerExists = await cloudBlobContainer.ExistsAsync();
            if (!isContainerExists) //if not exists create new container
            {
                await cloudBlobContainer.CreateAsync();
            }

            //Get the reference to the block blob from the container
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(formfile.FileName);
            // Upload the file
            using (var stream = formfile.OpenReadStream())
            {
                cloudBlockBlob.UploadFromStream(stream);
            }
        }
        private CloudBlobContainer GetBlobContainerConfig(string containerName)
        {
            string blobStoragekey = this._blobSettings.BlobStorageKey;
            CloudBlobContainer cloudBlobContainer = null;
            //create storage account
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(blobStoragekey, out storageAccount))
            { 
                  // Create the CloudBlobClient 
                  CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                 // Get reference to the blob container by passing the name 
                 cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            }
            return cloudBlobContainer;

        }
    }
}
