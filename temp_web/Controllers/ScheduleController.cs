using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IRequestHandler<CreateScheduleCommand, Schedule> _createHandler;
        private readonly IRequestHandler<GetCommand, Schedule> _getHandler;
        private readonly IRequestHandler<DeleteCommand, bool> _deleteHandler;

        public ScheduleController(
            IRequestHandler<CreateScheduleCommand, Schedule> createHandler,
            IRequestHandler<GetCommand, Schedule> getHandler,
            IRequestHandler<DeleteCommand, bool> deleteHandler)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _deleteHandler = deleteHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var enclosure = await _getHandler.Handle(new GetCommand(id), CancellationToken.None);
            if (enclosure == null) return NotFound();
            return Ok(enclosure);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateScheduleCommand command)
        {
            var schedule = await _createHandler.Handle(command, CancellationToken.None);
            if (schedule is null) return BadRequest();

            return CreatedAtAction(
                nameof(GetById),
                new { id = schedule.Id },
                schedule);
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
