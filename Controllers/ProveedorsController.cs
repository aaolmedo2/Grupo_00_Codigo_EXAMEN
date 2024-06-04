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
    [Authorize]
    public class ProveedorsController : Controller
    {
        private ProyectoTiendaMVCEntities db = new ProyectoTiendaMVCEntities();

        // GET: Proveedors
        public ActionResult Index()
        {
            return View(db.Proveedor.ToList());
        }

        // GET: Proveedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: Proveedors/Create
        public ActionResult Create()
        {
            // Opciones de Ciudades
            ViewBag.Ciudades = new List<SelectListItem>
            {
                new SelectListItem { Text = "Quito", Value = "Quito" },
                new SelectListItem { Text = "Guayaquil", Value = "Guayaquil" },
                new SelectListItem { Text = "Cuenca", Value = "Cuenca" }
            };

            // Opciones de Provincias
            ViewBag.Provincias = new List<SelectListItem>
            {
                new SelectListItem { Text = "Pichincha", Value = "Pichincha" },
                new SelectListItem { Text = "Guayas", Value = "Guayas" },
                new SelectListItem { Text = "Azuay", Value = "Azuay" }
            };

            return View();
        }

        // POST: Proveedors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo_proveedor,nombre_proveedor,ciudad,provincia,Email")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Proveedor.Add(proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proveedor);
        }

        // GET: Proveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            // Opciones de Ciudades
            ViewBag.Ciudades = new List<SelectListItem>
            {
                new SelectListItem { Text = "Quito", Value = "Quito" },
                new SelectListItem { Text = "Guayaquil", Value = "Guayaquil" },
                new SelectListItem { Text = "Cuenca", Value = "Cuenca" }
            };

            // Opciones de Provincias
            ViewBag.Provincias = new List<SelectListItem>
            {
                new SelectListItem { Text = "Pichincha", Value = "Pichincha" },
                new SelectListItem { Text = "Guayas", Value = "Guayas" },
                new SelectListItem { Text = "Azuay", Value = "Azuay" }
            };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo_proveedor,nombre_proveedor,ciudad,provincia,Email")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedor proveedor = db.Proveedor.Find(id);
            db.Proveedor.Remove(proveedor);
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
