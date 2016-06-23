using System;
using System.Configuration;
using System.Web;
using HSG.Services;

namespace HSG.Helper
{
    public class Config
    {
        public static System.Collections.Hashtable ConfigSettings
        {
            get { return (System.Collections.Hashtable)(HttpContext.Current.Session["ConfigSettings"]); }
            set { HttpContext.Current.Session["ConfigSettings"] = value; }
        }

        #region WEB.CONFIG Properties

        #region Root / Path / Download url / Email Temp dir

        /// Get the path of root directory
        /// <summary>
        /// Property used to get the path of root directory
        /// </summary>
        /// 
        public static string RootPath
        {
            get
            {
                return HttpContext.Current.Request.PhysicalApplicationPath; //+ FileIO.dSep;
            }
        }

        /// Get the path of upload file
        /// <summary>
        /// Property used to get the path of upload file
        /// </summary>
        public static string UploadPath
        {
            get
            {
                return RootPath + ConfigurationManager.AppSettings.Get("uploadPath");// +FileIO.webPathSep;
            }
        }

        /// Get the path for download Url
        /// <summary>
        /// Property used to get the path for download Url
        /// </summary>
        public static string DownloadUrl
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("downloadUrl");
            }
        }
                
        /// Path of email Template with reference to the roor directory
        /// <summary>
        /// Property used to get the path of email Template with reference to the roor directory
        /// </summary>
        /// 
        public static string EmailTemplatePathByRefToRoot
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("emailTemplatePathByRefToRoot");
            }
        }

        #endregion

        /// Max upload file size in MB
        /// <summary>
        /// Max upload file size in MB (Default 20mb)
        /// </summary>
        public static int MaxFileSizMB
        {
            get
            {
                int sizeMB;

                try { sizeMB = int.Parse(ConfigurationManager.AppSettings.Get("MaxFileSizMB")); }
                catch { sizeMB = 20; }

                return sizeMB;
            }
        }      

        ///Flag to debug(display as text on page) the emails sent bt web app
        /// <summary>
        /// Flag to debug(display as text on page) the emails sent bt web app
        /// </summary>
        public static bool DebugMail
        {
            get
            {
                bool flag = false;
                bool.TryParse(ConfigurationManager.AppSettings.Get("debugMail"), out flag);
                return flag;
            }
        }

        ///Flag to Nofity AssignTo user everytime his PO is updated
        /// <summary>
        ///Flag to Nofity AssignTo user everytime his PO is updated
        /// </summary>
        public static bool NofityAssignToEveryTime
        {
            get
            {
                bool flag = false;
                bool.TryParse(ConfigurationManager.AppSettings.Get("nofityAssignToEveryTime"), out flag);
                return flag;
            }
        }
                
        /// Application Error Email Notifiers
        /// <summary>
        /// Application Error Email Notifiers
        /// </summary>
        /// 
        public static string ApplicationErrorEmail
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("applicationErrorEmail");
            }
        }

        /// Customer Code Length in Location Code
        /// <summary>
        /// Customer Code Length in Location Code
        /// </summary>
        public static int CustCodeLenInLocCode
        { get { return int.Parse(ConfigurationManager.AppSettings.Get("custCodeLenInLocCode")); } }

        /// VendorID for Deestone
        /// <summary>
        /// VendorID for Deestone
        /// </summary>
        public static int VendorIDDeestone
        {
            get
            {
                try { return int.Parse(ConfigurationManager.AppSettings.Get("vendorIDDeestone")); }
                catch (Exception ex) { return -1; }
            }
        }

        /// VendorID for Svizz
        /// <summary>
        /// VendorID for Svizz
        /// </summary>
        public static int VendorIDSvizz
        {
            get
            {
                try { return int.Parse(ConfigurationManager.AppSettings.Get("VendorIDSvizz")); }
                catch (Exception ex) { return -1; }
            }
        }

        /// VendorID for Siamtruck Radial Company Ltd.
        /// <summary>
        /// VendorID for Siamtruck Radial Company Ltd.
        /// </summary>
        public static int VendorIDSiamtruck
        {
            get
            {
                try { return int.Parse(ConfigurationManager.AppSettings.Get("VendorIDSiamtruck")); }
                catch (Exception ex) { return -1; }
            }
        }

        #endregion //Properties

    }
}