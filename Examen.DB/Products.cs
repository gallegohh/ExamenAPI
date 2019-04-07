using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen.DB
{
    public partial class Products
    {
        public Products()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        public int ProductID { get; set; }

        [Display(Name = "Nombre")]
        public string ProductName { get; set; }

        [Display(Name = "ID del Distribuidor")]
        public int? SupplierID { get; set; }

        [Display(Name = "ID de la Categoría")]
        public int? CategoryID { get; set; }

        [Display(Name = "Cantidad por unidad")]
        public string QuantityPerUnit { get; set; }

        [Display(Name = "Precio Unitario")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Unidades en Stock")]
        public short? UnitsInStock { get; set; }

        [Display(Name = "Unidades en Pedido")]
        public short? UnitsOnOrder { get; set; }

        [Display(Name = "Nivel de Reorder")]
        public short? ReorderLevel { get; set; }

        [Display(Name = "Discontinuo")]
        public bool Discontinued { get; set; }

        [Display(Name = "Categoría")]
        public Categories Category { get; set; }

        [Display(Name = "Distribuidor")]
        public Suppliers Supplier { get; set; }

        [Display(Name = "Detalles de pedido")]
        public ICollection<Order_Details> Order_Details { get; set; }
    }
}
