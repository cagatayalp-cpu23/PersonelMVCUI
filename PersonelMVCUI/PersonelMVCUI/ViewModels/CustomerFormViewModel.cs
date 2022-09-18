using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelMVCUI.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<Departman> Departmanlar { get; set; }
        public Customers Customers { get; set; }
    }
}