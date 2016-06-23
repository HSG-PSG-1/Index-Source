using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HSG.DAL;
using HSG.Services;
using HSG.Helper;

namespace HSG.Controllers
{
    //[CompressFilter] - DON'T
    //[IsAuthorize(IsAuthorizeAttribute.Rights.NONE)]//Special case for some dirty session-abandoned pages and hacks
    [AllowAnonymous]
    public partial class DashboardController : BaseController
    {
        public DashboardController() : //HT: Make sure this is initialized with default constructor values!
            base() { ;}
                
        public ActionResult Index()
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie("ht", true);//Set forms authentication!
            return View(new DashboardService().Fetch());
        }

        public ActionResult List()
        {
            return View(new DashboardService().Fetch());
        }

        #region Misc actions
        // HT: Whem common controller is included, remove from her and also 
        // make changes in IsAuthorizedAttribute
        // filterContext.Controller = new HSG.Controllers.DashboardController();
                
        public ActionResult NoAccess()
        {
            return View();// Will return the default view
        }

        #endregion

    }
}
