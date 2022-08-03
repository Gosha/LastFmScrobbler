using IPA;
using IPA.Config.Stores;
using IPA.Loader;
using IPA.Logging;
using LastFmScrobbler.Config;
using SiraUtil;
using SiraUtil.Zenject;
using IPAConfig = IPA.Config.Config;

namespace LastFmScrobbler
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        private readonly Logger _log;

        [Init]
        public Plugin(IPAConfig cfg, Logger log, Zenjector injector)
        {
            _log = log;

            var config = cfg.Generated<MainConfig>();

            injector.UseLogger(log);
            injector.Install<Installers.AppInstaller>(Location.App, config);
            injector.Install<Installers.MenuInstaller>(Location.Menu);

            _log.Debug("Finished plugin initialization");
        }

        [OnEnable]
        [OnDisable]
        public void Nop()
        {
        }
    }
}