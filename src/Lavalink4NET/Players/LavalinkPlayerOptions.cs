﻿using System;

namespace Lavalink4NET.Players;

using Lavalink4NET.Rest.Entities.Tracks;

public record class LavalinkPlayerOptions
{
    public bool DisconnectOnStop { get; set; }

    public bool DisconnectOnDestroy { get; set; } = true;

    public string? Label { get; set; }

    public ITrackQueueItem? InitialTrack { get; set; }

    public TimeSpan? InitialPosition { get; set; }

    public TrackLoadOptions InitialLoadOptions { get; set; }

    public float? InitialVolume { get; set; }

    public bool SelfDeaf { get; set; }

    public bool SelfMute { get; set; }
}