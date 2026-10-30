public class Logger
{
    private string logFilePath = "logs.txt";


    readonly private StreamWriter _streamWriter;

    public Logger()
    {
        _streamWriter = new(logFilePath);
    }


    public void LogInfo(string message)
    {
        _streamWriter.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] INFO {message}");
        _streamWriter.Flush();
    }

    public void LogWarning(string message)
    {
        _streamWriter.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] WARNING {message}");
        _streamWriter.Flush();
    }

    public void LogError(string message)
    {
        _streamWriter.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR {message}");
        _streamWriter.Flush();
    }

    internal void Close()
    {
        _streamWriter.Close();
    }
}