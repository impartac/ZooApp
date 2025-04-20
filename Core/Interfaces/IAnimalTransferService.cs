using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAnimalTransferService
    {
        public Task TransferAnimalAsync(Guid animalId, Guid fromEnclosureId, Guid toEnclosureId);
    }
}
