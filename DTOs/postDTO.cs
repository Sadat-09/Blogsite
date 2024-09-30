using Blogsite.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogsite.DTOs
{
    public class postDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public virtual ICollection<commentDTO> comments { get; set; }

        public postDTO()
        {
            comments = new List<commentDTO>();
        }
    }
}