using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.BaseEntities;
using Domain.Enums;

namespace Domain.Entities
{
    public class Enclosure : IUniqueId
    {
        public AnimalType Type { get; private set; }
        public string Size { get; private set; }
        public int CurrentAnimals { get; private set; }
        public int MaxCapacity { get; private set; }
        private readonly List<Guid> _animalIds = new();
        public IReadOnlyCollection<Guid> AnimalIds => _animalIds.AsReadOnly();
        private Enclosure() { }

        public Enclosure(AnimalType type, string size, int maxCapacity) : base()
        {
            Type = type;
            Size = size;
            MaxCapacity = maxCapacity;
            CurrentAnimals = 0;
        }
        public bool Contains(Guid id) 
        {
            return _animalIds.Contains(id);
        }

        public bool Contains(Animal animal)
        {
            return _animalIds.Contains(animal.Id);
        }

        public bool CanAddAnimal(Animal animal)
        {
            return CurrentAnimals < MaxCapacity && animal.Type == Type && !_animalIds.Contains(animal.Id);
        }

        public void AddAnimal(Animal animal)
        {
            if (!CanAddAnimal(animal))
                throw new InvalidOperationException();
            _animalIds.Add(animal.Id);
            CurrentAnimals++;
        }

        public void RemoveAnimal(Animal animal)
        {
            if (CurrentAnimals <= 0 || !_animalIds.Contains(animal.Id))
                throw new InvalidOperationException();
            CurrentAnimals--;
            _animalIds.Remove(animal.Id);
        }

        public void Clean()
        {
            Console.WriteLine($"Cleaned at {DateTime.Now}");
        }

    }
}
