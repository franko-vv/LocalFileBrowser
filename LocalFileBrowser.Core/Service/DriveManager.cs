using System;
using System.Collections.Generic;
using System.IO;

namespace LocalFileBrowser.Core.Service
{
    public static class DriveManager
    {
        private const int bytesInOneGb = 1024 * 1024 * 1024;

        public static List<Drive> GetAllDrives()
        {
            List<Drive> driversList = new List<Drive>();

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Drive driveToAdd = new Drive();

                driveToAdd.Name = drive.Name;
                driveToAdd.IsReady = drive.IsReady;

                if (drive.IsReady)
                {
                    driveToAdd.TotalSize = drive.TotalSize / bytesInOneGb;
                }

                driversList.Add(driveToAdd);
            }

            driversList.Sort((x, y) => x.TotalSize.CompareTo(y.TotalSize));
            driversList.Reverse();

            return driversList;
        }
    }
}
