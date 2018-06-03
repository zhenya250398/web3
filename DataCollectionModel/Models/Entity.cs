using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectionModel.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
