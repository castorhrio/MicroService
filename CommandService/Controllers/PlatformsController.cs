using System;
using System.Collections.Generic;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Contorllers{

    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController:ControllerBase{
        private readonly ICommandRepo _respository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository,IMapper mapper)
        {
            _respository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("---------- Getting platforms from CommandService");

            var platformItems = _respository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpPost]
        public ActionResult TestInboundConnection(){
            Console.WriteLine("------------- Inbound POST Command Service ---------------");

            return Ok("Inbound test of form platforms controller");
        }
    }
}