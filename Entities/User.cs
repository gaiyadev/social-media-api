using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMediaApp.Entities;

[Table("users")]
public class User : BaseEntity
{
    [Required]
    [Column("email", TypeName = "VARCHAR(255)")]
    public required string Email { get; set; }

    [Required]
    [Column("first_name",TypeName = "VARCHAR(255)")] 
    public required string FirstName { get; set; }

    [Required]
    [Column("last_name",TypeName = "VARCHAR(255)")] 
    public required string LastName { get; set; }
    

    [Required]
    [Column("password", TypeName = "VARCHAR(255)")]
    public required string Password { get; set; }
    
    [Column("reset_token",TypeName = "VARCHAR(255)")]
    public string? ResetToken { get; set; }

    [Column("is_active", TypeName = "boolean")]
    [DefaultValue(false)]
    public bool IsActive { get; set; }

    
    [JsonIgnore]
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    
}