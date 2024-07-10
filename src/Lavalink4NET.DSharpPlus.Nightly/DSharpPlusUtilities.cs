namespace Lavalink4NET.DSharpPlus;

using System;
using System.Collections.Concurrent;
using System.Reflection;
using global::DSharpPlus;
using global::DSharpPlus.AsyncEvents;

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
}
