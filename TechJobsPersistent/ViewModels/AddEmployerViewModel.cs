using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Data;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        public int Id { get; set;  }

        [Required(ErrorMessage = "Employer Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

              

    }
}
