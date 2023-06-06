using System.Drawing;

namespace CRM.Models
{
    public class About
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Url { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
    }
}
