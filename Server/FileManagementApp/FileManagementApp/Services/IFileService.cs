using FileManagementApp.Models.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Services
{
    public interface IFileService
    {
        /// <summary>
        ///This method used to upload a file to the resources
        /// </summary>
        /// <param name="formfile">file to be request to upload</param>
        /// <param name="userId">Registered UserId of resources belongs to</param>
        /// <returns>empty</returns>
        Task UploadAsync(IFormFile formfile,string userId);
        /// <summary>
        /// This Method used to download the file from resources 
        /// </summary>
        /// <param name="filedownloadRequest">{FileDownloadRequest} properties to download file it includes userID and fileName </param>
        /// <returns>FileDownloadResponse</returns>
        Task<FileDownloadResponse> DownloadAsync(FileDownloadRequest filedownloadRequest);
        /// <summary>
        /// This method used to Fetch all the available files for the user
        /// </summary>
        /// <param name="userId">Registered userid </param>
        /// <returns>List of available files</returns>
        Task<List<FilesResponse>> FilesAsync(string userId);

    }
}
