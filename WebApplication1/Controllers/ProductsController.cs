using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        private trainEntities1 db = new trainEntities1();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Catagory).Include(p => p.Color).Include(p => p.Size);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Product_Brandid = new SelectList(db.Brands, "Brand_id", "Brand_name");
            ViewBag.Product_Colors = new SelectList(db.Catagories, "Catagory_id", "Catagory_name");
            ViewBag.Product_Colors = new SelectList(db.Colors, "Color_id", "Color_Name");
            ViewBag.Product_sizeid = new SelectList(db.Sizes, "Size_Id", "Size_name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_id,Product_name,Product_qty,Product_price,Product_desc,Product_sizeid,Product_Catagoryid,Product_Colors,Product_Brandid")] Product product,HttpPostedFileBase file2)
        {
            Random random = new Random();
            int num = random.Next(1, 999999999);

            String fileName = Path.GetFileName(file2.FileName);
            fileName = num + fileName;
            String filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
            file2.SaveAs(filePath);
            product.Product_image= "../Images/" + fileName;

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_Brandid = new SelectList(db.Brands, "Brand_id", "Brand_name", product.Product_Brandid);
            ViewBag.Product_Colors = new SelectList(db.Catagories, "Catagory_id", "Catagory_name", product.Product_Colors);
            ViewBag.Product_Colors = new SelectList(db.Colors, "Color_id", "Color_Name", product.Product_Colors);
            ViewBag.Product_sizeid = new SelectList(db.Sizes, "Size_Id", "Size_name", product.Product_sizeid);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_Brandid = new SelectList(db.Brands, "Brand_id", "Brand_name", product.Product_Brandid);
            ViewBag.Product_Colors = new SelectList(db.Catagories, "Catagory_id", "Catagory_name", product.Product_Colors);
            ViewBag.Product_Colors = new SelectList(db.Colors, "Color_id", "Color_Name", product.Product_Colors);
            ViewBag.Product_sizeid = new SelectList(db.Sizes, "Size_Id", "Size_name", product.Product_sizeid);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_id,Product_name,Product_qty,Product_price,Product_desc,Product_image,Product_sizeid,Product_Catagoryid,Product_Colors,Product_Brandid")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product_Brandid = new SelectList(db.Brands, "Brand_id", "Brand_name", product.Product_Brandid);
            ViewBag.Product_Colors = new SelectList(db.Catagories, "Catagory_id", "Catagory_name", product.Product_Colors);
            ViewBag.Product_Colors = new SelectList(db.Colors, "Color_id", "Color_Name", product.Product_Colors);
            ViewBag.Product_sizeid = new SelectList(db.Sizes, "Size_Id", "Size_name", product.Product_sizeid);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
