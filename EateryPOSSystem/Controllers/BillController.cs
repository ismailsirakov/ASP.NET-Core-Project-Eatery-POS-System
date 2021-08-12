namespace EateryPOSSystem.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Models.Bill;
    using EateryPOSSystem.Infrastructure;
    using static ControllerConstants;

    [Authorize(Roles ="Administrator, Seller")]
    public class BillController : Controller
    {
        private readonly IBillService billService;
        private readonly IDbService dbService;
        public BillController(IBillService billService, IDbService dbService)
        {
            this.billService = billService;
            this.dbService = dbService;
        }

        public IActionResult Details()
        {
            var billId = (int)TempData["BillId"];

            var billl = dbService.GetBillById(billId);

            var userName = User.Identity.Name;

            var soldProducts = billService.SoldProductsByBillId(billId).ToList();

            var totalSum = 0m;

            foreach (var soldProduct in soldProducts)
            {
                totalSum += soldProduct.Quantity * soldProduct.Price;
            }

            var bill = new CloseBillFormModel
            {
                Id = billId,
                UserName = userName,
                OpenDateTime = billl.OpenDateTime,
                SoldProducts = soldProducts,
                TotalSum = totalSum
            };

            TempData["BillId"] = billId;

            return View(bill);
        }

        public IActionResult New()
        {
            var userId = User.GetId();
            
            var billId = billService.NewBill(userId);

            var storeId = (int)TempData["StoreId"];

            var tableNumber = (int)TempData["TableNumber"];

            billService.AddNewBillToTable(userId, storeId, tableNumber, billId);

            TempData["BillId"] = billId;            

            return RedirectToAction("AddSoldProductToBill", "Seller");
        }

        public IActionResult CloseBill()
        {
            var billId = (int)TempData["BillId"];

            var billl = dbService.GetBillById(billId);

            var userName = User.Identity.Name;

            var soldProducts = billService.SoldProductsByBillId(billId).ToList();

            var paymentTypes = dbService.GetPaymentTypes();

            var totalSum = 0m;

            foreach (var soldProduct in soldProducts)
            {
                totalSum += soldProduct.Quantity * soldProduct.Price;
            }

            var bill = new CloseBillFormModel
            {
                Id = billId,
                UserName = userName,
                OpenDateTime = billl.OpenDateTime,
                SoldProducts = soldProducts,
                PaymentTypes = paymentTypes,
                TotalSum = totalSum
            };

            TempData["BillId"] = billId;

            return View(bill);
        }

        [HttpPost]
        public IActionResult CloseBill(CloseBillFormModel bill)
        {
            var billId = (int)TempData["BillId"];

            var billInDb = dbService.GetBillById(billId);

            var userName = User.Identity.Name;

            var soldProducts = billService.SoldProductsByBillId(billId).ToList();

            var paymentTypes = dbService.GetPaymentTypes();

            var totalSum = 0m;

            foreach (var soldProduct in soldProducts)
            {
                totalSum += soldProduct.Quantity * soldProduct.Price;
            }

            bill.Id = billId;

            bill.UserName = userName;

            bill.OpenDateTime = billInDb.OpenDateTime;

            bill.SoldProducts = soldProducts;

            bill.PaymentTypes = paymentTypes;

            bill.TotalSum = totalSum;            

            TempData["BillId"] = billId;

            if (!paymentTypes.Any(pt=>pt.Id == bill.PaymentTypeId))
            {
                ModelState.AddModelError(nameof(bill.PaymentTypeId), notExistingModelInDB);

                return View(bill);
            }

            if (!ModelState.IsValid)
            {
                return View(bill);
            }

            billInDb.PaymentTypeId = bill.PaymentTypeId;

            billInDb.Closed = true;

            billInDb.CloseDateTime = DateTime.UtcNow;

            billService.RemoveBillFromTable(billInDb.Id);

            return RedirectToAction("ClosedBillDetails","Bill", billInDb);
        }

        public IActionResult ClosedBillDetails(CloseBillFormModel bill)
        {
            bill.SoldProducts = billService.SoldProductsByBillId(bill.Id).ToList();

            var totalSum = 0m;

            foreach (var soldProduct in bill.SoldProducts)
            {
                totalSum += soldProduct.Quantity * soldProduct.Price;
            }

            bill.TotalSum = totalSum;

            bill.PaymentTypeName = dbService.GetPaymentTypes()
                                            .FirstOrDefault(pt=>pt.Id == bill.PaymentTypeId)
                                            .Name;

            bill.UserName = User.Identity.Name;

            return View(bill);
        }
    }
}
