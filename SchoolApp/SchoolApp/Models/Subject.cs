namespace SchoolApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class Subject : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 6, ErrorMessage = "Description length must be between 6 and 64 letters. ")]
        public string Description { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Range length must be between 4 and 16 letters. ")]
        public string Range { get; set; }

        public IEnumerable<Grade> Grades { get; set; }

        public string Display
            => Name + " " + Range;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var nameRegex = new Regex(@"^([A-Z]{1,}|\d)[A-Za-z0-9 ]*$");

            if (!nameRegex.IsMatch(Name))
            {
                yield return new ValidationResult("Name must contain only letters and start with big case. ",
                    new[] { nameof(Name) });
            }
        }
    }
}
