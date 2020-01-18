namespace SchoolApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Grade
    {
        public int Id { get; set; }

        [Required]
        [Range(1.0, 6.0)]
        public float GradeValue { get; set; }

        [StringLength(64, MinimumLength = 6, ErrorMessage = "Description length must be between 6 and 64 letters. ")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Creation date")]
        public DateTime Date { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Display(Name = "Student")]
        public Student Student { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Display(Name = "Subject")]
        public Subject Subject { get; set; }

    }
}
