using Domain.Entities;
using Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;

namespace Domain.Entities
{
    public class Schedule : IUniqueId
    {
        public Guid AnimalId { get; private set; }
        public DateTime FeedingTime { get; private set; }
        public string FoodType { get; private set; }
        public bool IsCompleted { get; private set; }

        private List<DomainEvent> _domainEvents = new();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public Schedule(Guid animalId, DateTime feedingTime, string foodType) : base()
        {
            AnimalId = animalId;
            FeedingTime = feedingTime;
            FoodType = foodType;
            IsCompleted = false;
        }

        public void CompleteFeeding()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Feeding already completed");

            IsCompleted = true;
            _domainEvents.Add(new FeedingTimeEvent(AnimalId, FeedingTime, FoodType));
        }

        public void Reschedule(DateTime newTime)
        {
            FeedingTime = newTime;
            IsCompleted = false;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}