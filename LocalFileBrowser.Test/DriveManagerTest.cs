using LocalFileBrowser.Core;
using LocalFileBrowser.Core.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LocalFileBrowser.Test
{
    [TestFixture]
    public class DriveManagerTest
    {
        [Test]
        public void GetCountOfDrivers()
        {
            List<Drive> drives = new List<Drive>();
            int countDrive = 6;

            drives = DriveManager.GetAllDrives();

            Assert.That(drives.Count, Is.EqualTo(countDrive));
        }

        [Test]
        public void GetSizeForC()
        {
            List<Drive> drives = new List<Drive>();
            long diskCsize = 195;

            drives = DriveManager.GetAllDrives();
            long diskC = drives.Where(c => c.Name == "C:\\").First().TotalSize;

            Assert.That(diskCsize, Is.EqualTo(diskC));
        }

        [Test]
        public void GetCountForInanctiveDrives()
        {
            List<Drive> drives = new List<Drive>();
            int inactiveDrives = 2;

            drives = DriveManager.GetAllDrives();
            int inactDrives = drives.Where(c => c.IsReady == false).ToList().Count;

            Assert.That(inactiveDrives, Is.EqualTo(inactDrives));
        }

        [Test]
        public void CheckSorting()
        {
            List<Drive> drives = new List<Drive>();
            bool firstSizeMoreThanLast = true;

            drives = DriveManager.GetAllDrives();
            var firstDrive = drives[0];
            var lastDrive = drives[drives.Count-1];
            bool checkSize = firstDrive.TotalSize > lastDrive.TotalSize;

            Assert.That(firstSizeMoreThanLast, Is.EqualTo(checkSize));
        }
    }
}
