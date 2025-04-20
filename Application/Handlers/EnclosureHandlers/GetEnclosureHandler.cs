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
    public class GetEnclosureHandler : IRequestHandler<GetCommand, Enclosure>
    {
        private readonly IRepository<Enclosure> _repository;

        public GetEnclosureHandler(IRepository<Enclosure> repository)
        {
            _repository = repository;
        }

        public async Task<Enclosure> Handle(
            GetCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var enclosure = await _repository.GetByIdAsync(request.Id);
                return await Task.FromResult(enclosure);
            }
            catch (Exception ex)
            {
                return await Task.FromResult<Enclosure>(null);
            }
        }
    }
}
