using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ScheduleHandlers
{
    public class GetScheduleHandler : IRequestHandler<GetCommand, Schedule>
    {
        private readonly IRepository<Schedule> _repository;

        public GetScheduleHandler(IRepository<Schedule> repository)
        {
            _repository = repository;
        }

        public async Task<Schedule> Handle(
            GetCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var schedule = await _repository.GetByIdAsync(request.Id);
                return await Task.FromResult(schedule);
            }
            catch (Exception ex)
            {
                return await Task.FromResult<Schedule>(null);
            }
        }
    }
}
