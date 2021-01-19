﻿using System;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using SiraUtil.Converters;
using Version = SemVer.Version;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace LastFmScrobbler.Config
{
    public class MainConfig
    {
        public Action? OnChanged;

        [NonNullable]
        [UseConverter(typeof(VersionConverter))]
        public Version Version { get; set; } = new("0.0.0");

        public virtual string? SessionName { get; set; } = null;

        public virtual string? SessionKey { get; set; } = null;

        public virtual int SongScrobbleLength { get; set; } = 50;

        public bool IsAuthorized()
        {
            return SessionName is not null && SessionKey is not null;
        }

        public virtual void Changed()
        {
            OnChanged?.Invoke();
        }
    }
}