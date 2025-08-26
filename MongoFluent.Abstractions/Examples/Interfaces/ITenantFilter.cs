using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoFluent.Abstractions.Examples.Interfaces
{
    public class ITenantFilter
    {
        public Guid TenantId { get; set; }
    }
}
