namespace EateryPOSSystem.Models.Storekeeper
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddPaymentTypeFormModel
    {
        [Required]
        [StringLength(PaymentTypeNameMaxLength, MinimumLength = PaymentTypeNameMinLength)]
        public string Name { get; init; }
    }
}
