using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BaseEntities;
using Domain.Enums;
using Domain.Events;

namespace Domain.Entities
{
    public class Animal : IUniqueId
    {
        public string Name { get; private set; }

        public AnimalType Type { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public string FavoriteFood { get; private set; }
        public HealthState Health { get; private set; }
        public Guid? EnclosureId { get; private set; }

        public void Treat()
        {
            Health = HealthState.Healthy;
        }

        public void Feed()
        {
            Console.WriteLine($"Animal with id = {Id} was feed at {DateTime.Now}");
        }

        public Animal(string name, DateTime birthDate, Gender gender, 
            string favoriteFood, AnimalType type, HealthState health = HealthState.Healthy) : base()
        {
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavoriteFood = favoriteFood;
            Type = type;
            Health = health;
        }

        public void MoveToEnclosure(Guid enclosureId)
        {
            EnclosureId = enclosureId;
            AddDomainEvent(new AnimalMovedEvent(Id, EnclosureId, enclosureId));
        }

        private readonly List<DomainEvent> _domainEvents = new();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent eventItem) => _domainEvents.Add(eventItem);
        public void ClearDomainEvents() => _domainEvents.Clear();

        public void SetEnclosureId(Guid id) 
        {
            EnclosureId = id;
        }
    }
}
