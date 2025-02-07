using System.Collections;

namespace HomoLudens.Services.SaveLoad
{
    public interface ISaveLoadAsyncService : ISaveLoadService
    {
        void LoadProgressOrInitNewAsync();
        IEnumerator SaveProgressCoroutine();
        IEnumerator LoadProgressCoroutine();
    }
}
