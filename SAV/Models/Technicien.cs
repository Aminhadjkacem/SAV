namespace SAV.Models
{
    public class Technicien
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public ICollection<Intervention> Interventions { get; set; } // Relation 1-n
    }
}
