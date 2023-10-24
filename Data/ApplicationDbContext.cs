using Microsoft.EntityFrameworkCore;

namespace TechnicalTestMasiv
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Data> MyDataItems { get; set; }
        public DbSet<ElevatorState> ElevatorStates { get; set; }
    }
}
