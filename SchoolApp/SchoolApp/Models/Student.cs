namespace SchoolApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class Student : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Second name")]
        public string SecondName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime BirthDate { get; set; }

        public IEnumerable<Grade> Grades { get; set; }

        public string Display
            => Name + " " + (SecondName != null ? Surname : "") + " " + Surname;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var nameRegex = new Regex(@"^([A-Z]{1,}|\d)[A-Za-z0-9 ]*$");

            if (!nameRegex.IsMatch(Name) || !nameRegex.IsMatch(Surname))
            {
                yield return new ValidationResult("Name and Surname must contain only letters and start with big case. ",
                    new[] { nameof(Name), nameof(Surname) });
            }

            if (!string.IsNullOrWhiteSpace(SecondName) && !nameRegex.IsMatch(SecondName))
            {
                yield return new ValidationResult("Second name must contain only letters and start with big case. ",
                    new[] { nameof(SecondName) });
            }
        }
    }
}
