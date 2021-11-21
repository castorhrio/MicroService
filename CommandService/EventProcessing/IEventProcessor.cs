namespace CommandService.EnventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}