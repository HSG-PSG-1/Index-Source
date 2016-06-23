using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
// Following are for Custom Textbox
using System.Reflection;
using System.Web.Routing;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;

namespace HSG.Helper
{
    public static class ExtensionMethods
    {
        #region Function to iterator of  SelectListItem for a specific type <T>
        // http://stackoverflow.com/questions/1110070/how-to-get-the-values-of-an-enum-into-a-selectlist
        // Also see for lambda expression support http://blogs.msdn.com/b/stuartleeks/archive/2010/05/21/asp-net-mvc-creating-a-dropdownlist-helper-for-enums.aspx
        public static IEnumerable<SelectListItem> GetEnumSelectList<T>(string selText)
        {
            // BusinessObjects._Enums.ParseEnum<T>(enu) /
            return (Enum.GetValues(typeof(T)).Cast<T>().Select(
                enu => new SelectListItem()
                {
                    Text = enu.ToString().Replace("_"," "),
                    Value = Convert.ToInt16(Enum.Parse(typeof(T), enu.ToString(), true)).ToString(),
                    Selected = (enu.ToString().ToLower() == (selText ?? "").ToLower())
                })).ToList();
        }    

        public static IEnumerable<SelectListItem> GetEnumSelectList<T>(int? selID)
        {
            // Required for AddEdit where selected text is NOT available
            return (Enum.GetValues(typeof(T)).Cast<T>().Select(
                enu => new SelectListItem()
                {
                    Text = enu.ToString().Replace("_", " "),
                    Value = Convert.ToInt16(Enum.Parse(typeof(T), enu.ToString(), true)).ToString(),
                    Selected = (Convert.ToInt16(Enum.Parse(typeof(T), enu.ToString(), true)) == (selID ?? -1))
                })).ToList();
        }
        #endregion
        
        #region SortableColumn
        
        #endregion

        #region Custom Textbox
        const string requiredClass = "required";
        const int nTextMaxLen = 4000;
                
        public static MvcHtmlString CustomTextAreaFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, object htmlAttributes)
        {
            return htmlHelper.TextAreaFor(expression, rows, columns , GetMaxLenAndRequired(expression, htmlAttributes));
        }

