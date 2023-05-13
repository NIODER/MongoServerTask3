// See https://aka.ms/new-console-template for more information

using ServerCore;

Core core = new();
HttpHandler httpHandler = new();

core.RequestReceived += httpHandler.ExecuteHandler;

core.Start();

Thread.Sleep(-1);