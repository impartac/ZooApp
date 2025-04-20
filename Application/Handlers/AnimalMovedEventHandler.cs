using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class AnimalMovedEventHandler : IDomainEventHandler<AnimalMovedEvent>
    {
        private readonly IZooStatisticService _statisticsService;
        private readonly IRepository<Animal> _animalsRepository;

        public async Task HandleAsync(AnimalMovedEvent @event, CancellationToken cancellationToken = default)
        {
            ZooStatistics stat = _statisticsService.GetStatistics();
            Animal animal = await _animalsRepository.GetByIdAsync(@event.AnimalId);
            stat.TransferAnimal(animal.EnclosureId ?? Guid.Empty, @event.NewEnclosureId ?? Guid.Empty, animal.Id);
        }
    }
}
