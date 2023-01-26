using AssignmentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApi.Data
{
    public class ContactsApiContext : DbContext
    {
        public ContactsApiContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contacts> Contact { get; set; }
    }
}
