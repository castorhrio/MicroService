using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataService;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataService.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController:ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataCLient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(IPlatformRepo repository,
        IMapper mapper,
        ICommandDataClient commandDataCLient,
        IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataCLient = commandDataCLient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--- Getting Paltforms...");

            var platformItem = _repository.GetAllPatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}",Name="GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _repository.GetPlatformById(id);

            if(platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreatDto platformCreatDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreatDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            //send sync message
            try
            {
                await _commandDataCLient.SendPlatformToCommand(platformReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"----- Could not send synchromously:{ex.Message}");
            }

            //send async message
            try
            {
                var platformPublishDto = _mapper.Map<PlatformPublishDto>(platformReadDto);

                platformPublishDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--- could not send async:{ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new {Id = platformReadDto.Id},platformReadDto);
        }     
    }
}