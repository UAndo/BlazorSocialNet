using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSocialNet.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("PasswordHash")] 
        public byte[] PasswordHash { get; set; } = new byte[32];

        [Column("PasswordSalt")]
        public byte[] PasswordSalt { get; set; } = new byte[32];

        [Column("Image")]
        public string Image { get; set; }

        [Column("Language")] 
        public string Language { get; set; } = "uk";

        [Column("VerificationToken")]
        public string VerificationToken { get; set; }

        [Column("VerifiedAt")] 
        public DateTime? VerifiedAt { get; set; }

        [Column("PasswordResetToken")]
        public string? PasswordResetToken { get; set; }

        [Column("ResetTokenExpires")]
        public DateTime? ResetTokenExpires { get; set; }
    }
}