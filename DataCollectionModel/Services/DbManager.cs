using DataCollectionModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectionModel.Services
{
    public class DbManager
    {
        private readonly AppDbContext _context = new AppDbContext();

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        public void DeleteEmployee(Guid id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                foreach (var reward in employee.Rewards.ToList())
                    _context.Rewards.Remove(reward);
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments;
        }

        public void DeleteReward(Guid id)
        {
            var reward = _context.Rewards.FirstOrDefault(r => r.Id == id);
            if (reward != null)
            {
                _context.Rewards.Remove(reward);
                _context.SaveChanges();
            }
        }

        public void CreateEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
        }

        public Department GetDepartmentsById(Guid id)
        {
            return _context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public Employee GetEmployeeById(Guid id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void CreateReward(Reward rwd)
        {
            _context.Rewards.Add(rwd);
            _context.SaveChanges();
        }

        public void ChangeEmployee(Employee empl)
        {
            _context.SaveChanges();
        }

        public Reward GetRewardById(Guid id)
        {
            return _context.Rewards.FirstOrDefault(r => r.Id == id);
        }

        public void ChangeReward(Reward rew)
        {
            _context.SaveChanges();
        }

        public void AddDepartment(Department dp)
        {
            _context.Departments.Add(dp);
            _context.SaveChanges();
        }
    }
}
