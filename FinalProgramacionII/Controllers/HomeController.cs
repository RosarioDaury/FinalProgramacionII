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
                        data = context.Entidades.FromSqlRaw("EXEC ACCESO {0}, {1}", credenciales.UserNameEntidad, credenciales.PasswordEntidad).ToList();
                        //Si Hay data Log in, Si No Pagina con el Error
                        if(data.Count > 0)
                        {
                            HttpContext.Session.SetString("Username", credenciales.UserNameEntidad);
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
                        //Ejecutar procedure que guarda nuevo USUARIO Y CLAVE
                        var results = context.Database.ExecuteSqlRaw("EXEC REGISTRAR {0}, {1}", data.Username.Trim(), data.Password.Trim());
                    }
                    //Luego de guardar de nuevo para el LOGIN
                    return Redirect("/Home/Login");
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
                data = context.Entidades.FromSqlRaw("EXEC GETALL").ToList();
            }
            //Data del query como parametro a la vista (data se usa en la vista)
            ModeloMain dat = new ModeloMain();
            dat.data = data;
            dat.username = HttpContext.Session.GetString("Username");
            return View(dat);
        }

        //Controlador para la vista que muestra informacion completa de un registro en especifico
        public IActionResult Detalles(int id)
        {
            List<Entidade> model;
            using(var context =new SellPointContext())
            {
                //procedure para conseguir un record en especifico
                model = context.Entidades.FromSqlRaw("EXEC GETONE {0}", id).ToList();
            }
            //retornar la visto con la data del registro pasado por parametro
            return View(model);
        }

        //Controller to edit record (this one send to the view with the form)
        public IActionResult Edit(int id)
        {
            List<Entidade> toEdit;
            using (var context = new SellPointContext())
            {
                toEdit = context.Entidades.FromSqlRaw("EXEC GETONE {0}", id).ToList();
            }
            return View(toEdit[0]);
        }

        //Controller to manage the post request of the Edit
        [HttpPost]
        public IActionResult Edit(Entidade data)
        {
            //Validate the Limite de credito is more than 0 cannot be 0 or less
            if (data.LimiteCredito > 0)
            {
                //Updating Record (Using the MVC Functionality allows to not expose query
                //and is better to control than a store procedure (This query even as a procedure 
                //turns out to be huge and take to many paramenters)
                using(var context = new SellPointContext())
                {
                    var toEdit = context.Entidades.Find(data.IdEntidades);
                    toEdit.Descripcion = data.Descripcion;
                    toEdit.Direccion = data.Direccion;
                    toEdit.Localidad = data.Localidad;
                    toEdit.TipoEntidad = data.TipoEntidad;
                    toEdit.TipoDocumento = data.TipoDocumento;
                    toEdit.NumeroDocumento = data.NumeroDocumento;
                    toEdit.Telefonos = data.Telefonos;
                    toEdit.UrlpaginaWeb = data.UrlpaginaWeb;
                    toEdit.Urlfacebook = data.Urlfacebook;
                    toEdit.Urlinstagram = data.Urlinstagram;
                    toEdit.Urltwitter = data.Urltwitter;
                    toEdit.UrltikTok = data.UrltikTok;
                    toEdit.CodigoPostal = data.CodigoPostal;
                    toEdit.CoordenadasGps = data.CoordenadasGps;
                    toEdit.LimiteCredito = data.LimiteCredito;
                    toEdit.UserNameEntidad = data.UserNameEntidad;
                    toEdit.PasswordEntidad = data.PasswordEntidad;
                    toEdit.RolUserEntidad = data.RolUserEntidad;
                    toEdit.Comentario = data.Comentario;
                    toEdit.Status = data.Status;
                    toEdit.NiEliminable = data.NiEliminable;
                    toEdit.FechaRegistro = data.FechaRegistro;

                    context.Entry(toEdit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                return Redirect("/Home/Detalles/" + data.IdEntidades);
            }
            return Redirect("/Home/EditError/" + data.IdEntidades);
        }

        //View for the Error of the Edit Controller
        public IActionResult EditError(int id)
        {
            List<Entidade> toEdit;
            using (var context = new SellPointContext())
            {
                toEdit = context.Entidades.FromSqlRaw("EXEC GETONE {0}", id).ToList();
            }
            return View(toEdit[0]);
        }

        //Controller to delete one record
        public IActionResult Delete(int id)
        {
            try
            {
                using(var context = new SellPointContext())
                {
                    //Delete record with the ID especified, is not possible to get an ID 
                    //that doesnt exist cause the delete botton is inside the record detail
                    var toDelete = context.Entidades.Find(id);
                    if(toDelete != null)
                    {
                        context.Entidades.Remove(toDelete);
                        context.SaveChanges();
                    }
                }
                //After Deleted the record Redirect to the View with all the records
                return Redirect("/Home/Main");
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        //Controller "Salir" Botton
        public IActionResult SalirHome()
        {
            return Redirect("/Home");
        }

        //Controller para information Page
        public IActionResult About()
        {
            return View();
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
