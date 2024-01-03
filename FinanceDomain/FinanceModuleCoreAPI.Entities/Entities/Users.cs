using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinanceModuleCoreAPI.Entities
{
    public class Users
    {
        [Key]
        [JsonIgnore]
        public int? UserId { get; set; } = null!;
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [JsonIgnore]
        public decimal Balance { get; set; } = 100;
        public string? Token { get; set; } = "";
    }
}
