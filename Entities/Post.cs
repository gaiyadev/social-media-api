using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SocialMediaApp.Entities;


[Table("posts")]
public class Post : BaseEntity
{
   [Required]
    [Column("content", TypeName = "VARCHAR(255)")]
    public required string Content { get; set; }

    [JsonIgnore]
     [Column("user_id", TypeName = "int" )]
    public int UserId { get; set; }

    public User? User { get; set; } 


}