using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class AnimalMovedEvent : DomainEvent
    {
        public Guid AnimalId { get; }
        public Guid? OldEnclosureId { get; }
        public Guid? NewEnclosureId { get; }

        public AnimalMovedEvent(Guid animalId, Guid? oldEnclosureId, Guid? newEnclosureId) : base()
        {
            AnimalId = animalId;
            OldEnclosureId = oldEnclosureId;
            NewEnclosureId = newEnclosureId;
        }
    }
}
