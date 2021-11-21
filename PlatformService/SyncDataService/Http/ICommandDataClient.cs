using System.Threading.Tasks;
using PlatformService.Data;

namespace PlatformService.SyncDataService.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}