using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBloodApi3.Models
{
    public class BloodModel
    {

        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Characters must be at least 4 ")]
        public string Username { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email in not valid")]
        public string Email { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
        public string Location { get; set; }
        public string BloodGroup { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
        public int Date { get; set; }
        public Guid userId { get; set; }
        [NotMapped]
        public Byte[] ImageArray { get; set; }
    }
}