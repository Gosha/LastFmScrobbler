﻿using System;
using BeatSaberMarkupLanguage.Attributes;
using LastFmScrobbler.Config;
using LastFmScrobbler.Managers;
using SiraUtil;
using Zenject;

namespace LastFmScrobbler.UI
{
    [ViewDefinition("LastFmScrobbler.UI.Views.config-view.bsml")]
    [HotReload(RelativePathToLayout = @"\Views\config-view.bsml")]
    public class ScrobblerConfigView : AbstractView
    {
        public event Action<bool>? AuthClicked = null;

        [Inject] private readonly MainConfig _config = null!;
        [Inject] private readonly LastFmClient _lastFmClient = null!;
        [Inject] private readonly NotAuthorizedView _notAuthorizedView = null!;

        private bool _authorized;
        [UIValue("authorized")]
        public bool Authorized
        {
            get => _authorized;
            set
            {
                _authorized = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(AuthText));
                NotifyPropertyChanged(nameof(AuthColor));
            }
        }

        [UIValue("auth-text")] public string AuthText => Authorized ? "Authorized" : "Not authorized";
        [UIValue("auth-color")] public string AuthColor => Authorized ? "#baf2bb" : "#BD288199";

        public async void Initialize()
        {
            Authorized = _config.IsAuthorized();

            if (_lastFmClient.AuthTokenTask == null) return;

            // Wait for 3 PauseChamps and BSML
            await Utilities.PauseChamp;
            await Utilities.PauseChamp;
            await Utilities.PauseChamp;

            SafeAwait(_lastFmClient.AuthTokenTask, ConsumeToken);
        }

        [UIAction("clicked-show-auth-button")]
        protected void ClickedShow()
        {
            _log.Debug("Auth clicked");
            AuthClicked?.Invoke(Authorized);
        }

        private void ConsumeToken(string token)
        {
            _notAuthorizedView.authButton.interactable = true;
            _notAuthorizedView.token = token;
        }
    }
}