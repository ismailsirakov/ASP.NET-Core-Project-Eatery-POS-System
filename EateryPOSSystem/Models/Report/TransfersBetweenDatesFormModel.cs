namespace EateryPOSSystem.Models.Report
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EateryPOSSystem.Services.Models;

    public class TransfersBetweenDatesFormModel
    {
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; } = DateTime.UtcNow.Date;

        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; } = DateTime.UtcNow.Date;

        public int MaterialId { get; set; }

        public string MaterialName { get; set; }

        public IEnumerable<MaterialServiceModel> Materials { get; set; }

        public IEnumerable<TransferServiceModel> Transfers { get; set; }
    }
}
