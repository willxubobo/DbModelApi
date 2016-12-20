using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace NET.Framework.Common.IOHelper
{
    public static class ZipHelper
    {
        public static void CreateZipFile(string folderPath)
        {
            using (FileStream zipFileToOpen = new FileStream(folderPath+".zip", FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Create))
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                foreach (var f in di.GetFiles())
                {
                    ZipArchiveEntry readMeEntry = archive.CreateEntry(f.Name);
                    using (System.IO.Stream stream = readMeEntry.Open())
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(f.FullName);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }
    }
}
