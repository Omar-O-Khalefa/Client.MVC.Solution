using Client.BLL.Interfaces;
using Client.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Client.PL.Controllers
{
    public class EmployeeController : Controller
    {
        // Inhertince : EmployeeController is Controller
        // Association : EmployeeController has EmployeeRepository

        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employee, IHostEnvironment Env)
        {
            _EmployeeRepository = employee;
            _env = Env;
        }
        // /Employee/Index
        public IActionResult Index()
        {
            var Employee = _EmployeeRepository.GetAll();

            return View(Employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var Coun = _EmployeeRepository.Add(employee);
                if (Coun > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            
            return View(employee);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Employee = _EmployeeRepository.Get(id.Value);
            if (Employee is null)
                return NotFound();
            return View(ViewName, Employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //    return BadRequest();
            //var Employee = _EmployeeRepository.Get(id.Value);
            //if (Employee is null)
            //    return NotFound();
            //return View(Employee);
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("An Error Ya Hamada");
            }
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            try
            {
                _EmployeeRepository.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                // 1.log Exception
                // friendly Message

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured Updating The Employee ");
                return View(employee);
            }
        }

        //Employee/Delete
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _EmployeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                // 1.log Exception
                // 2.friendly Message

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured Delete The Employee ");
                return View(employee);
            }
        }
    }
}
