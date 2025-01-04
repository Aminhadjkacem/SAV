namespace SAV.Models.DTO
{
    public class CreateInterventionDTO
    {
        public int ReclamationId { get; set; }
        public int TechnicienId { get; set; }
        public bool IsUnderWarranty { get; set; }
        public decimal TotalCost { get; set; }
        public List<int> PieceRechangeIds { get; set; } // Only IDs for related pieces
    }
}
