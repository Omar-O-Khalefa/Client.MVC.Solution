using AutoMapper;
using Client.BLL.Interfaces;
using Client.DAL.Models;
using Client.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Client.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;

        // Inhertince : EmployeeController is Controller
        // Association : EmployeeController has EmployeeRepository

        private readonly IEmployeeRepository _EmployeeRepository;
       //private readonly IDepartmentRepository _departmentRepository;
        private readonly IHostEnvironment _env;

        public EmployeeController(IMapper mapper,IEmployeeRepository employee,/*IDepartmentRepository departmentRepository,*/ IHostEnvironment Env)
        {
            _mapper = mapper;
            _EmployeeRepository = employee;
            //_departmentRepository = departmentRepository;
            _env = Env;
        }
        // /Employee/Index
        public IActionResult Index( string SearchInp)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchInp)) {


                employees = _EmployeeRepository.GetAll();

            return View(employees);

            }
            else
            {
                employees = _EmployeeRepository.SearchByName(SearchInp.ToLower());
                var MappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(MappedEmp);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
         //   ViewData["Departments"]= _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                //Manual Mapping
                ///var Mappedemployee = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Age = employeeVM.Age,
                ///    Adress = employeeVM.Adress,
                ///    Salary = employeeVM.Salary,
                ///    Email = employeeVM.Email,
                ///    PhoneNumber = employeeVM.PhoneNumber,
                ///    IsActive = employeeVM.IsActive,
                ///    HiringDate = employeeVM.HiringDate,
                ///};
                ///
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                var Coun = _EmployeeRepository.Add(mappedEmp);
                if (Coun > 0)
                {
                    TempData["Message"] = "Employee Is successfuly Add";
                    return RedirectToAction(nameof(Index));
                }
              
            }
            
            TempData["Message"] = "An Error Has Occred";
            return View(employeeVM);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Employee = _EmployeeRepository.Get(id.Value);
            var MappedEmp =_mapper.Map<Employee, EmployeeViewModel>(Employee);
            if (MappedEmp is null)
                return NotFound();
            return View(ViewName, MappedEmp);
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

           // ViewData["Departments"] = _departmentRepository.GetAll();
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest("An Error Ya Hamada");
            }
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                _EmployeeRepository.Update(mappedEmp);
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
                return View(employeeVM);
            }
        }

        //Employee/Delete
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVm)
        {

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _EmployeeRepository.Delete(mappedEmp);
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
                return View(employeeVm);
            }
        }
    }
}
