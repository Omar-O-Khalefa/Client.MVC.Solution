using Client.BLL.Interfaces;
using Client.BLL.Repositories;
using Client.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client.PL.Controllers
{
    public class DepartmentController : Controller
    {
        // Inhertince : DepartmentController is Controller
        // Association : DepartmentController has DepartmentRepository

        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository department)
        {
            _departmentRepository = department;
        }
        // /Department/Index
        public IActionResult Index()
        {
            var Deps = _departmentRepository.GetAll();
             
            return View(Deps);
        }
        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Create( Department department) 
        { 
            if (ModelState.IsValid)
            {
                var Coun = _departmentRepository.Add(department);
                if(Coun > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
    }
}
