using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogsite.DTOs
{
    public class userDTO
    {

        public int id { get; set; }
        [Required]
        [StringLength(3, ErrorMessage = "Username cannot be longer than 3 characters")]
        public string username { get; set; }
        public string password { get; set; }
        [EmailAddress]
        public string email { get; set; }

    }
}