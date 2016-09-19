using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace BO_ERS
{
    public class PC_BO
    {
        public int pc_id { get; set; }
        [Required(ErrorMessage = "Enter Placement Consultant Name")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name should be alphabetic")]
        public string pc_name { get; set; }
        public string pc_password { get; set; }
        public string pc_location { get; set; }
        public string pc_email { get; set; }

    }
}
