using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath("/Users/angietatianapenapena/Desktop/TallerRabbitMQ/")
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<MyDbContext>();
        var connectionString = configuration.GetConnectionString("MyDatabaseConnection");
        builder.UseSqlServer(connectionString);

        return new MyDbContext(builder.Options);
    }
}
