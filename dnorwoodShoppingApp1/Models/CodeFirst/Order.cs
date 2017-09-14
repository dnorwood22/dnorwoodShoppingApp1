using dnorwoodShoppingApp1.Models;
using dnorwoodShoppingApp1.Models.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dnorwoodShoppingApp1
{
    public class Order
    {
        public int Id { get; set;}
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public bool Completed { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        
        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}