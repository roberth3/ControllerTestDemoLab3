using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ControllerTestDemo.Domain;

namespace ControllerTestDemo.Controllers
{
    public class UserController : Controller
    {
        IUserRepository repository;

        // Dependency is injected by SimpleInjectorInitializer
        // IUserRepository service registered in App_Start.SimpleInjectorInitializer -> InitializeContainer
        public UserController(IUserRepository repository)
        {
            this.repository = repository;
            //this.repository = MvcApplication.Container.GetInstance<IUserRepository>();
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            IEnumerable<User> users = repository.GetAll();

            if(users.Count<User>()==0)
            {
                ModelState.AddModelError("", "No users found");
                return View("Error");
            }

            return View(users);
        }

        //
        // GET: /User/Details/mpolo

        public ActionResult Details(string id)
        {
            User user = repository.GetByUsername(id);

            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                repository.Save(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
