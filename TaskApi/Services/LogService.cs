namespace TaskApi.Services
{
    public class LogService
    {
        public void Log(string developerName, string message)
        {
            
            Console.WriteLine($"Developer: {developerName}, Message: {message}");
        }
    }
}
