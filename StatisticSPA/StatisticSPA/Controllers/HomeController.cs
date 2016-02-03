using System.Web.Mvc;

namespace StatisticSPA.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult ShowClients()
    {
      return PartialView();
    }

    public ActionResult EditClient()
    {
      return PartialView();
    }

    public ActionResult AddClient()
    {
      return PartialView("EditClient");
    }

    public ActionResult ShowGroups()
    {
      return PartialView();
    }

    public ActionResult AddGroup()
    {
      return PartialView("EditGroup");
    }

    public ActionResult EditGroup()
    {
      return PartialView();
    }

    public ActionResult ShowReports()
    {
      return PartialView();
    }
  }
}
