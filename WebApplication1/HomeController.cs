using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public class HomeController : Controller
    {
        private readonly IDb db;

        public HomeController(IDb db)
        {
            this.db = db;
        }

        public IActionResult Index(Command command)
        {
            var model = new HomeIndexModel(db.Get(command), command, UrlForCommand);

            return View(model);
        }

        private string UrlForCommand(Command command)
        {
            return Url.Action("Index", command);
        }
    }
}