using System.Collections.Generic;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Contorllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandForPlatform(int platformId)
        {
            if (!_repository.PlatformExits(platformId))
            {
                return Ok("platformId not exits");
            }

            var commands = _repository.GetCammandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            if (!_repository.PlatformExits(platformId))
            {
                return Ok("platformId not exits");
            }

            var commands = _repository.GetCommand(platformId, commandId);
            if (commands == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(commands));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            if (!_repository.PlatformExits(platformId))
            {
                return Ok("platformId not exits");
            }

            var command = _mapper.Map<Command>(commandDto);
            _repository.CreateCommand(platformId, command);
            _repository.SaveChange();

            var commadreadDto = _mapper.Map<CommandReadDto>(command);
            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commadreadDto.Id }, commadreadDto);
        }
    }
}