namespace Lavalink4NET.DSharpPlus;

using System.Reflection;
using global::DSharpPlus;
using global::DSharpPlus.Clients;

/// <summary>
///     An utility for getting internal / private fields from DSharpPlus WebSocket Gateway Payloads.
/// </summary>
public static partial class DSharpPlusUtilities
{
    /// <summary>
    ///     The internal "orchestrator" property info in <see cref="DiscordClient"/>.
    /// </summary>
    private static readonly FieldInfo orchestratorField =
        typeof(DiscordClient).GetField("orchestrator", BindingFlags.NonPublic | BindingFlags.Instance)!;

    /// <summary>
    /// Gets the amount of shards handled by this client's orchestrator.
    /// </summary>
    public static int GetConnectedShardCount(this DiscordClient client)
    {
        var orchestrator = (IShardOrchestrator)orchestratorField.GetValue(client)!;
        return orchestrator.ConnectedShardCount;
    }

    /// <summary>
    /// Gets the total amount of shards connected to this bot.
    /// </summary>
    public static int GetTotalShardCount(this DiscordClient client)
    {
        var orchestrator = (IShardOrchestrator)orchestratorField.GetValue(client)!;
        return orchestrator.TotalShardCount;
    }
}
