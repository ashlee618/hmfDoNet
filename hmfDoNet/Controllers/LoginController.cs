using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hmfDoNet.Models;
using hmfDoNet.Utils;

// 导入基础模块
using LeanCloud;
// 导入存储模块
using LeanCloud.Storage;
// 如有需要，导入即时通讯模块



namespace hmfDoNet.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            var sessionToken = CookieHelper.GetCookieValue("sessionToken");
            if(sessionToken.Equals(""))
            {
                return View();
            }
            else
            {
                //执行seetion登录
                LCUser user=await LCUser.BecomeWithSessionToken(sessionToken);
                Session["userName"] = user.Username;
                System.Diagnostics.Debug.WriteLine("使用cookie登录");
                return RedirectToAction("index", "home");


            }

        }


        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Login(User u)
        {
            try
            {
                // 登录成功
                LCUser user = await LCUser.Login(u.Username, u.Password);

                Session["currentUser"] = user;
                System.Diagnostics.Debug.WriteLine(Session["currentUser"]);
                CookieHelper.SetCookie("sessionToken", user.SessionToken, DateTime.Now.AddDays(7));
                return RedirectToAction("index","home");
               
               
            }
            catch (LCException e)
            {
                // 登录失败（可能是密码错误）
                System.Diagnostics.Debug.WriteLine(e);
                if(e.Code== 210)
                {
                    ViewBag.Message = "账号密码不匹配";
                }
                else if(e.Code== 211)
                {
                    ViewBag.Message = "该用户未注册";

                }
                else if (e.Code == 216)
                {
                    ViewBag.Message = "该用户未验证邮箱，已经向绑定邮箱发送验证信息";
                    try
                    {
                        await LCUser.RequestEmailVerify("tom@leancloud.rocks");

                    }catch(LCException e1)
                    {
                        
                    }
               
                }

                return View("Index");

            }
        }
    }


}