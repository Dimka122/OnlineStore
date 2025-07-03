﻿namespace Online_Store.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
