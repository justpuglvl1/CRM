using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.API.Models
{
    [Table("Author")]
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
