using System.Web.Mvc;

namespace SmartCalendar.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        
    }
}