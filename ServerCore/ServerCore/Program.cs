// See https://aka.ms/new-console-template for more information

using ServerCore;

try
{
    string configPath = args.First();
    Config.Load(configPath);
}
catch (InvalidOperationException)
{
    Logger.Log(LogSeverity.Error, "main", "Need config path");
    Console.ReadKey();
    return;
}
catch (ArgumentNullException e)
{
    Logger.Log(LogSeverity.Error, "main", "Error parsing config", e);
    Console.ReadKey();
    return;
}

Core core = new();
HttpHandler httpHandler = new();

core.RequestReceived += httpHandler.ExecuteHandler;

core.Start();

Thread.Sleep(-1);