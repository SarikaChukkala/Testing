using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private List<Product> products = new List<Product>();

        public enum productType { Cat1, Cat2, Cat3 };
        public enum productSubType { SubCat1, SubCat12, SubCat13,SubCat21, SubCat22, SubCat31 };

        //
        // GET: /Product/
        public ActionResult GetProducts()
        {
            products = GetDummyData();

            //var products = new List<Product>
            //{
            //new Product{ProductID=1,ProductName="Pencil",ProductQuantity=10},
            //new Product{ProductID=2,ProductName="Eraser",ProductQuantity=20},
            //new Product{ProductID=3,ProductName="Scale",ProductQuantity=30},
            //new Product{ProductID=4,ProductName="ColurBox",ProductQuantity=40}
            //};
            SetViewBagProductType(productType.Cat1);

            return View(products.ToList());
        }

         // GET: Product/Details/5
        public ActionResult Details(int id)
        {
             //var products = new List<Product>
            products = GetDummyData();
           
            Product product = products.Where(p => p.ProductID == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            products = GetDummyData();
            Product product = products.Where(p => p.ProductID == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to 
         [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id)
        {
            products = GetDummyData();
            var productToUpdate = products.Where(p => p.ProductID == id).FirstOrDefault();

            if (TryUpdateModel(productToUpdate, "",
               new string[] { "ProductName", "ProductQuantity"}))
            {
                try
                {
                     return RedirectToAction("GetProducts");
                }
                catch
                {
                }
            }
            return View(productToUpdate);
        }

         // GET: Student/Create
         public ActionResult Create()
         {
             FillProductType();
             SetViewBagProductType(productType.Cat1);
             return View();
         }

         // POST: Student/Create
         // To protect from overposting attacks, please enable the specific properties you want to bind to
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Create([Bind(Include = "ProductName, ProductQuantity")]Product product)
         {
             try
             {
                // SetViewBagProductType(productCategories.Cat1);
                 if (ModelState.IsValid)
                 {
                     products.Add(product);
                    
                    // db.SaveChanges();
                     return RedirectToAction("GetProducts");
                 }
             }
             catch 
             {
                }
             return View(products);
         }



        private List<Product> GetDummyData()
        {
            var products = new List<Product>
            {
            new Product{ProductID=1,ProductName="Pencil",ProductQuantity=10,ProductTypeID =1},
            new Product{ProductID=2,ProductName="Eraser",ProductQuantity=20,ProductTypeID =2},
            new Product{ProductID=3,ProductName="Scale",ProductQuantity=30,ProductTypeID =3},
            new Product{ProductID=4,ProductName="ColurBox",ProductQuantity=40,ProductTypeID =1}
            };

            return products.ToList();
        }


        private void SetViewBagProductType(productType selectedProduct)
        {

            IEnumerable<productType> values =

                              Enum.GetValues(typeof(productType))

                              .Cast<productType>();

            IEnumerable<SelectListItem> items =

                from value in values

                select new SelectListItem

                {

                    Text = value.ToString(),

                    Value = value.ToString(),

                    Selected = value == selectedProduct,

                };

            ViewBag.ProductType = items;

        }

        private void FillProductType()
        {
            List<SelectListItem> productTypeDropdown = new List<SelectListItem>();
              
            IEnumerable<productType> values =

                             Enum.GetValues(typeof(productType))

                             .Cast<productType>();

            foreach (var value in values)
            {
                productTypeDropdown.Add(new SelectListItem
                {
                    
                    Text = value.ToString(),
                    Value = value.ToString(),

                });
            }

            ViewBag.ProductType = productTypeDropdown;
            
            //selected value
            int intProductId = 1;
            FillSubProductType(intProductId);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult FillSubProductType(int intProductId)
        {
            List<SelectListItem> productSubTypeDropdown = new List<SelectListItem>();
            
            IEnumerable<productSubType> values =

                             Enum.GetValues(typeof(productSubType))

                             .Cast<productSubType>().Where(subtype => subtype.ToString().Contains("SubCat" + intProductId));

            foreach (var value in values)
            {
                productSubTypeDropdown.Add(new SelectListItem
                {

                    Text = value.ToString(),
                    Value = value.ToString(),

                });
            }

            ViewBag.ProductSubType = productSubTypeDropdown;

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(productSubTypeDropdown);
            return Json(result, JsonRequestBehavior.AllowGet);

       }

	}
}