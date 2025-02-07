using HomoLudens.Data;
using HomoLudens.Services.PersistentProgress;
using HomoLudens.Services.SaveLoad;

namespace HomoLudens.Core.States
{
    public class LoadProgressState : IState
    {
        private const string MainScene = "SampleScene";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadAsyncService _saveLoadProgress;
        private readonly ICoroutineRunner _coroutineRunner;

        public LoadProgressState( GameStateMachine gameStateMachine
                                , IPersistentProgressService progressService
                                , ISaveLoadAsyncService saveLoadProgress
                                , ICoroutineRunner coroutineRunner      
                                )
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
            _saveLoadProgress.LoadProgressOrInitNewAsync();
//#if !UNITY_EDITOR
//            _saveLoadProgress.LoadProgressOrInitNewAsync();
//#elif UNITY_EDITOR
//            _saveLoadProgress.LoadProgressOrInitNew();
//#endif
            _gameStateMachine.Enter<LoadLevelState, string>("SampleScene");
        //    _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

/*        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadProgress.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            return new PlayerProgress(initialLevel: MainScene);
        } */
    }
}