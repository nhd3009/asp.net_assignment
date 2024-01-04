using BigAssignment.Models;

namespace BigAssignment.Repository
{
	public interface ICategoryRepository
	{
        Category Add(Category Category);

        Category Update(Category Category);
        Category Delete(string maCategory);
        Category GetCategory(string maCategory);
        List<Category> GetAllCategory();
    }
}
