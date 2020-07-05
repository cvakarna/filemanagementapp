using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementApp.Models.Files;
using FileManagementApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize]
    public class FileServiceController : ControllerBase
    {

        private readonly IFileService _fileservice;
        public FileServiceController(IFileService fileService)
        {
            this._fileservice = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromQuery]string userId,[FromForm]IFormFile file)
        {
            await this._fileservice.UploadAsync(file, userId);
            return Ok();
           
        }

        [HttpGet("files")]
        public async Task<IActionResult> Files([FromQuery]string userId)
        {
            var result = await this._fileservice.FilesAsync(userId);
            return Ok(result);
        }
        [HttpPost("download")]
        public async Task<IActionResult> Download([FromBody] FileDownloadRequest filedownloadReq)
        {
            var obj = await this._fileservice.DownloadAsync(filedownloadReq);
            return File(obj.MemoryStream, obj.ContentType, filedownloadReq.FileName);
        }

    }
}