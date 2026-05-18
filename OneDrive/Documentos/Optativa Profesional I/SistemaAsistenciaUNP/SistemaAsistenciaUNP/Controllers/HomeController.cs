using Microsoft.AspNetCore.Mvc;

namespace SistemaAsistenciaUNP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}