using LocalFileBrowser.Core;
using LocalFileBrowser.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Drive> listAll = DriveManager.GetAllDrives();
            
            string path = "C:\\";
            string pathRec = "C:\\";
            var currentFolderItems = FolderManager.GetAllItemsForFolder(path);       

            foreach (var item in listAll)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("----------------------------------------------------------");

            foreach (var item in currentFolderItems)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("----------------------------------------------------------");

            var items = GlobalFilesCalculation.GetFilesCount(pathRec);
            Console.WriteLine($"{items.FilesThatLessThan10Mb} -- " +
                              $"{items.FilesBetween10ANd50Mb} --" +
                              $"errors - {items.Errors} --" +
                              $"{items.FilesMoreThan100Mb}");

            Console.ReadKey();
        }
    }
}
