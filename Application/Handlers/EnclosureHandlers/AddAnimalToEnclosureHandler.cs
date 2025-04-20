using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.EnclosureHandlers
{
    public class AddAnimalToEnclosureHandler : IRequestHandler<AddAnimalToEnclosureCommand, bool>
    {
        private readonly IRepository<Enclosure> _enclosureRepository;
        private readonly IRepository<Animal> _animalRepository;

        public AddAnimalToEnclosureHandler(IRepository<Enclosure> enclosureRepository, IRepository<Animal> animalRepository)
        {
            _enclosureRepository = enclosureRepository;
            _animalRepository = animalRepository;
        }

        public async Task<bool> Handle(
            AddAnimalToEnclosureCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                Animal animal = await _animalRepository.GetByIdAsync(request.animalId);
                if (animal == null || _enclosureRepository.Any(x => x.Contains(animal))) throw new ArgumentException();


                Enclosure enclosure =  await _enclosureRepository.GetByIdAsync(request.enclosureId);
                enclosure.AddAnimal(animal);
                animal.SetEnclosureId(request.enclosureId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return await Task.FromResult(false);
            }
        }
    }
}
