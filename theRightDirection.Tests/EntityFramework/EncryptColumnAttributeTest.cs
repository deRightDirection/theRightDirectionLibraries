using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using theRightDirection.EntityFramework.Util;

namespace theRightDirection.Tests.EntityFramework;
public class EncryptColumnAttributeTest
{
    [Fact]
    public void EncryptData()
    {
        var encryptionKey = "9d81212378af30dda0f7de10302a316b";
        var encryptionProvider = new GenerateEncryptionProvider(encryptionKey);
        TestDataContext.EncryptionProvider = encryptionProvider;
        var newRecord = new GeheimeData { FirstName = "Mannus", LastName = "Etten" };
        var db = new TestDataContext();
        db.Database.Migrate();
        db.Namen.Add(newRecord);
        db.SaveChanges();
        var recordFromDb = db.Namen.First();
        recordFromDb.FirstName.Should().Be(newRecord.FirstName);
        recordFromDb.LastName.Should().Be(newRecord.LastName);
    }
}
