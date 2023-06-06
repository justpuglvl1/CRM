using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace CRM.API.Models
{
    [Table("About")]
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Url { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
    }
}
