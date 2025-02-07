using HomoLudens.Data;
using System;
using UnityEngine;

namespace HomoLudens.Services.PersistentProgress
{
    // This class stores a reference to the player's progress
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}