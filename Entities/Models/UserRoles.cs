using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorSocialNet.Entities.Models
{
    public class UserRoles
    {
        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("RoleId")]
        public Guid RoleId { get; set; }
    }
}
