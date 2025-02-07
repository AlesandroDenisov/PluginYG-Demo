using HomoLudens.Data;

namespace HomoLudens.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
        void LoadProgressOrInitNew();
    }
}
