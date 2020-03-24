using System.Web.Mvc;

namespace RestAPIsApplication.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        ///     Controller method that returns the Home (Index) Page to the user.
        /// </summary>
        /// <returns> View() </returns>
        //[AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}