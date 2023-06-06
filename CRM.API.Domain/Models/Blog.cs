using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.API.Models
{
    [Table("Blog")]
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Url { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
    }
}
