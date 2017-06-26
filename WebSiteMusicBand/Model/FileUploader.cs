using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public class FileUploader
    {
        public delegate bool UploadDel(string path, int id);
        public event UploadDel changeDB;
        public string UlpoadFile(HttpPostedFileBase file, string folder, string serverPath, int id)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string pic = Path.GetFileName(file.FileName);
                    string path =serverPath + folder +"\\"+ pic;
                    file.SaveAs(path);
                    if (changeDB.Invoke(folder.Replace(@"\",@"/") + "/" + pic, id))
                    {
                        return folder + "/" + pic;
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                changeDB = null;
            }
        }

    }
}