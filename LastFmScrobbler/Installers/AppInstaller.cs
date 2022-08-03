using SiraUtil;
using LastFmScrobbler.Config;
using Zenject;
using Logger = IPA.Logging.Logger;

namespace LastFmScrobbler.Installers
{
    class AppInstaller : Installer
    {
        private readonly MainConfig _config;

        private AppInstaller(MainConfig config)
        {
            _config = config;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_config).AsSingle();
            //Container.BindInterfacesAndSelfTo<TrickSaberPlugin>().AsSingle();
        }
    }
}
