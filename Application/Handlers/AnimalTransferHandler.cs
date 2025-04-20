using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class AnimalTransferHandler : IRequestHandler<AnimalTransferCommand, bool>
    {
        private readonly IAnimalTransferService _animalTransferService;

        public AnimalTransferHandler(IAnimalTransferService animalTransferService)
        {
            _animalTransferService = animalTransferService;
        }

        public async Task<bool> Handle(
            AnimalTransferCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _animalTransferService.TransferAnimalAsync(request.animalId, request.fromEnclosureId, request.toEnclosureId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
