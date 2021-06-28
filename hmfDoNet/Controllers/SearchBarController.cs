using hmfDoNet.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hmfDoNet.Controllers
{
    [checkUser]
    public class SearchBarController : Controller
    {
        // GET: SearchBar
        public ActionResult Index()
        {
            return View();
        }

    }
}