using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using theRightDirection.EntityFramework.Attribute;

namespace theRightDirection.Tests.EntityFramework;
[Table("namen")]
[Index(nameof(Id), IsUnique = true)]
public class GeheimeData
{
    [Key]
    public int Id { get; set; }
    [EncryptColumn]
    public string FirstName { get; set; }
    [EncryptColumn]
    public string LastName { get; set; }
}
