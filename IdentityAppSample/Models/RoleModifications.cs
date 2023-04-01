using System.ComponentModel.DataAnnotations;

namespace IdentityAppSample.Models
{
    public class RoleModifications
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[]? AddIds { get; set; }
        public string[]? DeleteIds { get; set; }
    }
}
