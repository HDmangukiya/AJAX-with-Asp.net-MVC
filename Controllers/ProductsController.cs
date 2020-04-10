using CRUD_with_AJAX.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_with_AJAX.Controllers
{
    public class ProductsController : Controller
    {
        private InventoryEntities emp = new InventoryEntities();
        public static List<Product> emplist = new List<Product>();
        
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public bool SubmitData(Product obj)
        {
            try
            {
                obj.CratedDate = DateTime.Now;
                emp.Products.Add(obj);
                emp.SaveChanges();
                //emplist.Add(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public JsonResult GetAll()
        {
            emplist = emp.Products.ToList();
            return Json(emplist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int ID)
        {
            var emps = emplist.Find(x => x.ProductId.Equals(ID));
            return Json(emps, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Product Products)
        {
            if (ModelState.IsValid)
            {
                emp.Entry(Products).State = EntityState.Modified;
                emp.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Products);
        }

        public JsonResult Update(Product emps)
        {
            emps.CratedDate = DateTime.Now;
            emp.Entry(emps).State = EntityState.Modified;
            emp.SaveChanges();
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            Product Products = emp.Products.Find(ID);
            emp.Products.Remove(Products);
            emp.SaveChanges();
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

    }
}


