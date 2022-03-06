using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibApp.Models;

namespace LibApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}