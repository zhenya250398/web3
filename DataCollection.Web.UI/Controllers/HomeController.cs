using DataCollection.Web.UI.Models;
using DataCollectionModel.Models;
using DataCollectionModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace DataCollection.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbManager _dbManager = new DbManager();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEmployees()
        {
            try
            {
                var result = GetEmployeeModels();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetDepartments()
        {
            try
            {
                return Json(_dbManager.GetDepartments().Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    Name = d.Name
                }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                return Json(exp, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteEmployee(Guid id)
        {
            _dbManager.DeleteEmployee(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateEmployee(EmployeeViewModel employee)
        {
            Employee emp = new Employee
            {
                Name = employee.Name,
                Address = employee.Address,
                Birthday = employee.BirthDate,
                Gender = (Gender)employee.Gender.Value,
                Department = _dbManager.GetDepartmentsById(employee.Department.Id),
                Phone = employee.Phone
            };
            _dbManager.CreateEmployee(emp);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeEmployee(EmployeeViewModel employee)
        {
            var empl = _dbManager.GetEmployeeById(employee.Id);
            empl.Name = employee.Name;
            empl.Gender = (Gender)employee.Gender.Value;
            empl.Address = employee.Address;
            empl.Birthday = employee.BirthDate;
            empl.Department = _dbManager.GetDepartmentsById(employee.Department.Id);
            empl.Phone = employee.Phone;

            _dbManager.ChangeEmployee(empl);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteReward(Guid id)
        {
            _dbManager.DeleteReward(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateReward(RewardViewModel reward)
        {
            Reward rwd = new Reward
            {
                Name = reward.Name,
                Description = reward.Description,
                Year = reward.Year,
                Employee = _dbManager.GetEmployeeById(reward.EmployeeId)
            };
            _dbManager.CreateReward(rwd);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeReward(RewardViewModel reward)
        {
            var rew = _dbManager.GetRewardById(reward.Id);
            rew.Name = reward.Name;
            rew.Description = reward.Description;
            rew.Year = reward.Year;

            _dbManager.ChangeReward(rew);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddDepartment(DepartmentViewModel department)
        {
            Department dp = new Department { Name = department.Name };
            _dbManager.AddDepartment(dp);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult Download()
        {
            var fileManger = new FileManager();
            string fileName = fileManger.CreateExcelFileFromData(HostingEnvironment.ApplicationPhysicalPath);
            return File(fileName, "application/ooxml", "datacollection.xlsx");
        }

        private IEnumerable<EmployeeViewModel> GetEmployeeModels()
        {
            var result = _dbManager.GetEmployees().Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Address = e.Address,
                BirthDate = e.Birthday,
                Phone = e.Phone,
                Department = new DepartmentViewModel { Id = e.Department.Id, Name = e.Department.Name },
                Gender = new EnumViewModel { Description = EnumViewModel.GetDescription(e.Gender), Value = (int)e.Gender },
                Rewards = GetAllRewardsForEmployee(e)
            });
            return result;
        }

        private IEnumerable<RewardViewModel> GetAllRewardsForEmployee(Employee employee)
        {
            return employee.Rewards.Select(r => new RewardViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Year = r.Year,
                Description = r.Description
            });
        }
    }
}