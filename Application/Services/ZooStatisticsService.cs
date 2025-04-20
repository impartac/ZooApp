using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ZooStatisticsService : IZooStatisticService
    {
        private readonly ZooStatistics _stats;

        public ZooStatisticsService(
            ZooStatistics stats)
        {
            _stats = stats;
        }

        public async Task<uint> CountAnimalsAsync()
        {
            return _stats.TotalAnimals;
        }

        public async Task<uint> CountEnclosuresAsync()
        {
            return _stats.TotalEnclosures;
        }

        public async Task<uint> CountSchedulesAsync()
        {
            return _stats.TotalSchedules;
        }

        public ZooStatistics GetStatistics()
        {
            return _stats;
        }

        /*public async Task<uint> CountHealthyAnimalsAsync()
        {
            var stats = GetOrCreateStats();
            return stats.HealthyAnimals;
        }

        public async Task<uint> CountSickAnimalsAsync()
        {
            var stats = GetOrCreateStats();
            return stats.SickAnimals;
        }

        public async Task<Dictionary<Guid, uint>> GetEnclosuresWithWorkLoad()
        {
            var stats = GetOrCreateStats();
            return stats.EnclosureWorkloads;
        }

        public async Task<Dictionary<Guid, List<Guid>>> GetEnclosuresWithAnimals()
        {
            var stats = GetOrCreateStats();
            return stats.EnclosureAnimals;
        }

        private ZooStatistics GetOrCreateStats()
        {
            return _stats ?? new ZooStatistics { Id = Guid.NewGuid() };
        }*/
    }
}
