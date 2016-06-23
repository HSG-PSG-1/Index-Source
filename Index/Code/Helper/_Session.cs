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
    public class _Session
    {
        const string sep = ";";

        #region Security objects

        public static bool IsAdmin
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Misc & functions
                
        public static bool IsValid(HttpContext ctx)
        {/*See in future if need more deep validation 
            if (!string.IsNullOrEmpty((ctx.Session["UserObj"] ?? "").ToString()))
                return (_SessionUsr != new UserService().emptyView);

            return false;*/
            return _SessionUsr.ID > 0;
        }

        public static void Signout()
        {
            FormsAuthentication.SignOut();//HT: reset forms authentication!

            #region clear authentication cookie
            // Get all cookies with the same name
            string[] cookies = new string[] { Defaults.cookieName, Defaults.emailCookie, Defaults.passwordCookie };
            
            //Iterate for each cookie and remove
            foreach (string cookie in HttpContext.Current.Request.Cookies.AllKeys)
                if (!cookies.Contains(cookie))
                    HttpContext.Current.Request.Cookies.Remove(cookie);
            // Strange but it is needed to do it the second time
            foreach (string cookie in HttpContext.Current.Response.Cookies.AllKeys)
                if (!cookies.Contains(cookie))
                    HttpContext.Current.Response.Cookies.Remove(cookie);

            #endregion
            
            //Clear & Abandon session
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        public static string WebappVersion
        { // http://www.craftyfella.com/2010/01/adding-assemblyversion-to-aspnet-mvc.html
            get
            {
                if (string.IsNullOrEmpty((HttpContext.Current.Session["WebappVersion"] ?? "").ToString()))
                {
                    try
                    {
                        System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                        return version.Major + "." + version.Minor + "." + version.Build;
                    }
                    catch (Exception)
                    {
                        return "?.?.?";
                    }
                }
                else
                    return HttpContext.Current.Session["WebappVersion"].ToString();
            }
        }

        /// ref : http://www.codeproject.com/Articles/34422/Detecting-a-mobile-browser-in-ASP-NET
        public static string httpUserAgent()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"]??"~";
        }

        public static bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NET CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                        {
                    "midp", "j2me", "avant", "docomo",
                    "novarra", "palmos", "palmsource",
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/",
                    "blackberry", "mib/", "symbian",
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio",
                    "SIE-", "SEC-", "samsung", "HTC",
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx",
                    "NEC", "philips", "mmm", "xx",
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java",
                    "pt", "pg", "vox", "amoi",
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo",
                    "sgh", "gradi", "jb", "dddi",
                    "moto", "iphone"
                        };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }

}
