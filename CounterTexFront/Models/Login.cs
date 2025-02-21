using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;

namespace CounterTexFront.Front.Models
{
    public class Login
    {
        [Key]
        [DisplayName("Username")]
        
        public string Username { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El Campo {0} Es Requerido")]

        public string Password { get; set; }
    }
}