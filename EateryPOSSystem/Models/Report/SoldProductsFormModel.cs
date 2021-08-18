namespace EateryPOSSystem.Models.Report
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EateryPOSSystem.Services.Models;

    public class SoldProductsFormModel
    {

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; } = DateTime.UtcNow.Date;

        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; } = DateTime.UtcNow.Date;

        public bool Cumilative { get; set; }

        public IEnumerable<SoldProductServiceModel> SoldProducts { get; set; }

        public IEnumerable<StoreServiceModel> Stores { get; set; }
    }
}
