//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PedidosDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class PEDIDO
    {
        public int PedID { get; set; }
        public int PedUsu { get; set; }
        public int PedProd { get; set; }
        public decimal PedVrUnit { get; set; }
        public double PedCant { get; set; }
        public decimal PedSubTot { get; set; }
        public double PedIva { get; set; }
        public decimal PedTotal { get; set; }
    
        public virtual PRODUCTO PRODUCTO { get; set; }
        public virtual USUARIOS USUARIOS { get; set; }
    }
}
