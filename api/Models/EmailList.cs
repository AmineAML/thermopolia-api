using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class EmailList
    {
        public int ID { get; set; }
        
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DefaultValue(false)]
        public bool IsVerified { get; set; }
    }
}