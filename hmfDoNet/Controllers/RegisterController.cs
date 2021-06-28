using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hmfDoNet.Models;
// 导入基础模块
using LeanCloud;
// 导入存储模块
using LeanCloud.Storage;
// 如有需要，导入即时通讯模块
namespace hmfDoNet.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Register(User user)
        {
            LCUser Lcuser = new LCUser();

            Lcuser.Username = user.Username;
            Lcuser.Password = user.Password;

            Lcuser.Email = user.Email;

            Lcuser["gender"] = "secret";
            try
            {
                await Lcuser.SignUp();
                return RedirectToAction("index", "home");
            }
            catch (LCException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                ViewBag.Message = e.Message;
            }
            return View("Index");

        }

        public ActionResult CheckEmail()
        {


            return View();
        }
    }
}