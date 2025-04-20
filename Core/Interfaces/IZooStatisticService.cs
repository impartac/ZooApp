using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IZooStatisticService
    {
        public Task<uint> CountAnimalsAsync();
        public Task<uint> CountEnclosuresAsync();
        public Task<uint> CountSchedulesAsync();

        public ZooStatistics GetStatistics();

    }
}
    