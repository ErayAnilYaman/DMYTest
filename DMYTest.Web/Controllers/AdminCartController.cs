
namespace DMYTest.Web.Controllers
{
    #region Usings
    using PagedList;
    using System.Web.Mvc;
    using DMYTest.Data.Concrete;

    #endregion
    public class AdminCartController : Controller
    {

        // GET: AdminCart

        CartRepository cartRepository = new CartRepository();

        [Authorize(Roles = "admin")]
        public ActionResult Index(int page = 1)
        {
            
            return View(cartRepository.List().ToPagedList(page,3));
        }


    }
}