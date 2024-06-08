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
    public class PedidosItemsController : Controller
    {
        private ProyectoTiendaMVCEntities db = new ProyectoTiendaMVCEntities();

        // GET: PedidosItems
        public ActionResult Index()
        {
            var pedidosItems = db.PedidosItems.Include(p => p.Pedidos).Include(p => p.Productos);
            return View(pedidosItems.ToList());
        }

        // GET: PedidosItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidosItems pedidosItems = db.PedidosItems.Find(id);
            if (pedidosItems == null)
            {
                return HttpNotFound();
            }
            return View(pedidosItems);
        }

        // GET: PedidosItems/Create
        public ActionResult Create()
        {
            ViewBag.PedidoID = new SelectList(db.Pedidos, "PedidoID", "PedidoID");
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "NombreProducto");
            return View();
        }

        // POST: PedidosItems/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidoItemID,PedidoID,ProductoID,Cantidad")] PedidosItems pedidosItems)
        {
            if (ModelState.IsValid)
            {
                db.PedidosItems.Add(pedidosItems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PedidoID = new SelectList(db.Pedidos, "PedidoID", "PedidoID", pedidosItems.PedidoID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "NombreProducto", pedidosItems.ProductoID);
            return View(pedidosItems);
        }

        // GET: PedidosItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidosItems pedidosItems = db.PedidosItems.Find(id);
            if (pedidosItems == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoID = new SelectList(db.Pedidos, "PedidoID", "PedidoID", pedidosItems.PedidoID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "NombreProducto", pedidosItems.ProductoID);
            return View(pedidosItems);
        }

        // POST: PedidosItems/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoItemID,PedidoID,ProductoID,Cantidad")] PedidosItems pedidosItems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidosItems).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidoID = new SelectList(db.Pedidos, "PedidoID", "PedidoID", pedidosItems.PedidoID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "NombreProducto", pedidosItems.ProductoID);
            return View(pedidosItems);
        }

        // GET: PedidosItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidosItems pedidosItems = db.PedidosItems.Find(id);
            if (pedidosItems == null)
            {
                return HttpNotFound();
            }
            return View(pedidosItems);
        }

        // POST: PedidosItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidosItems pedidosItems = db.PedidosItems.Find(id);
            db.PedidosItems.Remove(pedidosItems);
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
