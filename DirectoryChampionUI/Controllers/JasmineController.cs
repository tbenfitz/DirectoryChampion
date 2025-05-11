using System;
using System.Web.Mvc;

namespace DirectoryChampionUI.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
