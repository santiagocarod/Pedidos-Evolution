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

    /*
     Para agregar un nuevo pedido solo es necesario 
    PedUsu
    PedProd
    PedCant
    PedIVA

    el resto se pueden calcular
     */
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

        // PUT: api/pedidos/1
        [HttpPut]
        public HttpResponseMessage Patch(int id, PEDIDO pedido)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);

            using (ProyectoPedidosEntities entities = new ProyectoPedidosEntities())
            {

                PEDIDO pedidoActualizar = entities.PEDIDO.First(u => u.PedID == id);
                if (pedidoActualizar != null)
                {
                    
                    pedidoActualizar.PedIVA = pedido.PedIVA != 0 ? pedido.PedIVA : pedidoActualizar.PedIVA;
                    pedidoActualizar.PedCant = pedido.PedCant != 0 ? pedido.PedCant : pedidoActualizar.PedCant;
                    pedidoActualizar.PedProd = pedido.PedProd != 0 ? pedido.PedProd : pedidoActualizar.PedProd;
                    pedidoActualizar.PedUsu = pedido.PedUsu != 0 ? pedido.PedUsu : pedidoActualizar.PedUsu;
                    String resultado = verificarValores(ref pedidoActualizar);
                    if (!resultado.Equals(""))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, resultado);
                    }
                    entities.SaveChanges();
                    responseMessage = Request.CreateResponse(HttpStatusCode.OK, pedidoActualizar);
                }
                else
                {
                    responseMessage = Request.CreateResponse(HttpStatusCode.NotFound, "PEDIDO NO ENCONTRADO");
                }

            }
            return responseMessage;

        }

        // POST: api/pedidos
        [HttpPost]
        public HttpResponseMessage Post(PEDIDO pedido)
        {
            
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);

            if (pedido.PedCant == 0.0 || pedido.PedIVA == 0.0 || pedido.PedProd == 0 || pedido.PedUsu == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "FALTA INFORMACIÓN: " + (pedido.PedCant == 0.0 ? " PedCant " : "") + (pedido.PedIVA == 0.0 ? " PedIVA " : "") + (pedido.PedProd == 0 ? " PedProd " : "") + (pedido.PedUsu == 0 ? " PedUsu " : ""));
            }

            using (ProyectoPedidosEntities entities = new ProyectoPedidosEntities())
            {

                String resultado = verificarValores(ref pedido);
                if (!resultado.Equals(""))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, resultado);
                }
                PEDIDO p = entities.PEDIDO.Add(pedido);
                entities.SaveChanges();
                responseMessage = Request.CreateResponse(HttpStatusCode.OK, p);

            }


            return responseMessage;
        }

        private String verificarValores(ref PEDIDO pedido)
        {
            int codProd = pedido.PedProd;
            int codUsu = pedido.PedUsu;

            using (ProyectoPedidosEntities entities = new ProyectoPedidosEntities())
            {
                PRODUCTO producto = entities.PRODUCTO.FirstOrDefault(u => u.ProID == codProd);
                USUARIOS usuario = entities.USUARIOS.FirstOrDefault(u => u.UsuID == codUsu);

                if(producto == null)
                {
                    return "PRODUCTO NO ENCONTRADO";
                }

                if(usuario == null)
                {
                    return "USUARIO NO ENCONTRADO";
                }

                pedido.PedVrUnit = (decimal)producto.ProValor;

                pedido.PedSubTot = pedido.PedVrUnit * (decimal)pedido.PedCant;

                pedido.PedTotal = pedido.PedSubTot + (pedido.PedSubTot * (decimal)pedido.PedIVA);
            }
                

            return "";
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