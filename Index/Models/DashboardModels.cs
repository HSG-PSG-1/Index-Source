using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using HSG.DAL;
using HSG.Services;

namespace HSG.DAL
{
    [Serializable]
    public partial class vw_Dashboard
    {
        public List<Category> categories { get; set; }
        public List<Website> websites { get; set; }

    }

    public class catItem
    {
        public Category catg { get; set; }
        public List<Website> websites { get; set; }
        public int webCount {
            get { return this.websites.Count; }
        }
    }
}

