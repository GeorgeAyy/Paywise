namespace MyMvcApp.Services
{
    public class SimpleLoggingLibraryAdapter : IAppLogger
    {
        private readonly SimpleLoggingLibrary _simpleLoggingLibrary;

        public SimpleLoggingLibraryAdapter(SimpleLoggingLibrary simpleLoggingLibrary)
        {
            _simpleLoggingLibrary = simpleLoggingLibrary;
        }

        public void LogInfo(string message)
        {
            _simpleLoggingLibrary.Log(message, LogLevel.Info);
        }

        public void LogError(string message)
        {
            _simpleLoggingLibrary.Log(message, LogLevel.Error);
        }
    }
}
