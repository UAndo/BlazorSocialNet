using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSocialNet.Entities.Models.Authorization
{
    public class Role
    {
        [Key]
        [Column("Name")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
