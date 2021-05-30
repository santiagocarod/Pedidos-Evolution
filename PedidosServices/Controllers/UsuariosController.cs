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
            using (PedidosEntities entities = new PedidosEntities())
            {
                return entities.USUARIOS.ToList();
            }
        }

        public USUARIOS Get(int id)
        {
            using (PedidosEntities entities = new PedidosEntities())
            {
                return entities.USUARIOS.FirstOrDefault(u => u.UsuID == id);
            }
        }
    }
}
