using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebBloodApi3.Helpers
{
    public class FilesHelper
    {

        public static bool UploadPhoto(MemoryStream memorystream, string FolderName, string FileName)
        {
            try
            {
                memorystream.Position = 0;
                var path = Path.Combine(HttpContext.Current.Server.MapPath(FolderName), FileName); //gets the complete path of the file i.e [c:/FolderName/FileName.....]
                File.WriteAllBytes(path, memorystream.ToArray());

            }
            catch(Exception ex)
            {

                throw new Exception(ex.ToString());
            }
            return true;
        }
    }
    
}