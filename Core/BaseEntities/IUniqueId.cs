using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseEntities
{
    public abstract class IUniqueId
    {
        public Guid Id { get; private set; }

        public IUniqueId() 
        {
            Id = Guid.NewGuid();
        }
    }
}
