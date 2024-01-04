using BigAssignment.Models;

namespace BigAssignment.Repository
{
    public interface IMenu
    {
        Menu Add(Menu menu);

        Menu Update(Menu menu);
        Menu Delete(string idMenu);
        Menu GetMenu(string idMenu);
        List<Menu> GetAllMenu();
    }
}
