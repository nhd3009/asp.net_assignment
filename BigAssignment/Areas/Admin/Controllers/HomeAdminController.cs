using Azure;
using BigAssignment.Areas.Admin.Models.AuthenAdmin;
using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BigAssignment.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [AuthenticationAdmin]
    public class HomeAdminController : Controller
    {
        MovieWebContext db = new MovieWebContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
