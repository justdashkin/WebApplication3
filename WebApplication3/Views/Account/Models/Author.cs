using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Author
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public Author()
        {
            Events = new List<Event>();
        }

    }
}