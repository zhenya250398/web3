using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectionModel.Models
{
    public class Reward : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
