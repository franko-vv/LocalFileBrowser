using LocalFileBrowser.Core.Models;
using LocalFileBrowser.Core.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFileBrowser.Test
{
    [TestFixture]
    class FolderManagerTest
    {
        [Test]
        public void GetItemsWhenNullPath()
        {
            List<Item> filesAndFolders = new List<Item>();

            filesAndFolders = FolderManager.GetAllItemsForFolder(null);
            
            Assert.That(filesAndFolders, Is.Null);
        }

        [Test]
        public void GetItemsWhenWrongPath()
        {
            List<Item> filesAndFolders = new List<Item>();
            string path = "sdasdas/asa";

            filesAndFolders = FolderManager.GetAllItemsForFolder(path);

            Assert.That(filesAndFolders, Is.Null);
        }

        [Test]
        public void GetItemsForFolderE()
        {
            List<Item> filesAndFolders = new List<Item>();
            string path = "E:\\";
            int count = 26;

            filesAndFolders = FolderManager.GetAllItemsForFolder(path);

            Assert.That(filesAndFolders.Count, Is.EqualTo(count));
        }

        [Test]
        [Ignore("Create/delete empty folder")]
        public void GetItemsForEmptyFolder()
        {
            List<Item> filesAndFolders = new List<Item>();
            string path = "E:\\";
            int count = 0;

            DirectoryInfo di = new DirectoryInfo(path);
            string folderName = "EmptyTestFolder";
            di.CreateSubdirectory(folderName);
            string fullPath = "E:\\EmptyTestFolder";

            filesAndFolders = FolderManager.GetAllItemsForFolder(fullPath);

            Assert.That(filesAndFolders.Count, Is.EqualTo(count));
#warning !
            DirectoryInfo diToDel = new DirectoryInfo(fullPath);
            diToDel.Delete();
        }

        [Test]
        public void GetFoldersCount()
        {
            List<Item> filesAndFolders = new List<Item>();
            string path = "G:\\";
            int count = 11;

            filesAndFolders = FolderManager.GetAllItemsForFolder(path);
            int foldersCount = filesAndFolders.Where(c => c.Kind == ItemEnum.Folder).ToList().Count;

            Assert.That(filesAndFolders.Count, Is.EqualTo(count));
        }
    }
}
