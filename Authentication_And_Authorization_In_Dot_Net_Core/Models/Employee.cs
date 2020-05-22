using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication_And_Authorization_In_Dot_Net_Core.Models
{
    public class Employee
    {

        [Column(TypeName = "varchar(30)")]
        public String Name { set; get; }
        [Key]
        [Column(TypeName="varchar(30)")]
        [Display(Name="Email Address")]
        [DataType(DataType.EmailAddress)]
        public String Email { set; get; }

        [Column(TypeName = "varchar(30)")]
        public String Address { set; get; }

        [Column(TypeName = "varchar(30)")]
        [Display(Name="Phone Number")]
        public String Phone { set; get; }
        [Column(TypeName = "varchar(30)")]
        [DataType(DataType.Password)]
        public String Password { set; get; }
       
        [NotMapped]
        [Compare("Password",ErrorMessage ="Password Doesn't Match")]
        [DataType(DataType.Password)]
        [Display(Name="Confrim Password")]
        public String ConfrimPassword { set; get; }


    }
}
