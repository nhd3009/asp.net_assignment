using BigAssignment.Helper;
using BigAssignment.Models;
using BigAssignment.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System.Collections.Generic;
using X.PagedList;

namespace BigAssignment.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMenu _menuRepository;
        MovieWebContext _context;
        public MenuViewComponent(IMenu menuRepository, MovieWebContext context)
        {
            _menuRepository = menuRepository;
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var menus = _menuRepository.GetAllMenu();
            var count = HttpContext.Session.GetComplexData<int>("cart_count");
            ViewBag.Count = count;
            return View(menus);

        }
    }
}
