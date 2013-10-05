using System.IO;

namespace Bellini.Library
{
    public class Helpers
    {
        public static bool DirectoryExist(string directoryPatch)
        {
            var objDirectory = new DirectoryInfo(directoryPatch);
            if (objDirectory.Exists)
            {
                return true;
            }

            try
            {
                Directory.CreateDirectory(directoryPatch);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
