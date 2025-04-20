using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;

namespace Application.Services
{
    public class AnimalTrasferService : IAnimalTransferService
    {
        private readonly IRepository<Animal> _animalRepository;
        private readonly IRepository<Enclosure> _enclosureRepository;
        private readonly IDomainEventDispatcher _eventDispatcher;


        public AnimalTrasferService(IRepository<Animal> animalRepository,
                                    IRepository<Enclosure> enclosureRepository,
                                    IDomainEventDispatcher eventDispatcher)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task TransferAnimalAsync(Guid animalId, Guid fromEnclosureId, Guid toEnclosureId)
        {
            try
            {
                var animal = await _animalRepository.GetByIdAsync(animalId);
                var from = await _enclosureRepository.GetByIdAsync(fromEnclosureId);
                var to = await _enclosureRepository.GetByIdAsync(toEnclosureId);
                if (animal is null || from is null || to is null) throw new ArgumentException($"Something is null");

                if (from.Contains(animalId) && to.CanAddAnimal(animal))
                {
                    from.RemoveAnimal(animal);
                    animal.MoveToEnclosure(toEnclosureId);
                    await _eventDispatcher.DispatchAsync(new AnimalMovedEvent(
                                        animal.Id,
                                        from?.Id ?? Guid.Empty,
                                        to.Id));
                }
                else
                {
                    throw new ArgumentException($"Can't add animal with id={animalId}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ArgumentException();

            }

        }
    }
}
