namespace EateryPOSSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Models.Bill;
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;
    using System;

    public class BillController : Controller
    {
        private readonly IBillService billService;
        public BillController(IBillService billService)
        {
            this.billService = billService;
        }

        public IActionResult Details()
        {
            var bill = new BillViewModel
            {
                Id = 35644,
                UserName = "PESHO",
                PaymentTypeName = "В брой",
                OpenDateTime = DateTime.UtcNow,
                CloseDateTime = DateTime.UtcNow.AddMinutes(25),
                Closed = false,
                SoldProducts = new List<SoldProductServiceModel>
                {
                    new SoldProductServiceModel
                    {
                        BillId = 123,
                        StoreProductName = "Kola 250ml",
                        Quantity = 2m,
                        Price = 3.5m,
                        MeasurementName = "br."
                    },
                    new SoldProductServiceModel
                    {
                        BillId = 123,
                        StoreProductName = "Fanta 250ml",
                        Quantity = 3m,
                        Price = 3.2m,
                        MeasurementName = "br."
                    },
                    new SoldProductServiceModel
                    {
                        BillId = 123,
                        StoreProductName = "Kafe",
                        Quantity = 6m,
                        Price = 4.5m,
                        MeasurementName = "br."
                    },
                    new SoldProductServiceModel
                    {
                        BillId = 123,
                        StoreProductName = "Krema",
                        Quantity = 4m,
                        Price = 0.5m,
                        MeasurementName = "br."
                    }
                },
                TotalSum = 42.25m
            };

            return View(bill);
        }

        public IActionResult Add()
        {


            return View();
        }
    }
}
