﻿using DIARS_PROYECTO_FINAL.BD;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIARS_PROYECTO_FINAL.Controllers
{
    public class ProductoController : Controller
    {
        public StoreContext context = StoreContext.getInstance();

        [Authorize]
        public ActionResult Index()
        {
            //1 = Admin
            //2 = User
            var usuuario = (Usuario)Session["Usuario"];
            if (usuuario.IdRol != 2){
                var productos = context.Productos.Include(a => a.Categoria).ToList();
                return View(productos);
            }
            else {
                return Redirect("~/");
            }
        }
       
        public ActionResult Especificaciones(int ID)
        {
            Producto producto = context.Productos.Find(ID);

            return View(producto);
        }
        [Authorize]
        public ActionResult Crear()
        {
            var usuuario = (Usuario)Session["Usuario"];
            if (usuuario.IdRol != 2)
            {
                ViewBag.Categoria = context.Categorias;
                return View(new Producto());
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult Crear(Producto producto, HttpPostedFileBase file)
        {
            var usuuario = (Usuario)Session["Usuario"];
            if (usuuario.IdRol != 2 && usuuario.IdRol != null)
            {
                ViewBag.Categoria = context.Categorias;
                if (file != null && file.ContentLength > 0)
                {
                    string ruta = Path.Combine(Server.MapPath("~/imagenes"), Path.GetFileName(file.FileName));
                    file.SaveAs(ruta);
                    producto.imagen = "/imagenes/" + Path.GetFileName(file.FileName);
                }
                validar(producto);
                if (ModelState.IsValid)
                {
                    producto.fecha = DateTime.Now;
                    producto.isActive = true;
                    context.Productos.Add(producto);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(producto);
            }
            else {
                return Redirect("~/");
            }
        }

        public ActionResult Editar(int ID)
        {
            var producto = context.Productos.Where(o => o.Id == ID).First();
            return View(producto);
        }

        [HttpPost]
        public ActionResult Editar(Producto producto)
        {
            try
            { if (ModelState.IsValid)
                {
                    context.Entry(producto).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return View(producto);
            }
            return View(producto);
        }

        public ActionResult Eliminar(int ID)
        {
            Producto producto = context.Productos.Where(x => x.Id == ID).First();
            context.Productos.Remove(producto);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public void validar(Producto producto)
        {
            if (producto.nombre == null || producto.nombre == "")
            {
                ModelState.AddModelError("Nombre", "Ingrese nombre del producto");
            }
            
            if (producto.nombre  == null || producto.nombre == "")
            {
                ModelState.AddModelError("Nombre", "Ingrese nombre del producto");
            }

            if (producto.stock.ToString() == null || producto.stock.ToString() == "")
            {
                ModelState.AddModelError("Stock", "Ingrese Stock del producto");
            }
            if (producto.modelo == null || producto.modelo == "")
            {
                ModelState.AddModelError("Modelo", "Ingrese Modelo del producto");
            }
            if (producto.marca == null || producto.marca == "")
            {
                ModelState.AddModelError("Marca", "Ingrese Marca del producto");
            }
            if (producto.talla.ToString() == null || producto.talla.ToString() == "")
            {
                ModelState.AddModelError("Talla", "Ingrese Talla del producto");
            }

            if (producto.imagen == null || producto.imagen== "")
            {
                ModelState.AddModelError("Imagen", "Ingrese imagen del producto");
            }
        }
    }
}
