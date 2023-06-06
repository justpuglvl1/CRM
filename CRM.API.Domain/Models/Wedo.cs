using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.API.Models
{
    [Table("Wedo")]
    public class Wedo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
