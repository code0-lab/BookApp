namespace BookApp.Models
{
    public class ProductViewModel
    {
        public List<ProductBook> Products {get;set;} = null!;
        public List<Category> Categories {get;set;} = null!;

        public string? SlectedCategory{get;set;}
    }

}