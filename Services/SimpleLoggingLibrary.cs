namespace MyMvcApp.Services
{
    public enum LogLevel
    {
        Info,
        Error
    }

    public class SimpleLoggingLibrary
    {
        public void Log(string message, LogLevel level)
        {
            // For simplicity, we'll just print to the console. In a real application, this could be more complex.
            Console.WriteLine($"[{level}] {message}");
        }
    }
}
