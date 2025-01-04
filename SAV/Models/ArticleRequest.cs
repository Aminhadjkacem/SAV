using Microsoft.AspNetCore.Mvc;

namespace SAV.Models
{
    public class ArticleRequest
    {
        public string Name { get; set; }
        public bool IsUnderWarranty { get; set; }
        public decimal Price { get; set; }
        public DateTime WarrantyEndDate { get; set; }
    }
}
