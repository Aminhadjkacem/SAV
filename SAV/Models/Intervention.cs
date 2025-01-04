using System.Text.Json.Serialization;

namespace SAV.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public int ReclamationId { get; set; }
        [JsonIgnore]

        public Reclamation Reclamation { get; set; }
        public int TechnicienId { get; set; }
        [JsonIgnore]

        public Technicien Technicien { get; set; }
        public bool IsUnderWarranty { get; set; }
        public decimal TotalCost { get; set; }
        [JsonIgnore]


        public ICollection<PieceRechange> PiecesRechange { get; set; }
    }
}
