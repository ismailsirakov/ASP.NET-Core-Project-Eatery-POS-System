namespace EateryPOSSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public interface IBillService
    {
        IEnumerable<SoldProductServiceModel> SoldProductsByBillId(int billId);
    }
}
