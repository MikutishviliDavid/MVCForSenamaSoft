using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MVCForSenamaSoft.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return Content(String.Format("Hello {0}!!!", User.Identity.Name));
        }
    }
}
