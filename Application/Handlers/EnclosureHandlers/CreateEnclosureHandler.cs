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
    public class CreateEnclosureHandler : IRequestHandler<CreateEnclosureCommand, Enclosure>
    {
        private readonly IRepository<Enclosure> _repository;

        public CreateEnclosureHandler(IRepository<Enclosure> repository)
        {
            _repository = repository;
        }

        public async Task<Enclosure> Handle(
            CreateEnclosureCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var enclosure = new Enclosure(
                    request.Type,
                    request.Size,
                    request.MaxCapacity);

                await _repository.AddAsync(enclosure);

                return await Task.FromResult(enclosure);
            }
            catch (Exception ex)
            {
                return await Task.FromResult<Enclosure>(null);
            }
        }
    }
}