        public static MvcHtmlString CustomTextBoxFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,object htmlAttributes)
        {
            return htmlHelper.TextBoxFor(expression, GetMaxLenAndRequired(expression, htmlAttributes));            
        }

        public static MvcHtmlString CustomTextBoxKOFor<TModel, TProperty>
            (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return htmlHelper.TextBoxFor(expression, GetMaxLenAndRequired(expression, htmlAttributes, true));
        }

        static IDictionary<string, object> GetMaxLenAndRequired(LambdaExpression expression, object htmlAttributes, bool isKO = false)
        {
            bool IsRequired = false;
            int maxLen = 0;
            MemberExpression member = expression.Body as MemberExpression;

            //First check the ColumnAttribute for max length and required
            try { maxLen = GetLengthLimitAndRequired(member.Member, ref IsRequired); }
            catch(Exception ex) { }
            //If the model was NOT a L2S class then we go back to its GetCustomAttributes because -
            StringLengthAttribute stringLength = member.Member
                .GetCustomAttributes(typeof(StringLengthAttribute), false)
                .FirstOrDefault() as StringLengthAttribute;
            //Partial model classes with metadata will not be inferred correctly

            #region Get max length and required

            if (stringLength != null && (maxLen < 1 || stringLength.MaximumLength < maxLen))
                maxLen = stringLength.MaximumLength;

            if (!IsRequired)
            {// In case the extended metadata has it as required
                try{IsRequired = !(member.Member.GetCustomAttributes(typeof(RequiredAttribute), true).
                    FirstOrDefault() as RequiredAttribute).AllowEmptyStrings;}
                catch { IsRequired = false; }
            }

            #endregion

            #region Set attributes

            var attributes = (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes);
            if (maxLen > 0)attributes.Add("maxlength", maxLen); //set maxlength based on metadata
            if (IsRequired)
            {
                string classAttrVal = (attributes["class"]??"").ToString();
                
                if(string.IsNullOrEmpty(classAttrVal))attributes.Add("class",requiredClass);
                else if(!classAttrVal.Contains(requiredClass))
                    attributes["class"] = classAttrVal + " " + requiredClass;
            }

            if (isKO)
            {
                attributes["data-bind"] = "value:" + member.Member.Name;
            }

            #endregion

            return attributes;
        }

        public static int GetLengthLimitAndRequired(MemberInfo obj, ref bool IsRequired)
        { //Ref: http://www.codeproject.com/Articles/27392/Using-the-LINQ-ColumnAttribute-to-Get-Field-Length & SO: 20684
            int maxLen = 0;   // default value = we can't determine the length

            Type type = obj.GetType();
            // Find the Linq 'Column' attribute
            // e.g. [Column(Storage="_FileName", DbType="NChar(256) NOT NULL", CanBeNull=false)]
            object[] info = obj.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), true);
            // Assume there is just one
            if (info.Length == 1)
            {
                System.Data.Linq.Mapping.ColumnAttribute ca = (System.Data.Linq.Mapping.ColumnAttribute)info[0];
                string dbtype = ca.DbType;

                if (dbtype == null) return maxLen;

                if (dbtype.StartsWith("NChar") || dbtype.StartsWith("NVarChar") || 
                    dbtype.StartsWith("Char") || dbtype.StartsWith("VarChar"))
                {
                    int index1 = dbtype.IndexOf("(");
                    int index2 = dbtype.IndexOf(")");
                    string dblen = dbtype.Substring(index1 + 1, index2 - index1 - 1);
                    int.TryParse(dblen, out maxLen);
                }
                else if(dbtype.StartsWith("NText"))
                    maxLen = nTextMaxLen;

                if (dbtype != null)//Set required
                    IsRequired = dbtype.Contains("NOT NULL");
            }//Return max length
            return maxLen;
        }

        #endregion

        #region For v4.0 & more: New MVC DropdownList typed-binding functions (for future)
        /* URL: http://blogs.msdn.com/b/stuartleeks/archive/2010/05/21/asp-net-mvc-creating-a-dropdownlist-helper-for-enums.aspx 
         OR: http://paste2.org/p/995813*/
        #endregion

        #region Future ref: Obj to obj comparison - but never gets called so dropped
        /* Also see: http://stackoverflow.com/questions/3701702/whats-the-difference-between-obj1-equalsobj2-and-static-object-equalsobj1-ob */
        /*public static Boolean Equals(this App_Data.User obj1, App_Data.User obj2, bool callMe)
        {
            return (obj1.Comment == obj2.Comment && obj1.EmailAddress == obj2.EmailAddress && obj1.FaxNumber == obj2.FaxNumber &&
                obj1.Id == obj2.Id && obj1.IsOnline == obj2.IsOnline && obj1.LastModifiedBy == obj2.LastModifiedBy &&
                obj1.LastModifiedDate == obj2.LastModifiedDate && obj1.LastPasswordChange == obj2.LastPasswordChange &&
                obj1.LogInDateTime == obj2.LogInDateTime && obj1.LogOutDateTime == obj2.LogOutDateTime && obj1.Name == obj2.Name &&
                obj1.OrgId == obj2.OrgId && obj1.Password == obj2.Password && obj1.PhoneNumber == obj2.PhoneNumber &&
                obj1.UpdateDateTime == obj2.UpdateDateTime && obj1.UserCode == obj2.UserCode && obj1.UserRoleId == obj2.UserRoleId);
        }*/
        #endregion

        public static System.Collections.IEnumerable Errors(this ModelStateDictionary modelState)
        { // http://stackoverflow.com/questions/2845852/asp-net-mvc-how-to-convert-modelstate-errors-to-json
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage).ToList())
                                    .Where(m => m.Value.Count() > 0).ToArray();
            }
            return null;
        }
    }
}

//HT: Extremely imp when altering the obj being selected without changing the select obj;
//http://blog.robvolk.com/2009/05/linq-select-object-but-change-some.html
namespace System.Linq
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Used to modify properties of an object returned from a LINQ query
        /// </summary>
        public static TSource Set<TSource>(this TSource input,
            Action<TSource> updater)
        {
            updater(input);
            return input;
        }
    }
}
