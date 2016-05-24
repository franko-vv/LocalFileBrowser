using LocalFileBrowser.Core.Model;
using LocalFileBrowser.Core.Models;
using LocalFileBrowser.Core.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFileBrowser.Test
{
    [TestFixture]
    class GlobalCalculationTest
    {   
        [Test]
        public void GetFilesCount()
        {
            FolderFilesVariations filesCount = new FolderFilesVariations();
            string path = "E:\\";

            filesCount = GlobalFilesCalculation.GetFilesCount(path);

            Assert.That(filesCount.AllFilesCount, Is.GreaterThan(700));
        }

        [Test]
        public void CheckNumberOfFilesBetween50To100()
        {
            FolderFilesVariations filesCount = new FolderFilesVariations();
            string path = "E:\\";

            filesCount = GlobalFilesCalculation.GetFilesCount(path);
            var filesCountBetween50To100 = filesCount.AllFilesCount - (filesCount.FilesBetween10ANd50Mb + filesCount.FilesMoreThan100Mb + filesCount.FilesThatLessThan10Mb);

            Assert.That(filesCount.AllFilesCount, Is.GreaterThan(0));
        }

        [Test]
        public void IfPathIsWrong()
        {
            FolderFilesVariations filesCount = new FolderFilesVariations();
            string path = "Essad:\\";

            filesCount = GlobalFilesCalculation.GetFilesCount(path);

            Assert.That(filesCount, Is.Null);
        }
    }
}
