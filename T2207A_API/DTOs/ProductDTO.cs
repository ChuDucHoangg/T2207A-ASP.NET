using System;
namespace T2207A_API.DTOs
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
        public int qty { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryDTO Category { get; set; } = null;
    }
}

