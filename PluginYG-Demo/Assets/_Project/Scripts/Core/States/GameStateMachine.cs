using System;
using System.Collections.Generic;
using HomoLudens.Services;
using HomoLudens.Services.PersistentProgress;
using HomoLudens.Services.SaveLoad;

namespace HomoLudens.Core.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(  SceneLoader sceneLoader
                                , LoadingCurtain loadingCurtain
                                , AllServices services
                                , ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState( this
                                                             , sceneLoader
                                                             , services
                                                             , coroutineRunner
                                                             ),
                [typeof(LoadProgressState)] = new LoadProgressState( this
                                                                   , services.Single<IPersistentProgressService>()
                                                                   , services.Single<ISaveLoadService>()
                                                                   , coroutineRunner
                                                                   ),
                [typeof(LoadLevelState)] = new LoadLevelState(this
                                                            , sceneLoader
                                                            , loadingCurtain
                                                            //, services.Single<IGameFactory>()
                                                            , services.Single<IPersistentProgressService>()
  //                                                          , services.Single<IStaticDataService>()
                                                            //, services.Single<IUIFactory>()
                                                            ),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;

    }
}
