using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Models.Files
{
    public class FileDownloadRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FileName { get; set; }
    }
}
