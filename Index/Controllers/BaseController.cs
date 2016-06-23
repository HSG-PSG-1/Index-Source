using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HSG.Helper;

namespace HSG.Controllers
{
    //[CompressFilter]
    //[HandleError] - handled in Application_Error
    public class BaseController : Controller
    {
        #region Variables & properties
        //HT: Make sure these two variables are populated in child controller
        public int gridPageSize = 2;
        private string sortOn = "";
               
        
        /// <summary>
        /// Stores the search filters. Make sure this is accessed as GET to set the ViewState
        /// </summary>
        /*public object searchOpts
        {
            set
            {
                if (filter == Filters.list._None)
                    System.Web.HttpContext.Current.Session["SearchOpts"] = value;
                else
                    _Session.Search[filter] = value;

                ViewData["SearchData"] = value;
            }
            get
            {// Set in Viewdata for filter-controls to be populated!
                object opts;
                if (filter == Filters.list._None) opts = (System.Web.HttpContext.Current.Session["SearchOpts"]) ?? searchObj;
                else opts = _Session.Search[filter];

//                ViewData["SearchData"] = opts;
                return opts;
            }
        }*/

        /// <summary>
        /// TempData["oprSuccess"] - will be reset upon first access
        /// </summary>
        public object operationSuccess
        {
            set { TempData["oprSuccess"] = value; }
            get
            {/* Set in Viewdata for filter-controls to be populated! */
                object opr = TempData["oprSuccess"];
                TempData["oprSuccess"] = null;//reset to avoid reusage
                return opr;
            }
        }

        #endregion

        public BaseController() { ;}

        public BaseController(int gridPgSize, string defaultSort, object searchObject)
        {
            gridPageSize = gridPgSize;
            sortOn = defaultSort;
            //searchObj = searchObject;
            //DON'T reset-_Session.PrevSort = "";
        }

        #region Extra Functions

        /// <summary>
        /// Default search opts will be taken from searchObj (initialized in constructor) and so will be the default sort
        /// </summary>
        /// <param name="index">page index</param>
        /*public object SetSearchOpts(int index)
        {
            if (index == 0 && sortExpr == sortOn)//Initialize only the first time (not a paging/sort)
                searchOpts = searchObj;

            return searchOpts;//Make sure searchOpts GET is accessed so that ViewState data is set
        }*/

        #endregion
    }
}
