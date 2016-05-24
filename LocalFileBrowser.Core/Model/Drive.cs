using System;

namespace LocalFileBrowser.Core
{
    public class Drive
    {
        public string Name { get; set; }
        public long TotalSize { get; set; }
        public bool IsReady { get; set; }
    }
}
