namespace EateryPOSSystem.Models.Report
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EateryPOSSystem.Services.Models;

    public class BillsForDateFormModel
    {
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; } = DateTime.UtcNow.Date;

        public bool Closed { get; set; }

        public IEnumerable<BillServiceModel> Bills { get; set; }
    }
}
