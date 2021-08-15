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

            var currentBill = dbService.GetBillById(billId);

            var user = dbService.GetUsers().FirstOrDefault(u=>u.UserId == currentBill.UserId);

            var userBadge = user.FirstName + " " + user.LastName;

            var soldProducts = billService.SoldProductsByBillId(billId).ToList();

            var totalSum = 0m;

            foreach (var soldProduct in soldProducts)
            {
                totalSum += soldProduct.Quantity * soldProduct.Price;
            }

            var bill = new CloseBillFormModel
            {
                Id = billId,
                UserId = currentBill.UserId,
                UserBadge = userBadge,
                OpenDateTime = currentBill.OpenDateTime,
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

            var currentBill = dbService.GetBillById(billId);

            var user = dbService.GetUsers().FirstOrDefault(u => u.UserId == currentBill.UserId);

            var userBadge = user.FirstName + " " + user.LastName;

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
                UserId = currentBill.UserId,
                UserBadge = userBadge,
                OpenDateTime = currentBill.OpenDateTime,
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

            var currentBill = dbService.GetBillById(billId);

            var user = dbService.GetUsers().FirstOrDefault(u => u.UserId == currentBill.UserId);

            var userBadge = user.FirstName + " " + user.LastName;

            var soldProducts = billService.SoldProductsByBillId(billId).ToList();

            var paymentTypes = dbService.GetPaymentTypes();

            var totalSum = 0m;

            foreach (var soldProduct in soldProducts)
            {
                totalSum += soldProduct.Quantity * soldProduct.Price;
            }

            bill.Id = billId;

            bill.UserId = currentBill.UserId;

            bill.UserBadge = userBadge;

            bill.OpenDateTime = currentBill.OpenDateTime;

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

            currentBill.PaymentTypeId = bill.PaymentTypeId;

            currentBill.Closed = true;

            currentBill.CloseDateTime = DateTime.UtcNow;

            billService.RemoveBillFromTable(currentBill.Id);

            return RedirectToAction("ClosedBillDetails","Bill", currentBill);
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
            var currentBill = dbService.GetBillById(bill.Id);

            var user = dbService.GetUsers().FirstOrDefault(u => u.UserId == currentBill.UserId);

            var userBadge = user.FirstName + " " + user.LastName;

            bill.UserBadge = userBadge;

            return View(bill);
        }
    }
}
