using Login_with_Roles_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

namespace Login_with_Roles_MVC.Controllers
{
    //el autorize cuando se crea la cookie al iniciar sesion, permitira una vez estando autoprizado acceder a ese usuario a cualquier de estas acciones del controlador
    //[Authorize]  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //accion que retorna la vista de ventas (con una autorizacion para solo los administradores y supervisores)
        [Authorize(Roles = "Administrador,Supervisor")]
        public IActionResult Ventas()
        {
            return View();
        }

        //accion que retorna la vista de compras (con una autorizacion para solo los administradores y supervisores)
        [Authorize(Roles = "Administrador,Supervisor,Empleado")]
        public IActionResult Compras()
        {
            return View();
        }

        public IActionResult Clientes()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AccessDenied()
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
