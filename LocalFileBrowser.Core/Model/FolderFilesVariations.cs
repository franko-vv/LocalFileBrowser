using System;

namespace LocalFileBrowser.Core.Model
{
    public class FolderFilesVariations
    {
        public long FilesMoreThan100Mb { get; set; }
        public long FilesBetween10ANd50Mb { get; set; }
        public long FilesThatLessThan10Mb { get; set; }
        public long AllFilesCount { get; set; }
        public int Errors { get; set; }
    }
}
