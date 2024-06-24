using FinShark.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace FinShark.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

     
    }
}
