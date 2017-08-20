using System.Web.Mvc;
using AgeRanger.Models;
using AgeRanger.Repositories;

namespace AgeRanger.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPersonRepository _personRepository;

        public HomeController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public ActionResult Index(string query = null)
        {
            var persons = _personRepository.GetPersons();
            if (!string.IsNullOrEmpty(query))
            {
                persons = _personRepository.GetPersonBySearch(query, persons);
            }

            var viewModel = new AgeGroupViewModel { Persons = persons, SearchTerm = query };
            return View("Index", viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new AgeGroupViewModel();
            ViewBag.Heading = "Create Person";

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(AgeGroupViewModel viewModel)
        {
            ViewBag.Heading = "Create Person";
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }
            var person = new Person()
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Age = viewModel.Age
            };
            _personRepository.Add(person);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            var remove = _personRepository.GetPersonById(id);
            _personRepository.Remove(remove);

            return RedirectToAction("Index", "Home");

        }

        public ActionResult Search(AgeGroupViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Heading = "Edit Person";

            var person = _personRepository.GetPersonById(id);
            var viewModel = new AgeGroupViewModel()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age
            };

            return View("Create", viewModel);
        }

        [HttpPost]
        public ActionResult Update(AgeGroupViewModel viewModel, int id)
        {
            ViewBag.Heading = "Edit Person";
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            _personRepository.Update(viewModel, id);
            return RedirectToAction("Index", "Home", new { id = viewModel.Id });
        }
    }
}