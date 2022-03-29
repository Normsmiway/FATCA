using System;
using System.IO;

namespace Fatca.Core.Extensions
{
    public static class PathExtension
    {
        public static string GetDatePathBaseOnCurrentPath(this string currentPath)
        {
            string directoryName = Path.GetDirectoryName(currentPath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(currentPath);

            if (fileNameWithoutExtension.Contains("_"))
                fileNameWithoutExtension = fileNameWithoutExtension.Split('_')[0];

            string fileName = Path.GetFileName(currentPath);
            string dateTimePath = Path.Combine(DateTime.Now.Year.ToString(),
                DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());

            string path = Path.Combine(directoryName, dateTimePath, fileNameWithoutExtension, fileName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}
