using System;
using System.Collections.Generic;
using System.Linq;
using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDBContext _context;

        public CommandRepo(AppDBContext context)
        {
            _context = context;
        }

        public void CreateCommand(int platformId, Command command)
        {
            if(command == null)
            {
                throw new ArgumentException(nameof(command));
            }

            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform plat)
        {
            if(plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }

            _context.Platforms.Add(plat);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }


        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands.Where(c=>c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCammandsForPlatform(int platformId)
        {
            return _context.Commands.Where(c=>c.PlatformId == platformId).OrderBy(c=>c.Platform.Name);
        }


        public bool PlatformExits(int platformId)
        {
            return _context.Platforms.Any(p=> p.Id == platformId);
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool ExternalPlatformExist(int enternalPlatformId)
        {
            return _context.Platforms.Any(a=>a.ExtendID == enternalPlatformId);
        }
    }
}