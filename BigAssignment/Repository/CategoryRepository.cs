using BigAssignment.Models;

namespace BigAssignment.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
        private readonly MovieWebContext _context;

        public CategoryRepository(MovieWebContext context)
        {
            _context = context;
        }

        public Category Add(Category Category)
        {
            throw new NotImplementedException();
        }

        public Category Delete(string maCategory)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategory()
        {
            var result = _context.Categories.ToList();
            return result;
        }

        public Category GetCategory(string maCategory)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category Category)
        {
            throw new NotImplementedException();
        }
    }
}
