using Application;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MediatrDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public MediatrDomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAsync(DomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case AnimalMovedEvent animalMoved:
                    await _mediator.Publish(animalMoved);
                    break;

                case FeedingTimeEvent feedingTime:
                    await _mediator.Publish(feedingTime);
                    break;

                default:
                    throw new ArgumentException($"Unknown event type: {domainEvent.GetType()}");
            }
        }
    }
}
