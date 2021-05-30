using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PedidosDataAccess;

namespace PedidosServices.Controllers
{
    public class ProductosController : ApiController
    {
        private ProyectoPedidosEntities db = new ProyectoPedidosEntities();

        // GET: api/Productos
        public IQueryable<PRODUCTO> GetPRODUCTO()
        {
            return db.PRODUCTO;
        }

        // GET: api/Productos/5
        [ResponseType(typeof(PRODUCTO))]
        public IHttpActionResult GetPRODUCTO(int id)
        {
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

            return Ok(pRODUCTO);
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPRODUCTO(int id, PRODUCTO pRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pRODUCTO.ProID)
            {
                return BadRequest();
            }

            db.Entry(pRODUCTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRODUCTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Productos
        [ResponseType(typeof(PRODUCTO))]
        public IHttpActionResult PostPRODUCTO(PRODUCTO pRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PRODUCTO.Add(pRODUCTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pRODUCTO.ProID }, pRODUCTO);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(PRODUCTO))]
        public IHttpActionResult DeletePRODUCTO(int id)
        {
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

            db.PRODUCTO.Remove(pRODUCTO);
            db.SaveChanges();

            return Ok(pRODUCTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PRODUCTOExists(int id)
        {
            return db.PRODUCTO.Count(e => e.ProID == id) > 0;
        }
    }
}