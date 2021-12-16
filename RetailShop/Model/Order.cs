using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RetailShop.Model
{
    [Table("Orders")]
    public class Order
    {
        [Key] public int Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateTime { get; set; }

        public Order()
        {

        }

        public Order(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
