using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using PyoyectoTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PyoyectoTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ApplicationDbContext db = new ApplicationDbContext();
            //Crear los Roles
            CrearRoles(db);
            //Crear el SuperUsuario
            CrearSuperUsuario(db);

            //Asignar Permisos
            AsignarPermisos(db);
            db.Dispose();
        }

        private void CrearRoles(ApplicationDbContext db)
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!rolemanager.RoleExists("Create"))
            {
                rolemanager.Create(new IdentityRole("Create"));
            }
            if (!rolemanager.RoleExists("Edit"))
            {
                rolemanager.Create(new IdentityRole("Edit"));
            }

            if (!rolemanager.RoleExists("Details"))
            {
                rolemanager.Create(new IdentityRole("Details"));
            }

            if (!rolemanager.RoleExists("Delete"))
            {
                rolemanager.Create(new IdentityRole("Delete"));
            }

            if (!rolemanager.RoleExists("SuperAdmin"))
            {
                rolemanager.Create(new IdentityRole("SuperAdmin"));
            }

            if (!rolemanager.RoleExists("Administrador"))
            {
                rolemanager.Create(new IdentityRole("Administrador"));
            }

            if (!rolemanager.RoleExists("Proveedor"))
            {
                rolemanager.Create(new IdentityRole("Proveedor"));
            }

            if (!rolemanager.RoleExists("Cliente"))
            {
                rolemanager.Create(new IdentityRole("Cliente"));
            }

        }

        private void CrearSuperUsuario(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user0 = userManager.FindByName("SuperAdmin");

            if (user0 == null)
            {
                user0 = new ApplicationUser
                {
                    UserName = "SuperAdmin",
                    Email = "superAdmin@hotmail.com",
                    PhoneNumber = "1753898111",
                    PasswordHash = new PasswordHasher().HashPassword("123")
                };
                userManager.Create(user0);
            }

        }

        private void AsignarPermisos(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = userManager.FindByName("SuperAdmin");

            if (!userManager.IsInRole(user.Id, "Create"))
            {
                userManager.AddToRole(user.Id, "Create");
            }
            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                userManager.AddToRole(user.Id, "Edit");
            }
            if (!userManager.IsInRole(user.Id, "Details"))
            {
                userManager.AddToRole(user.Id, "Details");
            }
            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                userManager.AddToRole(user.Id, "Delete");
            }

            if (!userManager.IsInRole(user.Id, "SuperAdmin"))
            {
                userManager.AddToRole(user.Id, "SuperAdmin");
            }


            // Asegurarnos de que el usuario AGENTE existe
            var user2 = userManager.FindByName("Administrador");
            if (user2 == null)
            {
                user2 = new ApplicationUser
                {
                    UserName = "Administrador",
                    Email = "administrador@hotmail.com",
                    PasswordHash = new PasswordHasher().HashPassword("123")
                };
                userManager.Create(user2);// Puedes cambiar la contraseĆ±a por una segura            }

            }
            if (!userManager.IsInRole(user2.Id, "Administrador"))
            {
                userManager.AddToRole(user2.Id, "Administrador");
            }
        }
    }
}
