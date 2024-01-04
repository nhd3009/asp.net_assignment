using BigAssignment.Models;

namespace BigAssignment.Repository
{
    public class MenuResository : IMenu
    {
        private readonly MovieWebContext _context;
        public MenuResository(MovieWebContext context)
        {
            _context = context;
        }

        public Menu Add(Menu menu)
        {
            _context.Add(menu);
            _context.SaveChanges();
            return menu;
        }

        public Menu Delete(string idMenu)
        {

            throw new NotImplementedException();
        }

        public List<Menu> GetAllMenu()
        {
            var result = _context.Menus.ToList();
            return result;
            
        }

        public Menu GetMenu(string idMenu)
        {
            return _context.Menus.Find(idMenu);
        }

        
        public Menu Update(Menu menu)
        {
           _context.Update(menu);
            _context.SaveChanges(true);
            return menu;
        }
    }
}
