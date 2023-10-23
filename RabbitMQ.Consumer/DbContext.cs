using Microsoft.EntityFrameworkCore;
using RabbitMQ.Consumer;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    public MyDbContext() { }
    public DbSet<Mensaje> Mensajes { get; set; }
}
