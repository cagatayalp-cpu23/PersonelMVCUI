using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonelMVCUI.Models.EntityFramework;

namespace PersonelMVCUI.ViewModels
{
    public class SorunViewModel
    {
        public IEnumerable<Status> Status;
        public IEnumerable<Customers> Customers;
        public Sorun Sorun;

    }
}