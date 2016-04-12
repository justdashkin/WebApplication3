using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace WebApplication3.Models
{
    public class EventDbInitializer : DropCreateDatabaseAlways<AuthorsContext>
    {
        protected override void Seed(AuthorsContext context)
        {
            Author s1 = new Author { Id = 1, Name = "Егор", Surname = "Иванов" };
            Author s2 = new Author { Id = 2, Name = "Мария", Surname = "Васильева" };
            Author s3 = new Author { Id = 3, Name = "Олег", Surname = "Кузнецов" };
            Author s4 = new Author { Id = 4, Name = "Ольга", Surname = "Петрова" };

            context.Authors.Add(s1);
            context.Authors.Add(s2);
            context.Authors.Add(s3);
            context.Authors.Add(s4);

            Event c1 = new Event
            {
                Id = 1,
                Name = "MiniFootball",
                Authors = new List<Author>() { s1, s2, s3 }
            };
            Event c2 = new Event
            {
                Id = 2,
                Name = "Quest",
                Description = "Quest from ENCOUNTER",
               Authors = new List<Author>() { s2, s4 }
            };
            Event c3 = new Event
            {
                Id = 3,
                Name = "Concert",
                Authors = new List<Author>() { s3, s4, s1 }
            };

            context.Events.Add(c1);
            context.Events.Add(c2);
            context.Events.Add(c3);

            base.Seed(context);
        }

    }
}