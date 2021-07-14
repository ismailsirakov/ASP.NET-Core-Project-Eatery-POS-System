namespace EateryPOSSystem.Models.Storekeeper
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddDocumentTypeFormModel
    {
        [Required]
        [StringLength(DocumentTypeNameMaxLength, MinimumLength = DocumentTypeNameMinLength)]
        public string Name { get; init; }
    }
}
