using System.Reflection;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Logging;

CancellationTokenSource cts = new();
Console.CancelKeyPress += (s, e) =>
{
    cts.Cancel();
    e.Cancel = true;
};

Console.WriteLine("Hello, World!");

var token = File.ReadAllText("token.txt");

var discord = new DiscordClient(new DiscordConfiguration()
{
    Token = token,
    TokenType = TokenType.Bot,
    Intents = DiscordIntents.All,
    MinimumLogLevel = LogLevel.Information
});

// Init CommandsNext
{
    var commandsNext = discord.UseCommandsNext(new CommandsNextConfiguration()
    {
        StringPrefixes = ["$"]
    });

    commandsNext.RegisterCommands(Assembly.GetExecutingAssembly());
}

await discord.ConnectAsync();

while (!cts.IsCancellationRequested)
{
    await Task.Delay(TimeSpan.FromSeconds(1));
}

Console.WriteLine("Exiting!");