using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataCollection.Web.UI.Models
{
    public class RewardViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid EmployeeId { get; set; }
        public int Year { get; set; }
    }
}