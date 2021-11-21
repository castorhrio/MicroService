using System;
using System.Collections.Generic;
using AutoMapper;
using CommandService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlatformService;

namespace CommandService.SyncDataService.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            Console.WriteLine($"---- Calling GRPC Service {_configuration["GrpcPlatform"]}");
            var channle = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
            var client = new GrpcPlatform.GrpcPlatformClient(channle);
            var request = new GetAllRequest();

            try
            {
                var replay = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(replay.Platform);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"------- Could not call GRPC Server:{ex.Message}");
                return null;
            }

        }
    }
}