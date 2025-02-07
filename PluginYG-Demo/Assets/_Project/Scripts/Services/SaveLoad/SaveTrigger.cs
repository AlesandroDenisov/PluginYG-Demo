using System.Collections;
using UnityEngine;

namespace HomoLudens.Services.SaveLoad
{
    [RequireComponent(typeof(Collider2D))]
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadAsyncService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadAsyncService>();
            if (_saveLoadService == null)
            {
                Debug.LogError("[SaveTrigger] ISaveLoadAsyncService not initialized!");
                StartCoroutine(WaitForSaveLoadService());
            }
            else
            {
                Debug.Log("[SaveTrigger] ISaveLoadAsyncService initialized!");
            }
        }

        private IEnumerator WaitForSaveLoadService()
        {
            while (_saveLoadService == null)
            {
                Debug.LogWarning("[SaveTrigger] Waiting for ISaveLoadAsyncService initialization...");
                yield return null;
            }
            _saveLoadService = AllServices.Container.Single<ISaveLoadAsyncService>();
            Debug.Log("[SaveTrigger] ISaveLoadAsyncService successfully initialized.");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _saveLoadService.SaveProgressCoroutine();
                Debug.Log("Progress saved in SaveTrigger!");
            }
        }

/*        private void OnDrawGizmos()
        {
            if(!Collider) return;
      
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }*/
    }
}