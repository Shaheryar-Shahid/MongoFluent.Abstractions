using MongoFluent.Abstractions.Examples.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoFluent.Abstractions.Examples.Model
{
    public class Student: ITenantFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }
    }
}
