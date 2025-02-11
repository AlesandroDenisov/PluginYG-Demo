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
        private readonly ISaveLoadService _saveLoadProgress;
        private readonly ICoroutineRunner _coroutineRunner;

        public LoadProgressState(GameStateMachine gameStateMachine
                                , IPersistentProgressService progressService
                                , ISaveLoadService saveLoadProgress
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
            _saveLoadProgress.LoadProgressOrInitNew(5, OnLoadedProgress);
        }

        public void Exit()
        {
        }

        private void OnLoadedProgress()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("SampleScene");
            //    _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }
    }
}
