using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WebApplication3.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        

        public virtual ICollection<Author> Authors { get; set; }
        public Event()
        {
            Authors = new List<Author>();
        }

    }
}