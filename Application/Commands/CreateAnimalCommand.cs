using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Commands
{
    public record CreateAnimalCommand
    (
        string Name,
        AnimalType Type,
        DateTime BirthDate,
        Gender Gender,
        string FavoriteFood
    );
}
