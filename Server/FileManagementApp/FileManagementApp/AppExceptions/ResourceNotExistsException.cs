using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.AppExceptions
{
    public class ResourceNotExistsException:Exception
    {
        public ResourceNotExistsException(string message):base(message)
        {

        }
    }
}
