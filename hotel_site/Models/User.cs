using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace hotel_site.Models
{
    public class User : IdentityUser
    {
        public string LastName { get; set; }
        //public int? BookId { get; set; }
        //public virtual Book Book { get; set; }
        //public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public User(string userName, string lastName) : base(userName)
        {
            LastName = lastName;
        }

        //public void SetBook(Book book)
        //{
        //    if (Book != null)
        //        throw new Exception("Ошибка. Бронь уже имеется.");
        //    Book = book;
        //    BookId = book.Id;
        //}
    }
}
