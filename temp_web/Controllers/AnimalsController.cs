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
    public class AnimalsController : ControllerBase
    {
        private readonly IRequestHandler<CreateAnimalCommand, Animal> _createHandler;
        private readonly IRequestHandler<GetCommand, Animal> _getHandler;
        private readonly IRequestHandler<DeleteCommand, bool> _deleteHandler;

        public AnimalsController(
            IRequestHandler<CreateAnimalCommand, Animal> createHandler, 
            IRequestHandler<GetCommand, Animal> getHandler,
            IRequestHandler<DeleteCommand, bool> deleteHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _deleteHandler = deleteHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var animal = await _getHandler.Handle(new GetCommand(id) , CancellationToken.None);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnimalCommand command)
        {
            var animal = await _createHandler.Handle(command, CancellationToken.None);
            if (animal is null) return BadRequest();

            return CreatedAtAction(
                nameof(GetById),
                new { id = animal.Id },
                animal);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _deleteHandler.Handle(new DeleteCommand(id), CancellationToken.None)) 
            {
                return Ok();
            }
            return NoContent();
        }
    }

}
