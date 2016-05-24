using System;
using System.Collections.Generic;

namespace LocalFileBrowser.Core.Models
{
    public enum ItemEnum
    {
        Folder,
        File
    }

    public class Item
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Parent { get; set; }
        public ItemEnum Kind { get; set; }
    }
}
