using System.ComponentModel.DataAnnotations;
namespace ELearningApp.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        [StringLength(100, ErrorMessage = "The title must not exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The end date is required.")]
        [DateComparison("StartDate", ErrorMessage = "The end date must be later than the start date.")]
        public DateTime EndDate { get; set; }

        [StringLength(500, ErrorMessage = "The description must not exceed 500 characters.")]
        public string Description { get; set; }
    }

}


public class DateComparisonAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateComparisonAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = (DateTime?)value;
        var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (comparisonProperty == null)
            throw new ArgumentException($"Property with name {_comparisonProperty} not found.");

        var comparisonValue = (DateTime?)comparisonProperty.GetValue(validationContext.ObjectInstance);

        if (currentValue != null && comparisonValue != null && currentValue <= comparisonValue)
        {
            return new ValidationResult(ErrorMessage ?? "The date must be later than the comparison date.");
        }

        return ValidationResult.Success;
    }
}
