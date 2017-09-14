using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dnorwoodShoppingApp1;
using dnorwoodShoppingApp1.Models;
using Microsoft.AspNet.Identity;
using dnorwoodShoppingApp1.Models.CodeFirst;

namespace dnorwoodShoppingApp1.Controllers
{
    public class CartItemsController : Universal
    {
        // GET: CartItems
        [Authorize]
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(db.CartItems.ToList());
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var user = db.Users.Find(User.Identity.GetUserId());
            if (user.CartItems.Any(c => c.ItemId == id))
            {
                var existingCartItem = user.CartItems.FirstOrDefault(c => c.ItemId == id);
                existingCartItem.Count += 1;
                db.SaveChanges();
            }
            else
            {
               
                CartItem cartItem = new CartItem();
                cartItem.Count = 1;
                cartItem.ItemId = id.Value;
                cartItem.CreationDate = System.DateTime.Now;
                cartItem.CustomerId = user.Id;
                db.CartItems.Add(cartItem);
                db.SaveChanges();
                
            }

           return RedirectToAction("Index","Items");
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemId,CustomerId,Count,CreationDate")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                CartItem cart = db.CartItems.Find(cartItem.Id);
                cart.ItemId = cartItem.ItemId;
                cart.CustomerId = cartItem.CustomerId;
                cart.Count = cartItem.Count;
                cart.CreationDate = cartItem.CreationDate;


                //db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
