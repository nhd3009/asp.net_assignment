using BigAssignment.Models;
using BigAssignment.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BigAssignment.ViewComponents
{
    public class FeaturedProductViewComponent : ViewComponent
    {
        private readonly IFeaturedProduct _featuredProduct;
        MovieWebContext db = new MovieWebContext();

        public FeaturedProductViewComponent(IFeaturedProduct featuredProduct)
        {
            _featuredProduct = featuredProduct;
        }

        public IViewComponentResult Invoke(int? page)
        {
            
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
    }
}
