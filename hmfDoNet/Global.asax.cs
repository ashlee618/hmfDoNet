using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
// 导入基础模块
using LeanCloud;
// 导入存储模块
using LeanCloud.Storage;
// 如有需要，导入即时通讯模块


namespace hmfDoNet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LCApplication.Initialize("xM2EwpLgU7TOX3bdDXKgrUNV-gzGzoHsz", 
                "8QHej2RY0tcdJgkKRNRMEOkU",
                "https://xm2ewplg.lc-cn-n1-shared.com");
        }
    }
}
