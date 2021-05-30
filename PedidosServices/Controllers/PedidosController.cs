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
    public class PedidosController : ApiController
    {
        private ProyectoPedidosEntities db = new ProyectoPedidosEntities();

        // GET: api/Pedidos
        public IQueryable<PEDIDO> GetPEDIDO()
        {
            return db.PEDIDO;
        }

        // GET: api/Pedidos/5
        [ResponseType(typeof(PEDIDO))]
        public IHttpActionResult GetPEDIDO(int id)
        {
            PEDIDO pEDIDO = db.PEDIDO.Find(id);
            if (pEDIDO == null)
            {
                return NotFound();
            }

            return Ok(pEDIDO);
        }

        // PUT: api/Pedidos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPEDIDO(int id, PEDIDO pEDIDO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pEDIDO.PedID)
            {
                return BadRequest();
            }

            db.Entry(pEDIDO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PEDIDOExists(id))
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

        // POST: api/Pedidos
        [ResponseType(typeof(PEDIDO))]
        public IHttpActionResult PostPEDIDO(PEDIDO pEDIDO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PEDIDO.Add(pEDIDO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PEDIDOExists(pEDIDO.PedID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pEDIDO.PedID }, pEDIDO);
        }

        // DELETE: api/Pedidos/5
        [ResponseType(typeof(PEDIDO))]
        public IHttpActionResult DeletePEDIDO(int id)
        {
            PEDIDO pEDIDO = db.PEDIDO.Find(id);
            if (pEDIDO == null)
            {
                return NotFound();
            }

            db.PEDIDO.Remove(pEDIDO);
            db.SaveChanges();

            return Ok(pEDIDO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PEDIDOExists(int id)
        {
            return db.PEDIDO.Count(e => e.PedID == id) > 0;
        }
    }
}