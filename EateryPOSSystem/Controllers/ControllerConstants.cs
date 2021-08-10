namespace EateryPOSSystem.Controllers
{
    public static class ControllerConstants
    {
        public const string existingCityInDB = "В базата данни съществува град със същото име.";

        public const string existingDocumentTypeInDB = "В базата данни съществува такъв тип документ.";

        public const string existingMeasurementInDB = "В базата данни съществува такава мерна единица.";

        public const string existingPaymentTypeInDB = "В базата данни съществува такъв вид плащане.";

        public const string existingPositionInDB = "В базата данни съществува такава длъжност.";

        public const string existingProductTypeInDB = "В базата данни съществува такъв тип продукт.";

        public const string existingStoreInDB = "В базата данни съществува обект с такова име.";

        public const string existingWarehouseInDB = "В базата данни съществува склад с такова име";

        public const string existingProductInDB = "В базата данни съществува такъв продукт";

        public const string existingProductInStoreInDB = "В базата данни съществува продукт с такова име в този обект";

        public const string existingAddressInDB = "В базата данни съществува такъв адрес.";

        public const string existingMaterialInDB = "В базата данни съществува материал с такова име.";

        public const string existingProviderInDB = "В базата данни съществува доставчик с такова име.";

        public const string notExistingModelInDB = "Избраният елемент не съществува в базата данни.";

        public const string existingMaterialInRecipe = "Избраният материал съществува в тази рецепта.";

        public const string warehouseCannotTransferToItself = "Склад не може да трансферира към себе си.";

        public const string greaterQuantityThenExistInWarehouse = "Трансферираното количество не може да надвишава количеството в склада.";
    }
}
