using DataCollectionModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DataCollectionModel
{
    internal class AppDbInit : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            Init(context);
            base.Seed(context);
        }
        private void Init(AppDbContext context)
        {
            //Reward reward = new Reward
            //{
            //    Name = "Работник года",
            //    Description = "Лучший работник года по результатам 2016 года",
            //    Year = 2017
            //};

            Department department = new Department { Name = "Отдел разработки ПО" };

            //Employee emp = new Employee
            //{
            //    Name = "Иванов Петр Степанович",
            //    Birthday = new DateTime(1980, 12, 11),
            //    Address = "Пермь",
            //    Gender = Gender.Male,
            //    Phone = "+78955648792",
            //    Department = department,
            //    Rewards = new List<Reward> { reward }
            //};

            //context.Employees.Add(emp);

            Department d1 = new Department { Name = "Отдел маркетинга" };
            Department d2 = new Department { Name = "Отдел продаж" };
            Department d3 = new Department { Name = "Управление" };
            Department d4 = new Department { Name = "Отдел технического обслуживания" };

            context.Departments.AddRange(new List<Department> { department, d1, d2, d3, d4 });
        }
    }
}
