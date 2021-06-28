using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeanCloud.Storage;
using LeanCloud;
namespace hmfDoNet.Filter

{
    public class checkUserAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 该方法会在Action方法执行之前调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CheckLogin();
            if (HttpContext.Current.Session["userName"] == null)
            {
                System.Diagnostics.Debug.WriteLine("用户还未登录");
                filterContext.Result = new RedirectResult("/Login/index");
            }
        }

        public async System.Threading.Tasks.Task CheckLogin()
        {
            try
            {
                LCUser user = await LCUser.GetCurrent();
            }
            catch (LCException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

        }

    }
}