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
    public class CreateScheduleHandler : IRequestHandler<CreateScheduleCommand, Schedule>
    {
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly IRepository<Animal> _animalRepository;

        public CreateScheduleHandler(IRepository<Schedule> scheduleRepository, IRepository<Animal> animalRepository)
        {
            _scheduleRepository = scheduleRepository;
            _animalRepository = animalRepository;
        }

        public async Task<Schedule> Handle(
            CreateScheduleCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                if (_animalRepository.GetByIdAsync(request.AnimalId) is null || request.feedingTime <= DateTime.Now) throw new ArgumentException();

                var schedule = new Schedule(
                    request.AnimalId,
                    request.feedingTime,
                    request.foodType);

                await _scheduleRepository.AddAsync(schedule);

                return await Task.FromResult(schedule);
            }
            catch (Exception ex)
            {
                return await Task.FromResult<Schedule>(null);
            }
        }
    }
}
