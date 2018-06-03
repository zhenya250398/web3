using DataCollectionModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataCollection.Web.UI.Models
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public EnumViewModel Gender { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public DepartmentViewModel Department { get; set; }
        public IEnumerable<RewardViewModel> Rewards { get; set; }
    }
}