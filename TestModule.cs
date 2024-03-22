using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DSharpPlusBug;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

[Group("test")]
public class TestModule : BaseCommandModule
{
    private static int i = 0;
    private static readonly object iLock = new();

    [GroupCommand]
    public async Task Test(CommandContext ctx)
    {
        int id;
        lock (iLock)
        {
            id = i++;
        }

        await ctx.RespondAsync($"Start: {id}");
        await Task.Delay(TimeSpan.FromSeconds(15));
        await ctx.RespondAsync($"End: {id}");
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CA1822 // Mark members as static
#pragma warning restore IDE0079 // Remove unnecessary suppression