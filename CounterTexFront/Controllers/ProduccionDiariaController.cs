using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using CounterTexFront.Models;
using Newtonsoft.Json;

namespace CounterTexFront.Controllers
{
    public class ProduccionDiariaController : BaseController
    {
        private readonly string baseUrl = ConfigurationManager.AppSettings["Api"];

    }
}
