using HomoLudens.Services;
using HomoLudens.Services.PersistentProgress;
using HomoLudens.Services.SaveLoad;

namespace HomoLudens.Core.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "InitialScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState( GameStateMachine stateMachine
                             , SceneLoader sceneLoader
                             , AllServices services
                             , ICoroutineRunner coroutineRunner
                             )
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        // Register all services.
        // Always start with the scene 'InitialScene'.
        public void Enter()
        {
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }
        public void Exit()
        {
        }

        private void RegisterServices()
        {
            RegisterStaticDataService();
//            _services.RegisterSingle<IInputService>(InputService());
//            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
/*            _services.RegisterSingle<IUIFactory>(new UIFactory( _services.Single<IAssetProvider>()
                                                              , _services.Single<IStaticDataService>()
                                                              , _services.Single<IPersistentProgressService>()) );*/
//            _services.RegisterSingle<IUIWindowService>(new UIWindowService(_services.Single<IUIFactory>()) );
 /*           _services.RegisterSingle<IGameFactory>(new GameFactory( _services.Single<IAssetProvider>()
                                                                  , _services.Single<IStaticDataService>()
                                                                  , _services.Single<IPersistentProgressService>()
                                                                  , _services.Single<IUIWindowService>()
                                                                  ));*/
            _services.RegisterSingle<ISaveLoadAsyncService>(new YGSaveLoadAsyncService( _services.Single<IPersistentProgressService>()
                                                                                      , _coroutineRunner
//                                                                                 , _services.Single<IGameFactory>()) );
                                                        ));
        }

        // After loading InitialScene move to new state LoadProgressState
        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterStaticDataService()
        {
            //IStaticDataService staticData = new StaticDataService();
            //staticData.LoadEnemies();
            //_services.RegisterSingle(staticData);
        }

/*        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();*/
            /*            #if UNITY_STANDALONE
                                    return new StandaloneInputService();
                        #elif UNITY_IOS || UNITY_ANDROID
                                    return new MobileInputService();
                        #endif*/
        //}
    }
}