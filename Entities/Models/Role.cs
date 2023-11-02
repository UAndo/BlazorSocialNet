using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorSocialNet.Entities.Models
{
    public class Role
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Name")]
        [Required]
        public string Name { get; set; }
    }
}
