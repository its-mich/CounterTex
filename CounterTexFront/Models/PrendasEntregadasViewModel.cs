﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CounterTexFront.Models
{
    public class PrendasEntregadasViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Color { get; set; }
        public int CantidadPrendas { get; set; }
    }
}