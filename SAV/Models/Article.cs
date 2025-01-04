using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace SAV.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUnderWarranty { get; set; }
        public decimal Price { get; set; }
        public DateTime WarrantyEndDate { get; set; }
    }
}
