﻿using System.Text.Json.Serialization;

namespace Online_Store.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int? CategoryId { get; set; }

        // Навигационное свойство
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
