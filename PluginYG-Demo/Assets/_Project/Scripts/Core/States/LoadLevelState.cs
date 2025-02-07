using HomoLudens.Services.PersistentProgress;

namespace HomoLudens.Core.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine
                            , SceneLoader sceneLoader
                            , LoadingCurtain loadingCurtain
                            , IPersistentProgressService progressService)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            //_gameFactory.Cleanup(); // clear progress cache, when loading a new scene
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
//            InitUIRoot();
//            InitGameWorld();
//            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        //        private const string InitialPointTag = "InitialPoint";
        //        private const string LootSpawnerTag = "LootSpawner";
        //        private const string BuildingSpawnerTag = "BuildingSpawner";
        //        private const string EnemySpawnerTag = "EnemySpawner";

        //        private readonly GameStateMachine _stateMachine;
        //        private readonly SceneLoader _sceneLoader;
        //        private readonly LoadingCurtain _loadingCurtain;
        //        private readonly IGameFactory _gameFactory;
        //        private readonly IStaticDataService _staticData;
        //        private readonly IUIFactory _uiFactory;

        //        public LoadLevelState(GameStateMachine gameStateMachine
        //                            , SceneLoader sceneLoader
        //                            , LoadingCurtain loadingCurtain
        //                            , IGameFactory gameFactory
        //                            , IPersistentProgressService progressService
        //                            , IStaticDataService staticDataService
        //                            , IUIFactory uiFactory)
        //        {
        //            _stateMachine = gameStateMachine;
        //            _sceneLoader = sceneLoader;
        //            _loadingCurtain = loadingCurtain;
        //            _gameFactory = gameFactory;
        //            _progressService = progressService;
        //            _staticData = staticDataService;
        //            _uiFactory = uiFactory;
        //        }

        //        public void Enter(string sceneName)
        //        {
        //            _loadingCurtain.Show();
        //            _gameFactory.Cleanup(); // clear progress cache, when loading a new scene
        //            _sceneLoader.Load(sceneName, OnLoaded);
        //        }

        //        public void Exit()
        //        {
        //            _loadingCurtain.Hide();
        //        }

        //        private void OnLoaded()
        //        {
        //            InitUIRoot();
        //            InitGameWorld();
        //            InformProgressReaders();

        //            _stateMachine.Enter<GameLoopState>();
        //        }

        //        private void InitUIRoot()
        //        {
        //            _uiFactory.CreateUIRoot();
        //        }

        //        // Notify all readers about current player's progress
        //        private void InformProgressReaders()
        //        {
        //            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        //                progressReader.LoadProgress(_progressService.Progress);
        //        }

        //        private void InitGameWorld()
        //        {
        //            InitSpawners();
        //            InitLootComponents();
        //            GameObject playerCar = _gameFactory.CreatePlayer(GameObject.FindWithTag(InitialPointTag));
        //            InitHud(playerCar);
        //            CameraFollow(playerCar);
        //        }

        //        private void InitSpawners()
        //        {
        ///*            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(LootSpawnerTag))
        //            {
        //                var lootSpawner = spawnerObject.GetComponent<LootSpawner>();

        //                if (lootSpawner != null)
        //                {
        //                    lootSpawner.Construct(_gameFactory);
        //                }
        //                else
        //                {
        //                    Debug.LogError("LoadLevelState - InitSpawners(): LootSpawner component missing.");
        //                }
        //                //_gameFactory.Register(lootSpawner);
        //            }*/

        //            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(BuildingSpawnerTag))
        //            {
        //                // TODO: add a call Construct() to BuildingSpawner and then call Register() here

        //                var lootSpawner = spawnerObject.GetComponent<LootSpawner>();

        //                //_gameFactory.Register(buildingProduce); 

        //                if (lootSpawner != null)
        //                {
        //                    lootSpawner.Construct(_gameFactory);
        //                }
        //                else
        //                {
        //                    Debug.LogError("BuildingProduce or LootSpawner component missing on BuildingSpawner.");
        //                }
        //            }

        //            foreach (GameObject enemySpawnerObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
        //            {
        //                var enemySpawner = enemySpawnerObject.GetComponent<EnemySpawner>();
        //                _gameFactory.Register(enemySpawner);
        //            }
        //        }

        //        private void InitLootComponents()
        //        {
        //            foreach (var dict in _progressService.Progress.WorldData.ResourceData.LootPiecesOnScene.Dictionary)
        //            {
        //                string key = dict.Key;
        //                LootComponentData lootComponentData = dict.Value;
        //                ResourceType resourceType = lootComponentData.Loot.ResourceType;
        //                LootComponent loot = _gameFactory.CreateLoot(resourceType);
        //                loot.GetComponent<UniqueId>().Id = key;
        //                loot.transform.position = lootComponentData.Position.AsUnityVector();
        //            }
        //        }

        //        private void InitHud(GameObject playerCar)
        //        {
        //            if (playerCar == null)
        //            {
        //                Debug.LogError("LoadLevelState - InitHud: playerCar is null");
        //                return;
        //            }

        //            GameObject hud = _gameFactory.CreateHud();

        //            // TODO: Add Exp bar, Offer bar, etc.
        //            //hud.GetComponentInChildren<ActorUI>().Construct(playerCar.GetComponent<PlayerHealth>());
        //        }

        //        private void CameraFollow(GameObject playerCar)
        //        {
        //            if (playerCar == null)
        //            {
        //                Debug.LogError("LoadLevelState - CameraFollow: playerCar is null");
        //                return;
        //            }

        //            Camera.main.GetComponent<CameraFollow>().Follow(playerCar);
        //        }
    }
}