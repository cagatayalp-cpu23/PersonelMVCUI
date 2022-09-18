using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelMVCUI.ViewModels
{
    public class TaskFormViewModel : IEnumerable
    {
        public IEnumerable<Status> Status;
        public IEnumerable<Customers> Customers;
        public IEnumerable<Personel> Personeller;
        public Tasks Tasks;
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}