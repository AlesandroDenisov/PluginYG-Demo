using HomoLudens.Core.States;
using UnityEngine;

namespace HomoLudens.Core
{
    public class GameEntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        public LoadingCurtain CurtainPrefab;

        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
