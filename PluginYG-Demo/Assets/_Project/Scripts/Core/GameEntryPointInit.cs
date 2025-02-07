using UnityEngine;

namespace HomoLudens.Core
{
    /// <summary>
    /// Checks if the game bootstrapper GameEntryPoint object is present in the scene.
    /// If the object is absent, creates it from the predefined EntryPointPrefab.
    /// </summary>
    public class GameEntryPointInit : MonoBehaviour
    {
        public GameEntryPoint EntryPointPrefab;
        private void Awake()
        {
            var entryPoint = FindObjectOfType<GameEntryPoint>();

            if (entryPoint != null) return;

            Instantiate(EntryPointPrefab);
        }
    }
}
