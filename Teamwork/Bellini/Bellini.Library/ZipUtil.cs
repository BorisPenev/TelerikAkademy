using System;
using System.Collections;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace Bellini.Library
{
    public static class ZipUtil
    {
        public static void UnZipFiles(string zipFilePath, string outputFolder, string password, bool deleteZipFile, string moveZipToFolder)
        {
            using (var s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                if (!string.IsNullOrEmpty(password))
                {
                    s.Password = password;
                }

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = outputFolder;
                    string fileName = Path.GetFileName(theEntry.Name);
                    // create directory 
                    if (Helpers.DirectoryExist(directoryName))
                    {
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            if (theEntry.Name.IndexOf(".ini", System.StringComparison.Ordinal) < 0)
                            {
                                string fullPath = directoryName + "\\" + theEntry.Name;
                                fullPath = fullPath.Replace("\\ ", "\\");
                                string fullDirPath = Path.GetDirectoryName(fullPath);
                                if (fullDirPath != null && !Directory.Exists(fullDirPath))
                                {
                                    Directory.CreateDirectory(fullDirPath);
                                }
                                
                                using (var streamWriter = File.Create(fullPath))
                                {
                                    int size = 2048;
                                    var data = new byte[size];
                                    while (true)
                                    {
                                        size = s.Read(data, 0, data.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(data, 0, size);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    streamWriter.Close();
                                }
                            }
                        }
                    }
                }

                s.Close();
                s.Dispose();
            }

            if (deleteZipFile)
            {
                File.Delete(zipFilePath);
            }

            if (!string.IsNullOrEmpty(moveZipToFolder) && Helpers.DirectoryExist(moveZipToFolder))
            {
                string zipName = zipFilePath.Split('\\').Last();
                File.Move(zipFilePath, moveZipToFolder + "\\" + zipName);
            }
        }

        public static void ZipFiles(string inputFolderPath, string fileNamePath, string password)
        {
            ArrayList arrayList = GenerateFileList(inputFolderPath); // generate file list
            int trimLength = (Directory.GetParent(inputFolderPath)).ToString().Length;
            // find number of chars to remove     // from orginal file path
            trimLength += 1; //remove '\'
            using (var oZipStream = new ZipOutputStream(File.Create(fileNamePath))) // create zip stream
            {
                if (!string.IsNullOrEmpty(password))
                {
                    oZipStream.Password = password;
                }
                oZipStream.SetLevel(9); // maximum compression

                foreach (string fill in arrayList) // for each file, generate a zipentry
                {
                    var oZipEntry = new ZipEntry(fill.Remove(0, trimLength));
                    oZipStream.PutNextEntry(oZipEntry);

                    if (!fill.EndsWith(@"/")) // if a file ends with '/' its a directory
                    {
                        byte[] obuffer;
                        using (FileStream ostream = File.OpenRead(fill))
                        {
                            File.OpenRead(fill);
                            obuffer = new byte[ostream.Length];
                            ostream.Read(obuffer, 0, obuffer.Length);
                        }
                        oZipStream.Write(obuffer, 0, obuffer.Length);
                    }
                }
                oZipStream.Finish();
                oZipStream.Close();
            }
        }


        private static ArrayList GenerateFileList(string folderPath)
        {
            var fils = new ArrayList();
            bool isEmpty = true;
            foreach (string file in Directory.GetFiles(folderPath)) // add each file in directory
            {
                fils.Add(file);
                isEmpty = false;
            }

            if (isEmpty)
            {
                if (Directory.GetDirectories(folderPath).Length == 0)
                // if directory is completely empty, add it
                {
                    fils.Add(folderPath + @"/");
                }
            }

            foreach (string dirs in Directory.GetDirectories(folderPath)) // recursive
            {
                foreach (object obj in GenerateFileList(dirs))
                {
                    fils.Add(obj);
                }
            }
            return fils; // return file list
        }
    }
}