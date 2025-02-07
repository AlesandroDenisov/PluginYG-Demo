using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HomoLudens.Core
{
    /// <summary>
    /// This class is used in state machine states to move around scenes
    /// </summary>
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) 
        { 
            _coroutineRunner = coroutineRunner;
        } 

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(WaitingLoadScene(name, onLoaded));
        }

        /// <summary>
        /// This method requests for load scene async and waits until it is done.
        /// </summary>
        /// <param name="nextScene"></param>
        /// <param name="onLoaded"></param>
        /// <returns></returns>
        public IEnumerator WaitingLoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
        
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            // Check an operation state every frame
            // until the next scene loading is not done
            while (!waitNextScene.isDone)
                yield return null;
      
            onLoaded?.Invoke();
        }
    }
}