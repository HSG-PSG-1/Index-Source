using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using HSG.Helper;
using System.Runtime.Serialization;

namespace HSG.Models
{
    public class LogInModel
    {// Log in model used to capture user data
        [Required(ErrorMessage = Defaults.RequiredMsg)]
        [DisplayName("Email")]
        [StringLength(80, ErrorMessage = Defaults.MaxLengthMsg)]
        public string Email { get; set; }

        [Required(ErrorMessage = Defaults.RequiredMsg)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [StringLength(20, ErrorMessage = Defaults.MaxLengthMsg)]
        public string Password { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }
    }
    
    public class Lookup
    {// Used by dropdown type of lookup fields
        public string id { get; set; }
        public string value { get; set; }
    }

}