using System.Web.Mvc;

namespace RestAPIsApplication.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        ///     Controller method that returns the Home (Index) Page to the user.
        /// </summary>
        /// <returns> View() </returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}