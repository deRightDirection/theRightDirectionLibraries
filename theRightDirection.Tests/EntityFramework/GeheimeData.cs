using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace theRightDirection.Tests.EntityFramework;

[Table("namen")]
[Index(nameof(Id), IsUnique = true)]
public class GeheimeData
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
