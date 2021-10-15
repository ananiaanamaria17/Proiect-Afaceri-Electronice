using Newtonsoft.Json;
using Proiect_Online_Shopping_Anania_Anamaria.Database;
using Proiect_Online_Shopping_Anania_Anamaria.Models;
using Proiect_Online_Shopping_Anania_Anamaria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_Online_Shopping_Anania_Anamaria.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitofWork();
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecords();
            foreach(var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<Category> allcategories = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecordsIQuerayble().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }
        public ActionResult UpdateCategory(int CategoryId)
        {
            CategoryDetail cd;
            if (CategoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(CategoryId)));

            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);
        }
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Product>().GetProduct());
        }
        public ActionResult ProductEdit(int ProductId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Product>().GetFirstOrDefault(ProductId));
        }
        [HttpPost]
        public ActionResult ProductEdit(Product tbl,HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                file.SaveAs(path);
            }
            tbl.ProductImage =file!=null ? pic: tbl.ProductImage;
            tbl.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Product>().Update(tbl);
                return RedirectToAction("Product");
        }

        public ActionResult ProductAdd()

        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Product tbl,HttpPostedFileBase file)
        {
            string pic = null;
            if(file!= null)
            {
                 pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Product>().Add(tbl);
            return RedirectToAction("Product");
        }
        public ActionResult CategoryEdit(int CatId)
        {
            
            return View(_unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(CatId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Category tbl)
        {
            
            _unitOfWork.GetRepositoryInstance<Category>().Update(tbl);
            return RedirectToAction("Categories");
        }
    }



}