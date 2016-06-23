using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Data.Linq.SqlClient;
using HSG.DAL;
using HSG.Helper;

namespace HSG.Services
{
    public class DashboardService : _ServiceBase
    {
        public List<catItem> Fetch()
        {
            using (dbc)
            {
                IQueryable<Category> cats = 
                    (from c in dbc.Categories orderby c.Name select c);

                IQueryable<Website> webs =
                    (from w in dbc.Websites orderby w.Featured descending, w.CompanyName select w);

                vw_Dashboard data = new vw_Dashboard()
                { categories = cats.ToList(), websites = webs.ToList() };

                List<catItem> result = new List<catItem>(data.categories.Count);

                foreach (Category cat in data.categories)
                { 
                    List<Website> catWebs = data.websites.FindAll(w => w.CategoryID == cat.ID);
                    result.Add(new catItem(){ catg = cat, websites = catWebs});
                }

                return result;
            }            
        }
        /*
        public static IQueryable<vw_Dashboard> PrepareQuery(IQueryable<vw_Dashboard> dasQ, vw_Dashboard das)
        {
            #region Append WHERE clause if applicable

            //dasQ = dasQ.Where(o => o.Archived == das.Archived);

            if (!string.IsNullOrEmpty(das.PONumbers))// Filter for multiple PO No.s
            {
                dasQ = dasQ.Where(o => SqlMethods.Like(o.PONumber.ToLower(), "%" + das.PONumbers.ToLower() + "%"));
            }
            
            if (das.BrandID > 0) dasQ = dasQ.Where(o => o.BrandID == das.BrandID);
            else if (!string.IsNullOrEmpty(das.BrandName))
                dasQ = dasQ.Where(o => SqlMethods.Like(o.BrandName.ToLower(), "%" + das.BrandName.ToLower() + "%"));
            
            if (das.OrderStatusID > 0) dasQ = dasQ.Where(o => o.OrderStatusID == das.OrderStatusID);
            else if (!string.IsNullOrEmpty(das.Status))
                dasQ = dasQ.Where(o => SqlMethods.Like(o.Status.ToLower(), das.Status.ToLower()));

            if (das.AssignTo > 0) dasQ = dasQ.Where(o => o.AssignTo == das.AssignTo);
            if (das.VendorID > 0) dasQ = dasQ.Where(o => o.VendorID == das.VendorID);

            if (!string.IsNullOrEmpty(das.ShipToCity)) dasQ = dasQ.Where
               (o => SqlMethods.Like(o.ShipToCity.ToLower(), "%" + das.ShipToCity.ToLower() + "%"));

            #region Apply date filter
            //http://www.filamentgroup.com/lab/date_range_picker_using_jquery_ui_16_and_jquery_ui_css_framework/
            if (das.PODateFrom.HasValue) dasQ = dasQ.Where(o => o.PODate.Value.Date >= das.PODateFrom_SQL.Value.Date);
            if (das.PODateTo.HasValue) dasQ = dasQ.Where(o => o.PODate.Value.Date <= das.PODateTo_SQL.Value.Date);
            if (das.ETAFrom.HasValue) dasQ = dasQ.Where(o => o.Eta.Value.Date >= das.ETAFrom_SQL.Value.Date);
            if (das.ETATo.HasValue) dasQ = dasQ.Where(o => o.Eta.Value.Date <= das.ETATo_SQL.Value.Date);
            if (das.ETDFrom.HasValue) dasQ = dasQ.Where(o => o.Etd.Value.Date >= das.ETDFrom_SQL.Value.Date);
            if (das.ETDTo.HasValue) dasQ = dasQ.Where(o => o.Etd.Value.Date <= das.ETDTo_SQL.Value.Date);

            #endregion

            #endregion

            #region Special case for Asia: Operations

            int[] orgs = new int[] { Config.VendorIDDeestone, Config.VendorIDSvizz };

            if (_Session.IsAsiaOperations) // SO : 183791
                dasQ = dasQ.Where(o => !orgs.Contains(o.VendorID ?? -1)); // (Internal) Asia operations role : can see all PO’s except Deestone and Svizz-One.
            else if (_Session.IsAsiaVendor)
                dasQ = dasQ.Where(o => orgs.Contains(o.VendorID ?? -1)); // (Vendor) Asia Vendor role : can see ONLY PO’s for both Deestone and Svizz-One.

            #endregion

            return dasQ;
        }
        */
    }
}
