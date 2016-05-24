using LocalFileBrowser.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace LocalFileBrowser.Core.Service
{
    public static class GlobalFilesCalculation
    {
        public const int TenMb = 10 * 1024 * 1024;
        public const int FiftyMb = 50 * 1024 * 1024;
        public const int OneHundredMb = 100 * 1024 * 1024;

        public static FolderFilesVariations GetFilesCount(string path)
        {
            if (!FolderManager.ValidateFolderPath(path))
                return null;

            int filesThatLessThan10Mb = 0;
            int filesBetween10ANd50Mb = 0;
            int filesMoreThan100Mb = 0;

            int errCount = 0;

            var listOfFiles = GetAllFilesRecurs(path, "*.*", ref errCount);

            try
            {
                foreach (string file in listOfFiles)
                {
                    FileInfo fileInf = new FileInfo(file);
                    long fileLength = fileInf.Length;

                    if (fileLength <= TenMb)
                        filesThatLessThan10Mb++;

                    else if (fileLength > TenMb && fileLength <= FiftyMb)
                        filesBetween10ANd50Mb++;

                    else if (fileLength >= OneHundredMb)
                        filesMoreThan100Mb++;
                }
            }
            catch (UnauthorizedAccessException ex) { errCount++; Console.WriteLine("GetFilesCount_Unauthorized"); Console.WriteLine(ex.Message); }
            catch (PathTooLongException ex) { errCount++; Console.WriteLine("GetFilesCount_PathTooLong"); Console.WriteLine(ex.Message); }
            catch (Exception ex) { errCount++; Console.WriteLine("GetFilesCount_Exception"); Console.WriteLine(ex.Message); }

            FolderFilesVariations folderFiles = new FolderFilesVariations();
            folderFiles.Errors = errCount;
            folderFiles.FilesMoreThan100Mb = filesMoreThan100Mb;
            folderFiles.FilesBetween10ANd50Mb = filesBetween10ANd50Mb;
            folderFiles.FilesThatLessThan10Mb = filesThatLessThan10Mb;
            folderFiles.AllFilesCount = listOfFiles.Count;

            return folderFiles;
        }

        private static List<string> GetAllFilesRecurs(string path, string pattern, ref int errCount)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));

                foreach (var directory in Directory.GetDirectories(path))
                    files.AddRange(GetAllFilesRecurs(directory, pattern, ref errCount));
            }
            catch (UnauthorizedAccessException ex) { errCount++; Console.WriteLine("GetAllFilesRecurs_Unauthorized"); Console.WriteLine(ex.Message); }
            catch (PathTooLongException ex) { errCount++; Console.WriteLine("GetAllFilesRecurs_PathTooLong"); Console.WriteLine(ex.Message); }
            catch (DirectoryNotFoundException) { errCount++; Console.WriteLine("GetAllFilesRecurs_NotFound"); return null;  }
            catch (Exception) { errCount++; Console.WriteLine("GetAllFilesRecurs_Exception"); return null; }

            return files;
        }
    }
}
