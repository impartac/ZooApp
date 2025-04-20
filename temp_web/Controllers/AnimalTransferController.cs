using Application.Commands;
using Application.Handlers.AnimalHandlers;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalTransferController : ControllerBase
    {
        private readonly IAnimalTransferService _animalTransferService;
        
        public AnimalTransferController(IAnimalTransferService animalTransferService)
        {
            _animalTransferService = animalTransferService;
        }

        [HttpPost]
        public async Task<IActionResult> Transfer([FromBody] AnimalTransferCommand command)  
        {
            try
            {
                await _animalTransferService.TransferAnimalAsync(command.animalId, command.fromEnclosureId, command.toEnclosureId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}
