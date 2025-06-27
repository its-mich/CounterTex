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
    /// <summary>
    /// Controlador encargado de la lógica del chat entre usuarios en la aplicación.
    /// Permite listar usuarios, obtener conversaciones y enviar mensajes.
    /// </summary>
    public class ChatController : BaseController
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor que inicializa el HttpClient con la URL base del backend.
        /// </summary>
        public ChatController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };
        }

        /// <summary>
        /// Acción que retorna la vista principal del chat.
        /// </summary>
        /// <returns>Vista vacía inicial del módulo de chat</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Acción que prepara y devuelve la vista del chat con los usuarios disponibles.
        /// Excluye al usuario actual de la lista de destinatarios.
        /// </summary>
        /// <returns>Vista con el modelo <see cref="ChatViewModel"/> y lista de usuarios</returns>
        public async Task<ActionResult> Chat()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Login", "Auth");

            var remitente = (LoginResponse)Session["Usuario"];
            var remitenteId = remitente.Id;

            var usuariosResponse = await _httpClient.GetAsync("api/Usuarios/GetUsuarios");
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

        /// <summary>
        /// Obtiene los mensajes entre un remitente y un destinatario.
        /// </summary>
        /// <param name="remitenteId">ID del usuario que envía</param>
        /// <param name="destinatarioId">ID del usuario que recibe</param>
        /// <returns>Lista de mensajes en formato JSON</returns>
        [HttpGet]
        public async Task<ActionResult> ObtenerMensajes(int remitenteId, int destinatarioId)
        {
            try
            {
                string endpoint = (remitenteId == 0 && destinatarioId > 0)
                    ? $"api/MensajesChat/Conversacion?remitenteId={destinatarioId}&destinatarioId={destinatarioId}"
                    : $"api/MensajesChat/Conversacion?remitenteId={remitenteId}&destinatarioId={destinatarioId}";

                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var mensajes = JsonConvert.DeserializeObject<List<MensajeChatDTO>>(contenido);
                    return Json(mensajes, JsonRequestBehavior.AllowGet);
                }

                return new HttpStatusCodeResult((int)response.StatusCode, "Error al obtener mensajes del backend.");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Envia un mensaje desde un usuario a otro.
        /// </summary>
        /// <returns>Resultado en formato JSON indicando éxito o error</returns>
        [HttpPost]
        public async Task<ActionResult> EnviarMensaje()
        {
            try
            {
                Request.InputStream.Position = 0;
                using (var reader = new System.IO.StreamReader(Request.InputStream))
                {
                    var json = await reader.ReadToEndAsync();
                    if (string.IsNullOrWhiteSpace(json))
                        return new HttpStatusCodeResult(400, "El cuerpo del mensaje está vacío.");

                    var mensaje = JsonConvert.DeserializeObject<MensajeChatDTO>(json);
                    if (mensaje == null || string.IsNullOrWhiteSpace(mensaje.Mensaje))
                        return new HttpStatusCodeResult(400, "El mensaje no puede ser nulo.");

                    mensaje.FechaHora = DateTime.Now;

                    var jsonEnviar = JsonConvert.SerializeObject(mensaje, new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    });

                    var content = new StringContent(jsonEnviar, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync("api/MensajesChat/EnviarMensaje", content);

                    if (response.IsSuccessStatusCode)
                        return Json(true);

                    var error = await response.Content.ReadAsStringAsync();
                    return new HttpStatusCodeResult((int)response.StatusCode, $"Error del backend: {error}");
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
