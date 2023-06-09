using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuckDrop.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int TeamId { get; set; }

    }
}
