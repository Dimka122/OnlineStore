using System.Text.Json.Serialization;

namespace Online_Store.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Навигационное свойство (не будет сериализоваться в JSON)
        [JsonIgnore]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
