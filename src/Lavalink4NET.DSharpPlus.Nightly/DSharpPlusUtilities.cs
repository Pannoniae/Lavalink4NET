namespace Lavalink4NET.DSharpPlus;

using System;
using System.Collections.Concurrent;
using System.Reflection;
using global::DSharpPlus;
using global::DSharpPlus.Clients;
using global::DSharpPlus.AsyncEvents;
using System.Threading.Tasks;

/// <summary>
///     An utility for getting internal / private fields from DSharpPlus WebSocket Gateway Payloads.
/// </summary>
public static partial class DSharpPlusUtilities
{
    /// <summary>
    ///     The internal "events" property info in <see cref="DiscordClient"/>.
    /// </summary>
    // https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Clients/DiscordClient.cs#L37
    private static readonly FieldInfo eventsField =
        typeof(DiscordClient).GetField("events", BindingFlags.NonPublic | BindingFlags.Instance)!;

    /// <summary>
    ///     Gets the internal "events" property value of the specified <paramref name="client"/>.
    /// </summary>
    /// <param name="client">the instance</param>
    /// <returns>the "events" value</returns>
    public static ConcurrentDictionary<Type, AsyncEvent> GetEvents(this DiscordClient client)
        => (ConcurrentDictionary<Type, AsyncEvent>)eventsField.GetValue(client)!;

    /// <summary>
    ///     The internal "errorHandler" property info in <see cref="DiscordClient"/>.
    /// </summary>
    // https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Clients/DiscordClient.cs#L41
    private static readonly FieldInfo errorHandlerField =
        typeof(DiscordClient).GetField("errorHandler", BindingFlags.NonPublic | BindingFlags.Instance)!;

    /// <summary>
    ///     Gets the internal "errorHandler" property value of the specified <paramref name="client"/>.
    /// </summary>
    /// <param name="client">the instance</param>
    /// <returns>the "errorHandler" value</returns>
    public static IClientErrorHandler GetErrorHandler(this DiscordClient client)
        => (IClientErrorHandler)errorHandlerField.GetValue(client)!;

    /// <summary>
    ///     The internal "Register" method info in <see cref="DiscordClient"/>.
    /// </summary>
    // https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/AsyncEvents/AsyncEvent.cs#L14
    private static readonly MethodInfo asyncEventRegisterMethod =
        typeof(AsyncEvent).GetMethod("Register", BindingFlags.NonPublic | BindingFlags.Instance, [typeof(Delegate)])!;

    /// <summary>
    ///     Calls the internal "Register" method of the spedificed <paramref name="asyncEvent"/>
    /// </summary>
    /// <param name="asyncEvent">the instance</param>
    /// <param name="delegate">the event to register</param>
    public static void Register(this AsyncEvent asyncEvent, Delegate @delegate) => asyncEventRegisterMethod.Invoke(asyncEvent, [@delegate]);

    /// <summary>
    ///     The internal "orchestrator" property info in <see cref="DiscordClient"/>.
    /// </summary>
    // https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Clients/DiscordClient.cs#L47
    private static readonly FieldInfo orchestratorField =
        typeof(DiscordClient).GetField("orchestrator", BindingFlags.NonPublic | BindingFlags.Instance)!;

    /// <summary>
    ///     The internal "shardCount" property info in <see cref="MultiShardOrchestrator"/>.
    /// </summary>
    // https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Clients/DiscordClient.cs#L47
    private static readonly FieldInfo shardCountField =
        typeof(MultiShardOrchestrator).GetField("shardCount", BindingFlags.NonPublic | BindingFlags.Instance)!;

    /// <summary>
    /// Gets the number of connected shards or this client
    /// </summary>
    public static async ValueTask<int> GetShardCountAsync(this DiscordClient client)
    {
        var orchestrator = (IShardOrchestrator)orchestratorField.GetValue(client)!;

        if (orchestrator is SingleShardOrchestrator)
            return 1;

        if (orchestrator is MultiShardOrchestrator multiShardOrchestrator)
            return (int)(uint)shardCountField.GetValue(multiShardOrchestrator)!;

        // If the orchestrator is neither a Single nor Multi sharded orchestrator, that means it
        // is using a custom solution implemented by the end user. There is no way to directly access
        // the shard count in this case, so instead we estimate it by using Discord's recommended
        // shard amount.
        return (await client.GetGatewayInfoAsync()).ShardCount;
    }
}
