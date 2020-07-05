using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Models.Files
{
    public class FilesResponse
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

    }
}
