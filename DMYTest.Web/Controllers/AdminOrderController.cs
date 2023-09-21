
namespace DMYTest.Web.Controllers
{
    #region Using 
    using DMYTest.Data.Concrete;
    using System.Web.Mvc;
    using PagedList;
    #endregion
    public class AdminOrderController : Controller
    {
        OrderRepository repository = new OrderRepository();


        [Authorize(Roles = "admin")]
        public ActionResult Index(int pageNumber = 1)
        {
            return View(repository.List().ToPagedList(pageNumber ,4));
        }


        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var orderToDelete = repository.GetById(id);
            repository.Delete(orderToDelete);
            return View("Index");
        }
    }
}