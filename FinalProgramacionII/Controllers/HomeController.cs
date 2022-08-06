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

        public IActionResult Main()
        {
            List<Entidade> data;
            using(var context = new SellPointContext())
            {
                data = context.Entidades.FromSqlRaw("SELECT * FROM dbo.Entidades").ToList();
            }
            return View(data);
        }

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