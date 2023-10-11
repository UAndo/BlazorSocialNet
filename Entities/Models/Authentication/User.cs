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

        [Column("Username")]
        public string Username { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("Description")]
        public string? Description { get; set; }

        [Column("PasswordHash")] 
        public string PasswordHash { get; set; }

        [Column("Image")]
        public string? Image { get; set; }

        [Column("Language")] 
        public string Language { get; set; } = "uk";

        [Column("Location")]
        public string Location { get; set; }

        [Column("Role")]
        public string Role { get; set; }

        [Column("BirthDate")]
        public DateTime? BirthDate { get; set; }

        [Column("LastActivityAt")]
        public DateTime? LastActivityAt { get; set; }

        [Column("IsOnline")]
        public bool? IsOnline
        {
            get
            {
                if (LastActivityAt.HasValue)
                {
                    var timeSinceLastActivity = DateTime.Now - LastActivityAt.Value;
                    return timeSinceLastActivity.TotalMinutes <= 5;
                }
                return false;
            }
        }

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
            Username = name;
            Language = language;
        }

        public User() { }
    }
}