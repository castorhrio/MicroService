using System;
using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandService.EnventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetemineEvent(message);

            switch (eventType)
            {
                case EventType.PlatformsPublished:
                    AddPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetemineEvent(string notifcationMessage)
        {
            Console.WriteLine(" ------ Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("Platform Published Event Detected");
                    return EventType.PlatformsPublished;
                default:
                    Console.WriteLine("------- Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishedMessage)
        {
            using (var scop = _scopeFactory.CreateScope())
            {
                var repo = scop.ServiceProvider.GetRequiredService<ICommandRepo>();

                var platformPublishDto = JsonSerializer.Deserialize<PlatformPublishDto>(platformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishDto);
                    if (!repo.ExternalPlatformExist(plat.ExtendID))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChange();
                        Console.WriteLine("---------- Platform Add...");
                    }
                    else
                    {
                        Console.WriteLine("---------- Platform already exisits...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"---------- could not add platform to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        PlatformsPublished,
        Undetermined
    }
}