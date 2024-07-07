
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Dtos
{
    public class UserDto
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        [StringLength(100)]
        public string Username { get; set; } = null!;

        [Column("password")]
        [StringLength(100)]
        public string Password { get; set; } = null!;

        [Column("role")]
        [StringLength(50)]
        public string Role { get; set; } = null!;
    }
}
