namespace Lavalink4NET.Players.Queued;

public enum TrackHistoryBehavior : byte
{
    /// <summary>
    ///     Denotes that all tracks should be added to the history. This indicates that each time a track is completed, it will
    ///     be added to the history.
    /// </summary>
    Full,

    /// <summary>
    ///     Denotes that all tracks that finished playing and were not looped before will be added to the history. This will
    ///     avoid duplicates when using the track repeat mode <see cref="TrackRepeatMode.Track" />.
    /// </summary>
    Filtered,

    /// <summary>
    ///     Denotes that all tracks that finished playing and were not looped before will be added to the history. This will
    ///     avoid duplicates when using the track repeat mode <see cref="TrackRepeatMode.Track" /> or <see cref="TrackRepeatMode.Queue"/> when the queue is empty.
    /// </summary>
    Adaptive,
}
