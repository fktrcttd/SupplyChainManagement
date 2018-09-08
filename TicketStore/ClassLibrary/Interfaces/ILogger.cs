namespace TicketStore.Core
{
    public interface ILogger
    {
        void Write(string category, string message);
    }
}