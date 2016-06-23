using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text.RegularExpressions;
using HSG.Services;
using HSG.Helper;

namespace HSG.DAL
{    
    /*[DuplicateMatchAttribute(DuplicateMatchAttribute.Target.User, DuplicateMatchAttribute.Field.Email,
        "Email", "EmailOLD", ErrorMessage = "Duplicate '{0}' found.")]
     url: Custom-class-level-attributes - 
     * http://weblogs.asp.net/peterblum/archive/2009/12/07/the-customvalidationattribute.aspx 
     * SO: 2280539/custom-model-validation-of-dependent-properties-using-data-annotations*/
    [MetadataType(typeof(UserMetadata))]
    public partial class Users 
    {//Extra properties
        public string EmailOLD { get; set; } /* Extra property used to check whether value is changed or not */
        public string LastModifiedByVal { get; set; }
        
        public const string chkDelRefMsg = "User cannot be deleted because he's linked with atleast one of the following:"+
            "<ul><li>A PO is assigned to this user.</li></ul>";
    }

    public class UserMetadata
    {
        [StringLength(50, ErrorMessage = Defaults.MaxLengthMsg)]
        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }
        
        [StringLength(80, ErrorMessage = Defaults.MaxLengthMsg)]
        [DisplayName("Email")]
        [Required(ErrorMessage = Defaults.RequiredMsg)]
        [RegularExpression(Defaults.EmailRegEx, ErrorMessage = Defaults.InvalidEmailMsg)]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage=Defaults.MaxLengthMsg)]
        [DisplayName("Password")]
        [Required(ErrorMessage = Defaults.RequiredMsg )]
        public string Password { get; set; }
                
        [DisplayName("Last Modified By")]
        public string LastModifiedBy { get; set; }

        [DisplayName("Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }
    }

    [Serializable]
    public partial class vw_Users : Users
    {
        public bool Editing { get; set; }
        public bool Edited { get; set; }

        public string LastModifiedDateTxt { get { return LastModifiedDate.ToString(Defaults.dtTFormat, Defaults.ci); } }
    }
}
