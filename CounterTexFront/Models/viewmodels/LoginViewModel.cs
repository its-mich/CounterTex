using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RunGymFront.Models.ViewModels
{
	public class LoginViewModel
	{
    
            [Required]
            [Display(Name = "Correo electrónico")]
            [EmailAddress]
            public string Correo { get; set; }

            [Required]
            [Display(Name = "Contraseña")]
            [DataType(DataType.Password)]
            public string Contraseña { get; set; }
        


    }
}