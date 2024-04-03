using Client.BLL.Interfaces;
using Client.BLL.Repositories;
using Client.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var Deps =await _unitOfWork.Repository<Department>().GetAllAsync();
             
            return View(Deps);
        }
        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( Department department) 
        { 
            if (ModelState.IsValid)
            {
                _unitOfWork.Repository<Department>().Add(department);
                var Coun = await _unitOfWork.Complete();
                if (Coun > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Dept =await _unitOfWork.Repository<Department>().GetAsync(id.Value);
            if(Dept is null)
                return NotFound();
            return View(ViewName,Dept);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (!id.HasValue)
            //    return BadRequest();
            //var Dept = _departmentRepository.Get(id.Value);
            //if (Dept is null)
            //    return NotFound();
            //return View(Dept);
            return await Details(id,"Edit");
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
                _unitOfWork.Repository<Department>().Update(department);
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id ,"Delete");
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.Repository<Department>().Delete(department);
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
