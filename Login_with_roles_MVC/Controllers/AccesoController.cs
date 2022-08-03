using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//importamos los modelos
using Login_with_Roles_MVC.Models;
using Login_with_Roles_MVC.Data;

//importamos librerias para hacer uso de las cookies y autorizaciones
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Login_with_Roles_MVC.Controllers
{
    public class AccesoController : Controller
    {
        //accion que retornara la vista principal de accesoController
        public IActionResult Index()
        {
            return View();
        }

        //metodo que recibira la accion del post del form y verificara los datos, por parametro se veriica que se envie un usuario
        [HttpPost]
        public async Task<IActionResult> Index(Usuario _usuario)
        {
            //instanciamos la clase de logica para hacer uso de ella
            ApplicationDbContext _da_usuario = new ApplicationDbContext();

            //validamos 
            var usuario = _da_usuario.ValidarUsuario(_usuario.Correo, _usuario.Clave);
            if(usuario != null)
            {
                //CREAMOS UNA COOKIE Y TODO SU ESQUEMA DE AUTORIZACION PARA EL USUARIO//

               //PASAMOS TODO EL ESQUEMA DEL USUARIO (nombre, correo, roles) dentro de un esquema predefinido de logeo
               //almacenamos el nombre, correo y todo los roles que necesitaremos en esa cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("Correo", usuario.Correo)
                };
                //recorremos todas las propiedades de la lista de roles para ver que permisos tiene
                foreach(string rol in usuario.Roles)
                {
                    //le ingresamos a claim todos los roles existentes
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //creamos la cookie (es asincrono) y con esto confirmamos que esta autorizado (para poder utilizar en [Authorize])
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                //si encuentra al usuario y sus datos son correctos, lo manda al index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //si no encuentra al usuario se vuelve a redirigir a la opcion de inicio de sesion
                return View();
            }
        }

        public async Task<IActionResult> Salir()
        {
            //eliminamos la cookie creada anteriormente al cerrar la sesion (invocar este metodo de salir) (ya no estara autorizado)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //nos reedirigimos a l apagina de logeo nuevamente
            return RedirectToAction("Index", "Acceso");
        }
    }
}
