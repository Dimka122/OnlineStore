namespace Online_Store.Models.DTOs
{
    public class CategoryDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ProductShortDTO> Products { get; set; }
    }
}
