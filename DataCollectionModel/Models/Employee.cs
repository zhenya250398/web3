using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataCollectionModel.Models
{
    public class Employee : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public virtual Department Department { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public virtual List<Reward> Rewards { get; set; }
    }

    public enum Gender
    {
        [Description("Муж.")]
        Male,

        [Description("Жен.")]
        Female
    }
}
