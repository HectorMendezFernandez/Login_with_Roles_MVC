using Login_with_Roles_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_with_Roles_MVC.Data
{
    //esta clase se puede manejar con entity framework para conectarla a una base de datos real, de momento se probara con datos quemados
    public class ApplicationDbContext
    {

        //metodo que retorna una lista con datos quemados de usuarios
        public List<Usuario> ListaUsuario()
        {
            return new List<Usuario>
            {
                new Usuario{Nombre = "Francisco", Correo="jose@gmail.com", Clave="123", Roles=new string[]{"Administrador"} },
                new Usuario{Nombre = "Milena", Correo="maria@gmail.com", Clave="321", Roles=new string[]{"Supervisor"} },
                new Usuario{Nombre = "Cristina", Correo="cristina@gmail.com", Clave="123", Roles=new string[]{ "Empleado" } },
                new Usuario{Nombre = "Juan", Correo="juan@gmail.com", Clave="123", Roles=new string[]{"Asistente", "Administrador"} }
            };
        }

        //metodo que validara si los datos ingresados son correctos y existentes
        public Usuario ValidarUsuario(string _correo, string _clave)
        {
            return ListaUsuario().Where(item => item.Correo == _correo && item.Clave == _clave).FirstOrDefault();
        }
    }
}
