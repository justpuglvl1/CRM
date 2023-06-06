using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    [Table("Notes")]
    public partial class Notes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "No date")]
        public string Date { get; set; }

        [Required(ErrorMessage = "No name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "No description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "No address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "No IBAN")]
        public string Iban { get; set; }
    }
}
