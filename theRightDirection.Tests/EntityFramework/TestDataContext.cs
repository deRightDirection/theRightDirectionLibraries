using Microsoft.EntityFrameworkCore;
using theRightDirection.EntityFramework.Extension;
using theRightDirection.EntityFramework.Interfaces;

namespace theRightDirection.Tests.EntityFramework;

public class TestDataContext : DbContext
{
    public static IEncryptionProvider EncryptionProvider;
    public DbSet<GeheimeData> Namen { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Pooling=False;Filename=c:\\temp\\unittest.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseEncryption(EncryptionProvider);
    }
}