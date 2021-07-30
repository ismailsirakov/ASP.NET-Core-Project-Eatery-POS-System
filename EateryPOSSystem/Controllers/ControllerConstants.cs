namespace EateryPOSSystem.Controllers
{
    public static class ControllerConstants
    {
        public const string existingModelInDB = "Този елемент съществува в базата данни.";

        public const string notExistingModelInDB = "Избраният елемент не съществува в базата данни.";

        public const string existingMaterialInRecipe = "Избраният материал съществува в тази рецепта.";

        public const string fromWarehouseAndToWarehouseAreTheSame = "Склад не може да трансферира към себе си.";

        public const string greaterQuantityThenExistInWarehouse = "Трансферираното количество не може да надвишава количеството в склада.";
    }
}
