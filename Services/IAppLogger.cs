namespace MyMvcApp.Services
{
    public interface IAppLogger
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}
