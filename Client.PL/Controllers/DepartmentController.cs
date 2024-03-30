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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _env;

        public DepartmentController(/*IDepartmentRepository department ,*/ IUnitOfWork unitOfWork,IHostEnvironment Env)

        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = department;
            _env = Env;
        }
        // /Department/Index
        public IActionResult Index()
        {
            var Deps = _unitOfWork.DepartmentRepository.GetAll();
             
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
                _unitOfWork.DepartmentRepository.Add(department);
                var Coun = _unitOfWork.Complete();
                if (Coun > 0)
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
            var Dept = _unitOfWork.DepartmentRepository.Get(id.Value);
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
                _unitOfWork.DepartmentRepository.Update(department);
                _unitOfWork.Complete();
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
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
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
