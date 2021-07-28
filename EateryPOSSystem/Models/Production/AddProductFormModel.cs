namespace EateryPOSSystem.Models.Production
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class AddProductFormModel
    {
        public string Name { get; set; }

        public int ProductTypeId { get; set; }

        public IEnumerable<ProductTypeServiceModel> ProductTypes { get; set; }
    }
}
