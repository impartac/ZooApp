using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AnimalHandlers
{
    public class CreateAnimalHandler : IRequestHandler<CreateAnimalCommand, Animal>
    {
        private readonly IRepository<Animal> _animalRepository;

        public CreateAnimalHandler(IRepository<Animal> animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Animal> Handle(
            CreateAnimalCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var animal = new Animal(
                    request.Name,
                    request.BirthDate,
                    request.Gender,
                    request.FavoriteFood,
                    request.Type);

                await _animalRepository.AddAsync(animal);

                return await Task.FromResult(animal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}
