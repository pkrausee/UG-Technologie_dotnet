using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public double Value { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }



    }
}
