using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HSG.DAL;
using HSG.Models;
using HSG.Services;
using System.Web.Security;

namespace HSG.Helper
{
    public static class _SessionUsr
    {
        public static void setUserSession(vw_Users data)
        {
            ID = data.ID;
            UserName = data.Name;
            Email = data.Email;            
        }

        public static int ID
        {
            get
            {
                try { return int.Parse(HttpContext.Current.Session["UsrID"].ToString()); }
                catch (Exception ex) { return Defaults.Integer; }
            }
            set { HttpContext.Current.Session["UsrID"] = value; }
        }
 
        public static string UserName
        {
            get { return (HttpContext.Current.Session["UsrUserName"] ?? "Guest").ToString(); }
            set { HttpContext.Current.Session["UsrUserName"] = value; }
        }

        public static string Email
        {
            get { return (HttpContext.Current.Session["UsrEmail"] ?? "").ToString(); }
            set { HttpContext.Current.Session["UsrEmail"] = value; }
        }

    }
}