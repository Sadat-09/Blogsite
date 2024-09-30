using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogsite.DTOs
{
    public class commentDTO
    {
        public int id { get; set; }
        public string contents { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}