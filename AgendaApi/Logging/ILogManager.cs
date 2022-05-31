namespace AgendaApi.Logging
{
    public interface ILogManager
    {
        void Debug(string message);
        void LogError(string message);
    }
}
