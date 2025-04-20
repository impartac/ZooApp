using Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ZooStatistics : IUniqueId
    {
        public uint TotalAnimals { get; private set; }
        public uint TotalEnclosures { get; private set; }
        public uint TotalSchedules { get; private set; }
        public uint HealthyAnimals { get; private set; }
        public uint SickAnimals { get; private set; }
        public Dictionary<Guid, uint> EnclosureWorkloads { get; private set; } = new();
        public Dictionary<Guid, List<Guid>> EnclosureAnimals { get; private set; } = new();

        public void UpdateEnclosureWorkload(Guid enclosureId, int delta)
        {
            EnclosureWorkloads[enclosureId] = (uint)((int)EnclosureWorkloads[enclosureId] + delta);
        }

        public void TransferAnimal(Guid fromEnclosureId, Guid toEnclosureId, Guid animalId)
        {
            EnclosureAnimals[fromEnclosureId].Remove(animalId);
            EnclosureWorkloads[fromEnclosureId]--;
            EnclosureAnimals[toEnclosureId].Add(animalId);
            EnclosureWorkloads[toEnclosureId]++;
        }
    }
}
