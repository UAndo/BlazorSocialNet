using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorSocialNet.Entities.Models.Authentication
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
        public string PasswordHash { get; set; }

        [Column("Image")]
        public string? Image { get; set; }

        [Column("Language")] 
        public string Language { get; set; } = "uk";

        [Column("VerificationToken")]
        public string? VerificationToken { get; set; }

        [Column("VerifiedAt")] 
        public DateTime? VerifiedAt { get; set; }

        [Column("PasswordResetToken")]
        public string? PasswordResetToken { get; set; }

        [Column("ResetTokenExpires")]
        public DateTime? ResetTokenExpires { get; set; }

        public User(string email, string name, string language)
        {
            Email = email;
            Name = name;
            Language = language;
        }

        public User() { }
    }
}