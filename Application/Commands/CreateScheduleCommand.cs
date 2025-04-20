using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public record CreateScheduleCommand (Guid AnimalId, DateTime feedingTime, string foodType);
}
