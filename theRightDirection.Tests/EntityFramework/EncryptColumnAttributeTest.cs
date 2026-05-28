using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using theRightDirection.EntityFramework;
using theRightDirection.Tests.EntityFramework;

namespace EntityFramework;

public class EncryptColumnAttributeTest
{
    [Fact]
    public void EncryptData()
    {
        var config = new Mock<IConfiguration>();
        config.Setup(c => c["EF:EncryptionKeyName"]).Returns("portalgenius_key");
        var service = new EncryptionService(config.Object);
        var newRecord = new GeheimeData { FirstName = "Mannus", LastName = "Etten" };
        var options = new DbContextOptionsBuilder<TestDataContext>()
            .UseSqlite("Pooling=False;Filename=c:\\temp\\unittest.db");
        //            .UseInMemoryDatabase(Guid.NewGuid().ToString())
        var db = new TestDataContext(options.Options, service);
        db.Database.Migrate();
        db.Namen.Add(newRecord);
        db.SaveChanges();
        var recordFromDb = db.Namen.First();
        recordFromDb.FirstName.Should().Be(newRecord.FirstName);
        recordFromDb.LastName.Should().Be(newRecord.LastName);
    }
}
