using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dnorwoodShoppingApp1.Models.CodeFirst
{
    public class Item
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set;}
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string MediaURL { get; set; }

        [AllowHtml]
        public string Description { get; set; }

    }
}