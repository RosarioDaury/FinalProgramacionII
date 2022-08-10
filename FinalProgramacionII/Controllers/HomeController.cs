﻿using FinalProgramacionII.Models;
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
                        context.Entidades.FromSqlRaw("EXEC REGISTRAR {0}, {1}", data.Username.Trim(), data.Password.Trim());
                    }
                    //Luego de guardar de nuevo para el LOGIN
                    return Redirect("Home/Login");
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
            return View(data);
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
            return View(toEdit);
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
                    context.Entidades.FromSqlRaw("EXEC DEL {0}", id);
                }
                //After Deleted the record Redirect to the View with all the records
                return Redirect("Home/Main");
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