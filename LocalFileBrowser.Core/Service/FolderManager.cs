using LocalFileBrowser.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFileBrowser.Core.Service
{
    public class FolderManager
    {
        public static List<Item> GetAllItemsForFolder(string path)
        {
            if (!ValidateFolderPath(path))
                return null;

            List<Item> mlist = new List<Item>();

            try
            {
                mlist.AddRange(GetSubfoldersForFolder(path));
                mlist.AddRange(GetFilesForFolder(path));
            }
            catch (ArgumentNullException) { Console.WriteLine("Null exception"); }
            catch (Exception) { Console.WriteLine("Exception"); }
            
            return mlist;
        }

        private static List<Item> GetSubfoldersForFolder(string dirName)
        {
            try
            {
                List<Item> folders = new List<Item>();

                foreach (var item in Directory.GetDirectories(dirName))
                {
                    Item fItem = new Item();

                    DirectoryInfo ds = new DirectoryInfo(item);
                    fItem.FullName = ds.FullName;
                    fItem.Name = ds.Name;
                    fItem.Parent = ds.Parent.FullName;
                    fItem.Kind = ItemEnum.Folder;

                    folders.Add(fItem);
                }

                folders.Sort((x, y) => x.Name.CompareTo(y.Name));

                return folders;
            }
            catch (UnauthorizedAccessException ex) { Console.WriteLine("GetSubfoldersForFolder_Unauthorized"); Console.WriteLine(ex.Message); }
            catch (PathTooLongException ex) { Console.WriteLine("GetSubfoldersForFolder_PathTooLong"); Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine("GetSubfoldersForFolder_Exception"); Console.WriteLine(ex.Message); }
            
            return null;
        }

        private static List<Item> GetFilesForFolder(string dirName)
        {
            try
            {
                List<Item> files = new List<Item>();

                foreach (var item in Directory.GetFiles(dirName))
                {
                    Item fItem = new Item();

                    FileInfo fi = new FileInfo(item);
                    fItem.Name = fi.Name;
                    fItem.FullName = fi.FullName;
                    fItem.Parent = fi.DirectoryName;
                    fItem.Kind = ItemEnum.File;

                    files.Add(fItem);
                }

                files.Sort((x, y) => x.Name.CompareTo(y.Name));

                return files;
            }
            catch (UnauthorizedAccessException ex) { Console.WriteLine("GetFilesForFolder_Unauthorized"); Console.WriteLine(ex.Message); }
            catch (PathTooLongException ex) { Console.WriteLine("GetFilesForFolder_PathTooLong"); Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine("GetFilesForFolder_Exception"); Console.WriteLine(ex.Message); }

            return null;
        }

        public static bool ValidateFolderPath(string path)
        {
            if (path == null)
                return false;
            if (!Directory.Exists(path))
                return false;

            return true;
        }
    }
}
