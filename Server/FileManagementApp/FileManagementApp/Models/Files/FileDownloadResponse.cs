using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Models.Files
{
    public class FileDownloadResponse
    {
        public MemoryStream MemoryStream { get; set; }
        public string ContentType { get; set; }
    }
}
