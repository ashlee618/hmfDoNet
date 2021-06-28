using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
// �������ģ��
using LeanCloud;
// ����洢ģ��
using LeanCloud.Storage;
// ������Ҫ�����뼴ʱͨѶģ��


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
