using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FeedingOrganizationService : IFeedingOrganizationService
    {
        private readonly IRepository<Animal> _animalRepository;
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly IDomainEventDispatcher _eventDispatcher;

        public FeedingOrganizationService(
            IRepository<Animal> animalRepository,
            IRepository<Schedule> scheduleRepository,
            IDomainEventDispatcher eventDispatcher)
        {
            _animalRepository = animalRepository;
            _scheduleRepository = scheduleRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task ScheduleFeedingAsync(Guid animalId, DateTime time, string foodType)
        {
            var schedule = new Schedule(animalId, time, foodType);
            await _scheduleRepository.AddAsync(schedule);
        }

        public async Task FeedAnimalAsync(Guid animalId)
        {
            var animal = await _animalRepository.GetByIdAsync(animalId);
            animal.Feed();

            foreach (var domainEvent in animal.DomainEvents)
            {
                await _eventDispatcher.DispatchAsync(domainEvent);
            }
            animal.ClearDomainEvents();
        }
    }
}
