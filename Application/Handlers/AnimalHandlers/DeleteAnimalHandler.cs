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
    public class DeleteAnimalHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly IRepository<Animal> _repository;

        public DeleteAnimalHandler(IRepository<Animal> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            DeleteCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteByIdAsync(request.Id);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
