namespace BookApp.Models
{
    public class Repository
    {
        // Statik listeler oluşturarak veri deposunu başlatıyoruz.
        private static readonly List<ProductBook> _productBooks = new();
        private static readonly List<Category> _Categories = new();

        static Repository(){
            _Categories.Add(new Category{CategoryId = 1, Name = "Novel"});
            _Categories.Add(new Category{CategoryId = 2, Name = "History"});
            _Categories.Add(new Category{CategoryId = 3, Name = "Philosophy"});

            _productBooks.Add(new ProductBook{BookId = 1, BookName = "The Conduct of War - Colmar Freiherr von der Goltz", Description ="This work has been selected by scholars as being culturally important, and is part of the knowledge base of civilization as we know it.", PageCount = 308, IsActive = true,
            Image = "1.png", CategoryId = 2});
            _productBooks.Add(new ProductBook{BookId = 2, BookName = "The nation in arms - Colmar Freiherr von der Goltz", Description ="", PageCount = 300, IsActive = true,
            Image = "2.png", CategoryId = 2});
            _productBooks.Add(new ProductBook{BookId = 3, BookName = "Thus Spoke Zarathustra - Friedrich Nietzsche", Description ="", PageCount = 352, IsActive = false,
            Image = "4.png", CategoryId = 1});
        }

        // Kitap listesine erişim sağlayan public property
        public static List<ProductBook> Products
        {
            get { return _productBooks; }
        }

        public static void CreateBook(ProductBook entity)
        {
            _productBooks.Add(entity);
        }
        public static void EditBook(ProductBook UpdateBook)
        {
            var entity = _productBooks.FirstOrDefault(b=>b.BookId == UpdateBook.BookId);
            if(entity != null)
            {
                entity.BookName= UpdateBook.BookName;
                entity.Description = UpdateBook.Description;
                entity.PageCount = UpdateBook.PageCount;
                entity.BookId = UpdateBook.BookId;
                entity.CategoryId = UpdateBook.CategoryId;
                entity.Image = UpdateBook.Image;
                entity.IsActive = UpdateBook.IsActive;
            }
        }

        public static void DeleteBook(ProductBook DeletedBook)
        {
            var entity = _productBooks.FirstOrDefault(b=>b.BookId == DeletedBook.BookId);
            if(entity != null)
            {
                _productBooks.Remove(entity);
            }
        }
        // Kategori listesine erişim sağlayan public property
        public static List<Category> Categories
        {
            get { return _Categories; }
        }
    }
}
