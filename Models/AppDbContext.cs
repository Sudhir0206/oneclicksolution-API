using Microsoft.EntityFrameworkCore;

namespace OneClickSolution.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Quotation> quotation { get; set; }
    }
}
