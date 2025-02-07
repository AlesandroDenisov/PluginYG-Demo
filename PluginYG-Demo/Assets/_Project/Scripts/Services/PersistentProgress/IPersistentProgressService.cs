using HomoLudens.Data;
using System;

namespace HomoLudens.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}