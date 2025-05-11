using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DirectoryChampionUI.Controllers
{
	public class DirectoryController : Controller
	{
        ////private readonly IConfiguration configuration;

        ////public MyController(IConfiguration configuration)
        ////{
        ////    this.configuration = configuration;
        ////}

        ////[HttpGet]
        ////public ActionResult GetConfigurationValue(string sectionName, string paramName)
        ////{
        ////    var parameterValue = configuration[$"{sectionName}:{paramName}"];
        ////    return Json(new { parameter = parameterValue });
        ////}

        // GET: Default
        public ActionResult Index()
	      {
            return View();
	      }
	}
}