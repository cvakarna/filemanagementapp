using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Helpers
{
    public class ContentTypeHelper
    {
        public static string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
