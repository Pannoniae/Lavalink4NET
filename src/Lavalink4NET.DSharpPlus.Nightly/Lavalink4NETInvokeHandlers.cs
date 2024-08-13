namespace Lavalink4NET.DSharpPlus;

using System.Threading.Tasks;
using global::DSharpPlus;
using global::DSharpPlus.EventArgs;
using Lavalink4NET.Clients;

/// <summary>
/// Forwards event triggers to the Lavalink4NET client wrapper
/// </summary>
internal sealed class Lavalink4NETInvokeHandlers(IDiscordClientWrapper wrapper) :
    IEventHandler<GuildDownloadCompletedEventArgs>,
    IEventHandler<VoiceServerUpdatedEventArgs>,
    IEventHandler<VoiceStateUpdatedEventArgs>
{
    private readonly DiscordClientWrapper wrapper = (DiscordClientWrapper)wrapper;

    public async Task HandleEventAsync(DiscordClient sender, GuildDownloadCompletedEventArgs eventArgs)
        => await wrapper.OnGuildDownloadCompleted(sender, eventArgs);

    public async Task HandleEventAsync(DiscordClient sender, VoiceServerUpdatedEventArgs eventArgs)
        => await wrapper.OnVoiceServerUpdated(sender, eventArgs);

    public async Task HandleEventAsync(DiscordClient sender, VoiceStateUpdatedEventArgs eventArgs)
        => await wrapper.OnVoiceStateUpdated(sender, eventArgs);
}
