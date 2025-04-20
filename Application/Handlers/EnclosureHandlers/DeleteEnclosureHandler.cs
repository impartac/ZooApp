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
    public class DeleteEnclosureHandler : IRequestHandler<DeleteCommand, bool>
    {
        private readonly IRepository<Enclosure> _repository;

        public DeleteEnclosureHandler(IRepository<Enclosure> repository)
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
