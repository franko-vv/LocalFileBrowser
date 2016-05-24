using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace LocalFileBrowser.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FileBrowser()
        {
            return View();
        }
    }
}
