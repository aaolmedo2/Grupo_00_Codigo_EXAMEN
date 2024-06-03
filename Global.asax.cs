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
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!roleManager.RoleExists("SuperAdmin"))
            {
                roleManager.Create(new IdentityRole("SuperAdmin"));
            }

            if (!roleManager.RoleExists("Administrador"))
            {
                roleManager.Create(new IdentityRole("Administrador"));
            }

            if (!roleManager.RoleExists("Proveedor"))
            {
                roleManager.Create(new IdentityRole("Proveedor"));
            }

            if (!roleManager.RoleExists("Cliente"))
            {
                roleManager.Create(new IdentityRole("Cliente"));
            }
        }

        private void CrearSuperUsuario(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("SuperAdmin");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "superAdmin",
                    Email = "superAdmin@hotmail.com"
                };
                var result = userManager.Create(user, "123");
            }
        }

        private void AsignarPermisos(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //var user = userManager.FindByName("SuperAdmin");
            //if (!userManager.IsInRole(user.Id, "SuperAdmin"))
            //{
            //    userManager.AddToRole(user.Id, "SuperAdmin");
            //}

        }
    }
}
