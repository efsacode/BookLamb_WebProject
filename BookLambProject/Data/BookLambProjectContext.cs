using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookLambProject.Models;

namespace BookLambProject.Data
{
    public class BookLambProjectContext : DbContext
    {
        public BookLambProjectContext (DbContextOptions<BookLambProjectContext> options)
            : base(options)
        {
        }

        public DbSet<BookLambProject.Models.Book> Book { get; set; } = default!;

        public DbSet<BookLambProject.Models.ShoppingCart>? ShoppingCart { get; set; }
    }
}
