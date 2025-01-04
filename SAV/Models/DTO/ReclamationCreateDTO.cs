using System.ComponentModel.DataAnnotations;

namespace SAV.Models.DTO
{
    public class ReclamationCreateDTO
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        [Required]
        public Guid ClientId { get; set; }
    }

}
