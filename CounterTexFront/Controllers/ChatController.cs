using CounterTexFront.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class ChatController : BaseController
    {

        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();
        private readonly HttpClient _httpClient;


        public ChatController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["Api"].ToString());
        }
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Chat()
        {
            // ✅ Validar que la sesión existe
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var remitente = (LoginResponse)Session["Usuario"];
            var remitenteId = remitente.Id;

            // ✅ Obtener lista de usuarios excluyendo el remitente
            var usuariosResponse = await _httpClient.GetAsync("api/MensajesChat/usuarios");
            if (!usuariosResponse.IsSuccessStatusCode)
            {
                ViewBag.Error = "No se pudieron cargar los usuarios.";
                ViewBag.Usuarios = new SelectList(new List<UsuarioViewModel>());
                return View(new ChatViewModel());
            }

            var usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(await usuariosResponse.Content.ReadAsStringAsync())
                           .Where(u => u.Id != remitenteId)
                           .ToList();

            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nombre");
            ViewBag.RemitenteId = remitenteId;
            return View(new ChatViewModel());
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerMensajes(int remitenteId, int destinatarioId)
        {
            var response = await _httpClient.GetAsync($"api/MensajesChat/historial/{remitenteId}/{destinatarioId}");
            if (!response.IsSuccessStatusCode)
                return new HttpStatusCodeResult(400, "Error al obtener los mensajes");

            var mensajes = JsonConvert.DeserializeObject<List<MensajeChatDTO>>(await response.Content.ReadAsStringAsync());
            return Json(mensajes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> EnviarMensaje(MensajeChatDTO mensaje)
        {
            mensaje.FechaHora = DateTime.Now;
            var content = new StringContent(JsonConvert.SerializeObject(mensaje), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/MensajesChat", content);
            return Json(response.IsSuccessStatusCode);
        }
    }
}
