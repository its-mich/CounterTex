using CounterTexFront.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterTexFront.Models
{
    public class Tokens
    {
        [Key]

        public string TokenValue { get; set; }

        public string Rol { get; set; }

    }
}
