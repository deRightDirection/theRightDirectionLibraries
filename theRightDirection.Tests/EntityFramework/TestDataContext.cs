using Microsoft.EntityFrameworkCore;
using theRightDirection.EntityFramework;
using theRightDirection.EntityFramework.Converter;

namespace theRightDirection.Tests.EntityFramework;

public class TestDataContext(DbContextOptions<TestDataContext> options, EncryptionService encryptionService)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var encryptionConverter = new EncryptionConverter(encryptionService);
        modelBuilder.Entity<GeheimeData>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FirstName).HasConversion(encryptionConverter);
            entity.Property(x => x.LastName).HasConversion(encryptionConverter);
        });
    }
    public DbSet<GeheimeData> Namen { get; set; }

}