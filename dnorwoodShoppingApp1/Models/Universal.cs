using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dnorwoodShoppingApp1.Models
{
    public class Universal : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                ViewBag.FirstName = user.FirstName;
                ViewBag.LastName = user.LastName;
                ViewBag.FullName = user.FullName;
                ViewBag.CartItems = user.CartItems;
                ViewBag.TotalCartItems = db.CartItems.Where(c => c.CustomerId == user.Id).ToList();
                ViewBag.TotalCartItems = user.CartItems.Sum(c => c.Count);
                decimal Total = 0;
                foreach(var cartItem in db.CartItems.Where(c => c.CustomerId == user.Id).Include("Item"))
                {
                    Total += cartItem.Count * cartItem.Item.Price;
                }
                ViewBag.CartTotal = Total;

                base.OnActionExecuting(filterContext);
            }
        }
    }
}