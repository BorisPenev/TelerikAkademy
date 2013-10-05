using System;

namespace CohesionAndCoupling
{
    static class FileUtils
    {
        public static string GetFileExtension(string fileName)
        {
            var indexOfLastDot = fileName.LastIndexOf(".");
            if (indexOfLastDot == -1)
            {
                throw new FormatException("Invalid fileName");
            }

            var extension = fileName.Substring(indexOfLastDot + 1);
            return extension;
        }

        public static string GetFileNameWithoutExtension(string fileName)
        {
            var indexOfLastDot = fileName.LastIndexOf(".");
            if (indexOfLastDot == -1)
            {
                throw new FormatException("Invalid fileName");
            }

            var extension = fileName.Substring(0, indexOfLastDot);
            return extension;
        }
    }
}
