using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PyoyectoTest.Models;

namespace PyoyectoTest.Controllers
{
    [Authorize(Users = "SuperAdmin")]
    public class AspNetRolesController : Controller
    {
        private ProyectoTiendaMVCEntities db = new ProyectoTiendaMVCEntities();

        // GET: AspNetRoles
        public ActionResult Index()
        {
            // Lista de nombres de roles que no quieres que aparezcan en la vista
            var excludedRoles = new List<string> { "Create", "Delete", "Edit", "Details" };

            // Filtrar los roles excluyendo los mencionados
            var roles = db.AspNetRoles.Where(r => !excludedRoles.Contains(r.Name)).ToList();

            return View(roles);

        }

        // GET: AspNetRoles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            if (aspNetRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetRoles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AspNetRoles aspNetRoles)
        {
            if (ModelState.IsValid)
            {
                db.AspNetRoles.Add(aspNetRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Edit/5
        public ActionResult Edit(string id)
        {

            //ProyectoTiendaMVCEntities db = new ProyectoTiendaMVCEntities();

            ////PAra el dropdownlist de paciente y medico
            //ViewBag.clientes = new SelectList(db.Clientes, "ClienteID", "Nombre");
            //ViewBag.proveedores = new SelectList(db.Proveedor, "codigo_proveedor", "nombre_proveedor");


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            if (aspNetRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRoles);
        }

        // POST: AspNetRoles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AspNetRoles aspNetRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetRoles);
        }

        // GET: AspNetRoles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            if (aspNetRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetRoles);
        }

        // POST: AspNetRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            db.AspNetRoles.Remove(aspNetRoles);
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

        // GET: VerRoles/Asignar/5
        public ActionResult Asignar(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = db.AspNetRoles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        // POST: VerRoles/Asignar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Asignar(AspNetRoles role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }


    }
}
