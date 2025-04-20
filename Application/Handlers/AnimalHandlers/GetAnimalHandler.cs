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
    public class GetAnimalHandler : IRequestHandler<GetCommand, Animal>
    {
        private readonly IRepository<Animal> _repository;

        public GetAnimalHandler(IRepository<Animal> repository)
        {
            _repository = repository;
        }

        public async Task<Animal> Handle(
            GetCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var animal = await _repository.GetByIdAsync(request.Id);
                return await Task.FromResult(animal);
            }
            catch (Exception ex)
            {
                return await Task.FromResult<Animal>(null);
            }
        }
    }
}
