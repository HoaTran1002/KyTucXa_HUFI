﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLKTX.Models;

namespace QLKTX.Controllers
{
	public class AboutController : Controller, IController
    {
		public ActionResult Index()
		{
			return View();
		}
	}
}

