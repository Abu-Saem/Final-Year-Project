using AutoMapper;
using StockManage.BLL.BLL;
using StockManage.DatabaseContext.DatabaseContext;
using StockManage.Models.Models;
using StockManageWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockManageWeb.Controllers
{
    public class SupplierController : Controller
    {
        StockManageSupplierManager _stockManageManger = new StockManageSupplierManager();
        StockManageDbContext Db = new StockManageDbContext();
        Supplier _supplier = new Supplier();
        SupplierVM _supplierVM = new SupplierVM();

        public ActionResult Add()
        {
            SupplierVM model = new SupplierVM();
            model.suppliers = _stockManageManger.Show();

            return View(model);
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add(SupplierVM model)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = Mapper.Map<Supplier>(model);

                HttpPostedFileBase image = Request.Files["ImageData"];

                _stockManageManger.Add(supplier, image);

            }
            _supplierVM.suppliers = _stockManageManger.Show();
            return View(_supplierVM);
        }

        [HttpPost]
        public ActionResult Show(SupplierVM model)
        {

            Supplier supplier = Mapper.Map<Supplier>(model);
            model.suppliers = _stockManageManger.Show();
            if (supplier.Name != null)
            {
                model.suppliers = Db.suppliers.Where(c => c.Name.ToLower().Contains(supplier.Name.ToLower())).ToList();
            }


            return View(model);
        }

        public ActionResult Show()
        {
            SupplierVM model = new SupplierVM();

            model.suppliers = _stockManageManger.Show();

            return View(model);
        }

        //public ActionResult Edit(int? id)
        //{
        //    SupplierVM model = new SupplierVM();
        //    model.ID = Convert.ToInt32(id);
        //    Supplier supplier = Mapper.Map<Supplier>(model);

        //    var GetByID = _stockManageManger.GetByID(supplier);
        //    //_customer.customers = _stockManageManger.Show();
        //    model.Name = GetByID.Name;
        //    model.Address = GetByID.Address;
        //    model.Code = GetByID.Code;
        //    model.Contact = GetByID.Contact;
        //    model.Email = GetByID.Email;
        //    model.ContactPerson = GetByID.ContactPerson;



        //    return View(model);
        //}


        //[HttpPost]
        //public ActionResult Edit(Supplier supplier)
        //{


        //    _supplier.Name = supplier.Name;
        //    _supplier.ID = supplier.ID;
        //    _supplier.Code = supplier.Code;
        //    _supplier.Address = supplier.Address;
        //    _supplier.Email = supplier.Email;
        //    _supplier.Contact = supplier.Contact;
        //    _supplier.ContactPerson = supplier.ContactPerson;
        //    _stockManageManger.Edit(_supplier);


        //    _supplierVM.suppliers = _stockManageManger.Show();

        //    return RedirectToAction("Add");
        //}


        //public ActionResult Delete(int? id)
        //{
        //    SupplierVM model = new SupplierVM();
        //    Supplier supplier = Mapper.Map<Supplier>(model);
        //    _supplier.ID = Convert.ToInt32(id);
        //    if (id != null)
        //    {
        //        var supplier1 = _stockManageManger.GetByID(_supplier);
        //        _stockManageManger.Delete(supplier1);
        //    }




        //    _supplierVM.suppliers = _stockManageManger.Show();
        //    return View(_supplier);

        //}

        public ActionResult Edit(int? id)
        {
            SupplierVM model = new SupplierVM();
            model.ID = Convert.ToInt32(id);
            Supplier supplier = Mapper.Map<Supplier>(model);

            var GetByID = _stockManageManger.GetByID(supplier);
            //_customer.customers = _stockManageManger.Show();
            model.Name = GetByID.Name;
            model.Address = GetByID.Address;
            model.Code = GetByID.Code;
            model.Contact = GetByID.Contact;
            model.Email = GetByID.Email;
            model.ContactPerson = GetByID.ContactPerson;



            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SupplierVM model)
        {

            Supplier supplier = Mapper.Map<Supplier>(model);



            _stockManageManger.Edit(supplier);

            model.suppliers = _stockManageManger.Show();
            ViewBag.message = "Updated";
            return RedirectToAction("Add");
        }


        public ActionResult Delete(int? id)
        {
            SupplierVM model = new SupplierVM();
            model.ID = Convert.ToInt32(id);
            Supplier supplier = Mapper.Map<Supplier>(model);

            if (id != null)
            {
                var aCustomer = _stockManageManger.GetByID(supplier);
                _stockManageManger.Delete(aCustomer);
            }

            _supplierVM.suppliers = _stockManageManger.Show();
            return RedirectToAction("Add");

        }


    }
}