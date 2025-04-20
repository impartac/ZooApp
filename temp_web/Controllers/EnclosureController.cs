using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnclosureController : ControllerBase
    {
        private readonly IRequestHandler<CreateEnclosureCommand, Enclosure> _createHandler;
        private readonly IRequestHandler<GetCommand, Enclosure> _getHandler;
        private readonly IRequestHandler<DeleteCommand, bool> _deleteHandler;
        private readonly IRequestHandler<AddAnimalToEnclosureCommand, bool> _addAnimalToEnclosureHandler;

        public EnclosureController(
            IRequestHandler<CreateEnclosureCommand, Enclosure> createHandler,
            IRequestHandler<GetCommand, Enclosure> getHandler,
            IRequestHandler<DeleteCommand, bool> deleteHandler,
            IRequestHandler<AddAnimalToEnclosureCommand, bool> addAnimalToEnclosureHandler
        )
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _deleteHandler = deleteHandler;
            _addAnimalToEnclosureHandler = addAnimalToEnclosureHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var enclosure = await _getHandler.Handle(new GetCommand(id), CancellationToken.None);
            if (enclosure == null) return NotFound();
            return Ok(enclosure);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnclosureCommand command)
        {
            var enclosure = await _createHandler.Handle(command, CancellationToken.None);
            if (enclosure is null) return BadRequest();

            return CreatedAtAction(
                nameof(GetById),
                new { id = enclosure.Id },
                enclosure);
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

        [HttpPost("add-animal")]
        public async Task<IActionResult> AddAnimal([FromBody] AddAnimalToEnclosureCommand command) 
        {
            if (await _addAnimalToEnclosureHandler.Handle(command, CancellationToken.None)) 
            {
                return Ok();
            }
            return BadRequest();

        }
    }   

}
