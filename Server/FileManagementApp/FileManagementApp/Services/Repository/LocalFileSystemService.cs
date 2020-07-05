using FileManagementApp.Helpers;
using FileManagementApp.Models.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Services.Repository
{
    public class LocalFileSystemService : IFileService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public LocalFileSystemService(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public async Task<FileDownloadResponse> DownloadAsync(FileDownloadRequest fileDownloadReq)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileDownloadReq.UserId, "uploads");
            var filePath = Path.Combine(uploads, fileDownloadReq.FileName);
            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return new FileDownloadResponse { MemoryStream = memory, ContentType = ContentTypeHelper.GetContentType(filePath) };

        }

        public Task<List<FilesResponse>> FilesAsync(string userId)
        {
            var result = new List<FilesResponse>();
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", userId, "uploads");
            if (Directory.Exists(uploads))
            {
                var provider = _hostingEnvironment.ContentRootFileProvider;
                int count = 0;
                foreach (string fileName in Directory.GetFiles(uploads))
                {
                    var fileInfo = provider.GetFileInfo(fileName);
                    var lastmodifeid = File.GetLastWriteTime(fileName);
                    string fileShorName = Path.GetFileName(fileInfo.Name);
                    result.Add(new FilesResponse { Position=++count,Name= fileShorName,ModifiedDateTime= lastmodifeid });
                }
            }
            return Task.FromResult(result);
        }

        public async Task UploadAsync(IFormFile formfile, string userId)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", userId, "uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);

            }
            if (formfile.Length > 0)
            {
                var filePath = Path.Combine(uploads, formfile.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formfile.CopyToAsync(fileStream);
                }

            }
        }

        //private string GetContentType(string path)
        //{
        //    var provider = new FileExtensionContentTypeProvider();
        //    string contentType;
        //    if (!provider.TryGetContentType(path, out contentType))
        //    {
        //        contentType = "application/octet-stream";
        //    }
        //    return contentType;
        //}
    }


}
