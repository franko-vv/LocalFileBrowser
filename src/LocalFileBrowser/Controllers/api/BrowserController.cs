using System;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Caching.Memory;
using LocalFileBrowser.Core.Service;
using LocalFileBrowser.Core.Model;
using System.Net;

namespace LocalFileBrowser.Controllers.api
{
    [Route("api/[controller]")]
    public class BrowserController : Controller
    {
        private IMemoryCache _memoryCache;

        public BrowserController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        //GET api/browser
        [HttpGet]
        public JsonResult Get()
        {
            var listAll = DriveManager.GetAllDrives();

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(listAll);
        }

        //GET api/browser/drive
        [HttpGet]
        [Route("drive")]
        public JsonResult Get([FromQuery] string path)
        {
            if (path == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
            
            try
            {               
                // Get files and folders for path
                var currentFolderItems = FolderManager.GetAllItemsForFolder(path);

                FolderFilesVariations countFilesVariationsForFolder;

                // If path - drive check cache
                if (path.Length == 3)
                    countFilesVariationsForFolder = SetGetCache(path);
                else
                    countFilesVariationsForFolder = GlobalFilesCalculation.GetFilesCount(path);

                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { files = currentFolderItems, countFiles = countFilesVariationsForFolder });
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }

        public FolderFilesVariations SetGetCache(string path)
        {
            FolderFilesVariations responseCountFiles;
            const int countMinutesForCache = 15;

            if (_memoryCache.TryGetValue(path, out responseCountFiles))
                return responseCountFiles;
            else
            {
                responseCountFiles = GlobalFilesCalculation.GetFilesCount(path);
                _memoryCache.Set(path, responseCountFiles,
                                new MemoryCacheEntryOptions()
                                .SetAbsoluteExpiration(TimeSpan.FromMinutes(countMinutesForCache)));
            }

            return responseCountFiles;
        }
    }
}
