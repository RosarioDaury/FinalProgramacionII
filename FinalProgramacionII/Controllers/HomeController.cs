using FinalProgramacionII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace FinalProgramacionII.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //LA Applicacion entra por default por este controller, aqui mostramos el splash
        //redirigimos al login (inicio de sesion)
        public IActionResult Index()
        {
            return View();
        }

        //Controller para inicio de session
        public IActionResult Login()
        {
            return View();
        }

        //Proceso de logeo (Luego de llenar el formulario y clikear iniciar sesion)
        [HttpPost]
        public IActionResult Login(Login credenciales)
        {
            List<Entidade> data; //PARA GUARDAR DATA DEL QUERY
            try
            {
                //Validar que los inputs complen las validaciones
                if (ModelState.IsValid)
                {
                    //Conexion con el Contexto (Conexion a la base de datos)
                    using(SellPointContext context = new SellPointContext())
                    {
                        //Query (Recuerda hacer store procedure desde la DB)
                        data = context.Entidades.FromSqlRaw("SELECT * FROM dbo.Entidades WHERE UserNameEntidad = {0}", credenciales.UserNameEntidad).ToList();
                        //Si Hay data Log in, Si No Pagina con el Error
                        if(data.Count > 0)
                        {
                            return Redirect("/Home/Main");
                        }
                    }
                }

                //Pagina del ERROR.
                return Redirect("/Home/LoginError");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Pagina del Login Con el mensaje de ERROR
        public IActionResult LoginError()
        {
            return View();
        }

        //Controlador para la pagina de registro, Crear un nuevo username y Clave
        public IActionResult Registrarse()
        {
            return View();
        }

        //Controlador para registrar nuevo usuario antes de logearse
        [HttpPost]
        public IActionResult Registrarse(Registrar data)
        {
            //Instacia de la clase que sirve de modelo de la tabla
            Entidade modelo = new Entidade();

            try
            {
                //Si formulario es valido continuar
                if (ModelState.IsValid)
                {
                    using(SellPointContext context = new SellPointContext())
                    {
                        //la tabla tiene filas requeridas asi que hay que llenar los requeridos
                        //porque no hay una tabla aparte solo para el username y password
                        modelo.Descripcion = "Pendiente Requerido";
                        modelo.Direccion = "Pendiente Requerido";
                        modelo.Localidad = "";
                        modelo.TipoEntidad = "";
                        modelo.TipoDocumento = "";
                        modelo.NumeroDocumento = 001;
                        modelo.Telefonos = "Pendiente Requerido";
                        modelo.UrlpaginaWeb = "";
                        modelo.Urlfacebook = "";
                        modelo.Urlinstagram = "";
                        modelo.Urltwitter = "";
                        modelo.UrltikTok = "";
                        modelo.CodigoPostal = "";
                        modelo.CoordenadasGps = "";
                        modelo.LimiteCredito = 1;
                        modelo.UserNameEntidad = data.Username;
                        modelo.PasswordEntidad = data.Password;
                        modelo.RolUserEntidad = "";
                        modelo.Comentario = "";
                        modelo.Status = "";
                        modelo.NiEliminable = true;
                        modelo.FechaRegistro = DateTime.Today;

                        //Agregar registro y guardar
                        context.Entidades.Add(modelo);
                        context.SaveChanges();
                    }
                    //Luego de guardar de nuevo para el LOGIN
                    return Redirect("/Login");
                }
                return View(data);
            }
            catch(Exception excp)
            {
                throw new Exception(excp.Message);     
            }

        }

        //Pagina principal del CRUD (Info minima, opcion de ver mas informacion)
        public IActionResult Main()
        {
            //var para guardar data del query
            List<Entidade> data;
            using(var context = new SellPointContext())
            {
                //Query, resultado guardado en data
                data = context.Entidades.FromSqlRaw("SELECT * FROM dbo.Entidades").ToList();
            }
            //Data del query como parametro a la vista (data se usa en la vista)
            return View(data);
        }

        //Controller para boton "Salir"
        public IActionResult SalirHome()
        {
            return Redirect("/Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}