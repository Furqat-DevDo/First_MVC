using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models;
public class Category
{
   [Required]
   public string Name { get; set; }
   [Key]
   public Guid ID{ get; set; }=Guid.NewGuid();
   [DisplayName("Created  At")]
   public DateTimeOffset Created_At { get; set; }=DateTimeOffset.UtcNow;
}