using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSocialNet.Entities.Models.Authorization
{
    public class UserRoles
    {
        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("RoleId")]
        public Guid RoleId { get; set; }
    }
}
