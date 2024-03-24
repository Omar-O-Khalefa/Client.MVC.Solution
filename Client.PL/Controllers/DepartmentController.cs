using Client.BLL.Interfaces;
using Client.BLL.Repositories;
using Client.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Client.PL.Controllers
{
    public class DepartmentController : Controller
    {
        // Inhertince : DepartmentController is Controller
        // Association : DepartmentController has DepartmentRepository

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IHostEnvironment _env;

        public DepartmentController(IDepartmentRepository department ,IHostEnvironment Env)
        {
            _departmentRepository = department;
            _env = Env;
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

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Dept = _departmentRepository.Get(id.Value);
            if(Dept is null)
                return NotFound();
            return View(ViewName,Dept);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //    return BadRequest();
            //var Dept = _departmentRepository.Get(id.Value);
            //if (Dept is null)
            //    return NotFound();
            //return View(Dept);
            return Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id ,Department department)
        {
            if(id != department.Id)
            {
                return BadRequest("An Error Ya Hamada");
            }
            if(!ModelState.IsValid)
            {
                return View(department);
            }
            try
            {
                _departmentRepository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                // 1.log Exception
                // friendly Message
                
                if (_env.IsDevelopment())
                ModelState.AddModelError(string.Empty,ex.Message);
                else
                    ModelState.AddModelError(string.Empty,"An Error Has Occured Updating The Department ");
                return View(department);
            }
        }

        //Department/Delete
        public IActionResult Delete(int? id)
        {
            return Details(id ,"Delete");
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            try
            {
                _departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                // 1.log Exception
                // friendly Message

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured Delete The Department ");
                return View(department);
            }
        }
    }
}
