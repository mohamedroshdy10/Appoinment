using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppoinmentApp.Models.ViewModels
{
    public class DoctorViewModel
    {
        public string  Id { get; set; }
        public string   Name { get; set; }
        public string Email { get; set; }

    }
    public class PatinetViewModel:DoctorViewModel
    {

    }
}
