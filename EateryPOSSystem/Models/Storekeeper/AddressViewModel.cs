namespace EateryPOSSystem.Models.Storekeeper
{
    using System.Collections.Generic;
    public class AddressViewModel
    {
        public int CityId { get; init; }

        public ICollection<CityViewModel> Cities { get; set; }

        public string AddressDetails { get; init; }
    }
}
