using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectionModel.Models
{
    public class Department : Entity
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
