using HomoLudens.Data;
using System;
using System.Collections;

namespace HomoLudens.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        void LoadProgress(Action onLoaded = null);
        void LoadProgressOrInitNew(uint waitingTimeSeconds, Action onLoaded = null);

        IEnumerator WaitingLoadProgress(uint waitingTimeSeconds, Action onLoaded = null);

        void ResetProgress();
    }
}
