//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockTracking.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SALES
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public int CategoryID { get; set; }
        public int ProductSalesAmount { get; set; }
        public int ProductSalesPrice { get; set; }
        public System.DateTime SalesDate { get; set; }
        public bool isDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}
