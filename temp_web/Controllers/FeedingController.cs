using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedingController : ControllerBase
    {
        private readonly IFeedingOrganizationService _feedingOrganizationService;
        private readonly IRequestHandler<GetCommand, Animal> _getHandler;

        public FeedingController(
            IFeedingOrganizationService feedingOrganizationService,
            IRequestHandler<GetCommand, Animal> getHandler)
        {
            _feedingOrganizationService = feedingOrganizationService;
            _getHandler = getHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Feed([FromBody] GetCommand command)
        {
            var animal = await _getHandler.Handle(command, CancellationToken.None);
            if (animal is null) return BadRequest();

            return 
        }

    }

}
