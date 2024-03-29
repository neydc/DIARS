﻿using DIARS_PROYECTO_FINAL.BD.Maps;
using DIARS_PROYECTO_FINAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DIARS_PROYECTO_FINAL.BD
{
    public class StoreContext:DbContext
    {
        private static StoreContext  context;
        private StoreContext() { }
        public static StoreContext getInstance()
        {
            if (context == null ) {
                context = new StoreContext();
            }
            return context;
            
        }
        public IDbSet<Categoria> Categorias { get; set; }
        public IDbSet<MetodoPago> metodoPagos { get; set; }
        public IDbSet<Oferta> Ofertas { get; set; }
        public IDbSet<Usuario>  Usuarios{ get; set; }
        public IDbSet<Producto> Productos { get; set; }
        public IDbSet<Direcciones> Direccioness{ get; set; }
        public IDbSet<Carousel> Carousels{ get; set; }
        public IDbSet<Carrito> Carritos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new CategoriaMap());
            modelBuilder.Configurations.Add(new MetodoPagoMap());
            modelBuilder.Configurations.Add(new OfertaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new ProductoMap());
            modelBuilder.Configurations.Add(new DireccionesMap());
            modelBuilder.Configurations.Add(new CarouselMap());
            modelBuilder.Configurations.Add(new CarritoMap());
        }
    }
    
}