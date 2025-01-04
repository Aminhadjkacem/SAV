using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SAV.Models
{
    public class Reclamation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public Guid ClientId { get; set; }  // Ensure this is a GUID
        public virtual Client Client { get; set; }  // Navigation property

        public ICollection<Intervention> Interventions { get; set; }

    }


}
