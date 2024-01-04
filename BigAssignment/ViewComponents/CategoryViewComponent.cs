using Microsoft.AspNetCore.Mvc;
using BigAssignment.Repository;
using BigAssignment.Models;

namespace BigAssignment.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _category;
       
        
        public CategoryViewComponent(ICategoryRepository category)
        {
            _category = category;
           
        }

        public IViewComponentResult Invoke()
        {
            var categories = _category.GetAllCategory();
          
            return View(categories);
        }

        private IViewComponentResult View(List<Category> categories, List<Slider> sliders)
        {
            throw new NotImplementedException();
        }
    }
}
