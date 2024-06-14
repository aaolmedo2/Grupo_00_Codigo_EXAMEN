using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PyoyectoTest.Models;

namespace PyoyectoTest.Controllers
{
    public class AspNetUsersController : Controller
    {
        private ProyectoTiendaMVCEntities db = new ProyectoTiendaMVCEntities();
        // GET: AspNetUsers
        public ActionResult Index()
        {
            string query = @"
                            SELECT u.Id AS UserId, u.UserName, ISNULL(r.Name, 'Sin Rol') AS RoleName, u.Email
                            FROM AspNetUsers u
                            LEFT JOIN AspNetUserRoles ur ON u.Id = ur.UserId
                            LEFT JOIN AspNetRoles r ON ur.RoleId = r.Id";

            var usersWithRoles = db.Database.SqlQuery<UserViewModel>(query).ToList();

            return View(usersWithRoles);
        }


        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            // Obtener la lista de roles
            var roles = db.AspNetRoles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            // Inicializar una nueva instancia de AspNetUsers con valores predeterminados y una contraseña hasheada
            AspNetUsers aspNetUsers = new AspNetUsers
            {
                Id = Guid.NewGuid().ToString(),
                EmailConfirmed = false,
                PasswordHash = new PasswordHasher().HashPassword("123"), // Contraseña predeterminada
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEndDateUtc = null,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };


            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,UserName,EmailConfirmed,SecurityStamp,PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] AspNetUsers aspNetUsers, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                // Generar un nuevo GUID para el Id en caso de que no se haya asignado correctamente
                aspNetUsers.Id = string.IsNullOrEmpty(aspNetUsers.Id) ? Guid.NewGuid().ToString() : aspNetUsers.Id;

                // Asignar nuevamente la contraseña predeterminada hasheada
                aspNetUsers.PasswordHash = new PasswordHasher().HashPassword("123");

                //GEnerar SecurityStamp
                aspNetUsers.SecurityStamp = string.IsNullOrEmpty(aspNetUsers.SecurityStamp) ? Guid.NewGuid().ToString() : aspNetUsers.SecurityStamp;

                // Agregar el nuevo usuario a la base de datos y guardar los cambios
                db.AspNetUsers.Add(aspNetUsers);
                db.SaveChanges();


                // Asignar roles seleccionados al usuario
                if (selectedRoles != null)
                {
                    foreach (var roleId in selectedRoles)
                    {
                        var role = db.AspNetRoles.Find(roleId);
                        aspNetUsers.AspNetRoles.Add(role);
                    }
                    db.SaveChanges();
                }


                return RedirectToAction("Index");
            }

            // Si algo sale mal, recargar la lista de roles
            var roles = db.AspNetRoles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }

            // Obtener la lista de roles
            var roles = db.AspNetRoles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            // Obtener los roles asignados al usuario
            var userRoles = aspNetUsers.AspNetRoles.Select(r => r.Id).ToArray();
            ViewBag.UserRoles = userRoles;

            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,UserName")] AspNetUsers aspNetUsers, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                // Obtener el usuario actual de la base de datos
                var user = db.AspNetUsers.Find(aspNetUsers.Id);

                if (user == null)
                {
                    return HttpNotFound();
                }

                // Actualizar los campos del usuario
                user.Email = aspNetUsers.Email;
                user.UserName = aspNetUsers.UserName;

                // Eliminar todos los roles asignados actualmente
                user.AspNetRoles.Clear();

                // Asignar los roles seleccionados
                if (selectedRoles != null)
                {
                    foreach (var roleId in selectedRoles)
                    {
                        var role = db.AspNetRoles.Find(roleId);
                        if (role != null)
                        {
                            user.AspNetRoles.Add(role);
                        }
                    }
                }

                // Guardar cambios en la base de datos
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Si algo sale mal, recargar la lista de roles
            var roles = db.AspNetRoles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            // Obtener los roles asignados al usuario
            var userRoles = aspNetUsers.AspNetRoles.Select(r => r.Id).ToArray();
            ViewBag.UserRoles = userRoles;

            return View(aspNetUsers);
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
