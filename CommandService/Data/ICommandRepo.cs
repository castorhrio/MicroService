using System.Collections.Generic;
using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepo
    {
        bool SaveChange();
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform plat);
        bool PlatformExits(int platformId);
        IEnumerable<Command> GetCammandsForPlatform(int platformId);
        Command GetCommand(int platformId,int commandId);
        void CreateCommand(int platformId,Command command);
        
        bool ExternalPlatformExist(int enternalPlatformId);
    }
}