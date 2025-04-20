using Application;
using Application.Commands;
using Application.Handlers;
using Application.Handlers.AnimalHandlers;
using Application.Handlers.EnclosureHandlers;
using Application.Handlers.ScheduleHandlers;
using Application.Services;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using Infrastructure;

public class App
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();


        builder.Services.AddSingleton<IRepository<Animal>, AnimalRepository>();
        builder.Services.AddSingleton<IRepository<Enclosure>, EnclosureRepository>();
        builder.Services.AddSingleton<IRepository<Schedule>, ScheduleRepository>();
        builder.Services.AddSingleton<ZooStatistics>();

        builder.Services.AddScoped<IRequestHandler<CreateAnimalCommand, Animal>, CreateAnimalHandler>();
        builder.Services.AddScoped<IRequestHandler<GetCommand, Animal>, GetAnimalHandler>();
        builder.Services.AddScoped<IRequestHandler<DeleteCommand, bool>, DeleteAnimalHandler>();

        builder.Services.AddSingleton<IDomainEventHandler<AnimalMovedEvent>, AnimalMovedEventHandler>();

        builder.Services.AddScoped<IRequestHandler<CreateEnclosureCommand, Enclosure>, CreateEnclosureHandler>();
        builder.Services.AddScoped<IRequestHandler<GetCommand, Enclosure>, GetEnclosureHandler>();
        builder.Services.AddScoped<IRequestHandler<DeleteCommand, bool>, DeleteEnclosureHandler>();

        builder.Services.AddScoped<IRequestHandler<AddAnimalToEnclosureCommand, bool>, AddAnimalToEnclosureHandler>();

        builder.Services.AddScoped<IRequestHandler<CreateScheduleCommand, Schedule>, CreateScheduleHandler>();
        builder.Services.AddScoped<IRequestHandler<GetCommand, Schedule>, GetScheduleHandler>();
        builder.Services.AddScoped<IRequestHandler<DeleteCommand, bool>, DeleteScheduleHandler>();

        builder.Services.AddScoped<IAnimalTransferService, AnimalTrasferService>();
        builder.Services.AddScoped<IRequestHandler<AnimalTransferCommand, bool>, AnimalTransferHandler>();

        builder.Services.AddSingleton<IDomainEventDispatcher, MediatrDomainEventDispatcher>();
        /*
                builder.Services.AddSingleton<IDomainEventHandler<FeedingTimeEvent>, Fee>();*/

        builder.Services.AddSingleton<IZooStatisticService, ZooStatisticsService>();
        builder.Services.AddSingleton<IFeedingOrganizationService, FeedingOrganizationService>();

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<AnimalTransferHandler>());

        builder.Services.AddControllers();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}


