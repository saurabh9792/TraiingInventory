using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {

        DataLayer dl = new DataLayer();


        #region category

        [HttpGet]
        public IActionResult Categorylist()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCategory()
        {


            return View();
        }


        [HttpPost]
        public IActionResult AddCategory(AllClass obj)
        {
            DataLayer dl = new DataLayer();
            string userid = HttpContext.Session.GetString("UserdId");
            bool status = dl.InsertCategory(obj);


            return View();
        }



        #endregion

        public IActionResult SubCategorylist()
        {
            try
            {
                DataTable dt = dl.GetSubCategoryList();
                List<SubCategory> subCategoryList = new List<SubCategory>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SubCategory subCategory = new SubCategory
                        {
                            SubCcategoryId = Convert.ToInt32(dr["SubCcategoryId"]),
                            CategoryName = dr["CategoryName"].ToString(),
                            SubCcategoryName = dr["SubCategoryName"].ToString()
                        };
                        subCategoryList.Add(subCategory);
                    }
                }

                return View(subCategoryList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        public IActionResult AddEditSubcat()
        {
            DataTable dt = dl.GetAllCategoryLis();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    categoryList.Add(new SelectListItem
                    {
                        Value = row["CategoryID"].ToString(),
                        Text = row["CategoryName"].ToString()
                    });
                }


            }
            ViewBag.CategoryList = categoryList;


            return View();
        }


        [HttpPost]
        public IActionResult AddEditSubcat(SubCategory model)
        {
            DataTable dt = dl.GetAllCategoryLis();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    categoryList.Add(new SelectListItem
                    {
                        Value = row["CategoryID"].ToString(),
                        Text = row["CategoryName"].ToString()
                    });
                }


            }
            ViewBag.CategoryList = categoryList;
            model.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            bool status = dl.InsertSubCategory(model);
            if (status == true)
            {
                ViewBag.msg = "Data Save Suceefully";
            }
            else
            {
                ViewBag.msg = "Something went wrong";
            }



            return View();
        }








        [HttpGet]
        public IActionResult BrandList()
        {
            DataTable dt = dl.GetBrandDetailsLis();
            List<Brand> brandList = new List<Brand>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Brand brand = new Brand
                    {
                        BrandId = Convert.ToInt32(item["BrandId"]),
                        BrandName = item["BrandName"].ToString(),
                        BrandDescription = item["BrandDes"].ToString(),
                        BrandImagePath = item["BrandImgPath"].ToString(),
                    };
                    brandList.Add(brand);
                }
            }
            return View(brandList);
        }


        [HttpGet]
        public IActionResult AddBrand()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult>AddBrand(Brand model)
        {
            model.CreatedBy =Convert.ToInt32(HttpContext.Session.GetString("UserdId"));
            if (model.BrandImage != null && model.BrandImage.Length > 0)
            {
                string fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(model.BrandImage.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", fileName);
                string tempPath = Path.Combine("upload", fileName);


                Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.BrandImage.CopyToAsync(stream);
                }

                model.BrandImagePath = tempPath;
            }
            bool status =  dl.AddBrand(model);
            if (status == true)
            {
                ViewBag.msg = "Data Save Sucessfully";
            }
            else
            {
                ViewBag.msg = "SomeThing Went Wrong";
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditBrand(string BrandId) 
        {
            DataTable dt = dl.GetBrandDetailsById(BrandId);
            Brand model = new Brand();
            if (dt!=null && dt.Rows.Count>0)
            {
                model.BrandId = Convert.ToInt32(dt.Rows[0]["BrandId"]);
                model.BrandName = dt.Rows[0]["BrandName"].ToString();
                model.BrandDescription = dt.Rows[0]["BrandDes"].ToString();
                model.BrandImagePath = dt.Rows[0]["BrandImgPath"].ToString();              


            }

            return View(model); 
        }

        [HttpPost]
        public IActionResult EditBrand(Brand model)
        {
            model.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("UserdId"));
            if (model.BrandImage != null && model.BrandImage.Length > 0)
            {
                string fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(model.BrandImage.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", fileName);
                string tempPath = Path.Combine("upload", fileName);


                Directory.CreateDirectory(Path.GetDirectoryName(path));

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.BrandImage.CopyToAsync(stream);
                }

                model.BrandImagePath = tempPath;
            }
            bool status = dl.EditBrand(model);
            if (status == true)
            {
                ViewBag.msg = "Data Update Sucessfully";
                return RedirectToAction("BrandList");
            }
            else
            {
                ViewBag.msg = "SomeThing Went Wrong";
                return RedirectToAction("BrandList");
            }


            return View(model);
        }

        [HttpGet]
        public IActionResult ProductList()
        {



            return View();
        }


        [HttpGet]
        public IActionResult AddProduct()
        {
            DataTable dt = new DataTable();
            dt = dl.GetAllCategoryLis();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    categoryList.Add(new SelectListItem
                    {

                        Value = row["CategoryID"].ToString(),
                        Text = row["CategoryName"].ToString()
                    });


                }

            }
            ViewBag.CategoryList = categoryList;
            AllClass obj= new AllClass();
            ViewBag.msg = obj.GetAllCategory();


            return View();
        }





        [HttpPost]
        public JsonResult GetSubCategory(string cateId)
        {
            DataTable dt = dl.GetSubCategoryListByCateId(cateId);
            List<SubCategory> subcategoryList = new List<SubCategory>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    subcategoryList.Add(new SubCategory
                    {
                        SubCcategoryId = Convert.ToInt32(row["SubCateId"]),
                        SubCcategoryName = row["SubCatName"].ToString()
                    });
                }
            }

            return Json(subcategoryList);
        }


        public IActionResult AddPurchase()
        {
            return View();  
        }

        [HttpGet]
		public IActionResult Sales()
		{
            var bill = "";
			return View();
		}


        [HttpPost]
        public IActionResult SavePurchase(string data)
        {
             var msg = "";
             var model = JsonConvert.DeserializeObject<PurchaseDto>(data);
             if (model != null)
            {

                bool status = dl.InsertPurachseData(model);
                 if (status) {
                    msg = "Purchase Sucessfully Save";
                }
            }

            return Json(msg);
        }




    }


}

    