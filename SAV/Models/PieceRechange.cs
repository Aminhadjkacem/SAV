namespace SAV.Models
{
    public class PieceRechange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Intervention> Interventions { get; set; } // Relation n-n
    }

}
