using PedidosDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PedidosServices.Controllers
{
    public class UsuariosController : ApiController
    {
        public IEnumerable<USUARIOS> Get()
        {
            using (ProyectoPedidosEntities entities = new ProyectoPedidosEntities())
            {
                
                return entities.USUARIOS.ToList();
            }
        }

        public USUARIOS Get(int id)
        {
            using (ProyectoPedidosEntities entities = new ProyectoPedidosEntities())
            {
                return entities.USUARIOS.FirstOrDefault(u => u.UsuID == id);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(USUARIOS usuario)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
            using (ProyectoPedidosEntities entities = new ProyectoPedidosEntities())
            {
                
                USUARIOS u = entities.USUARIOS.Add(usuario);
                entities.SaveChanges();
                responseMessage = Request.CreateResponse(HttpStatusCode.OK, u);

            }
            

            return responseMessage;
        }

        [HttpPatch]
        public HttpResponseMessage Patch(USUARIOS usuario)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
            
            using (ProyectoPedidosEntities entities = new ProyectoPedidosEntities())
            {

                USUARIOS usuarioActualizar = entities.USUARIOS.FirstOrDefault(u => u.UsuID == usuario.UsuID);
                if(usuarioActualizar != null) { 
                    usuarioActualizar.UsuNombre = usuario.UsuNombre != null?usuario.UsuNombre:usuarioActualizar.UsuNombre;
                    usuarioActualizar.UsuPass =usuario.UsuPass != null?usuario.UsuPass:usuarioActualizar.UsuPass;
                    entities.SaveChanges();
                    responseMessage = Request.CreateResponse(HttpStatusCode.OK, usuarioActualizar);
                }
                else
                {
                    responseMessage = Request.CreateResponse(HttpStatusCode.NotFound, "Sin Codigo de Usuario o Usuario no encontrado Por favor envia algo asi: {\"UsuID\":1,\"UsuNombre\":\"Nuevo Nombre\"}");
                }

            }
            return responseMessage;

        }

        [HttpDelete]
        public HttpResponseMessage Delete(USUARIOS usuario)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);

            using (ProyectoPedidosEntities entities = new ProyectoPedidosEntities())
            {
                entities.USUARIOS.Attach(usuario);
                USUARIOS usuarioEliminar = entities.USUARIOS.Remove(usuario);
                if (usuarioEliminar == null)
                
                {
                    responseMessage = Request.CreateResponse(HttpStatusCode.NotFound, "Sin Codigo de Usuario o Usuario no encontrado Por favor envia algo asi: {\"UsuID\":1}");
                }
                else { 
                    entities.SaveChanges();
                    responseMessage = Request.CreateResponse(HttpStatusCode.NoContent);
                }

            }
            return responseMessage;

        }
    }
}
