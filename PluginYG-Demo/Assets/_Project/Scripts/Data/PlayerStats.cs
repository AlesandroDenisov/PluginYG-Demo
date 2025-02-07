using System;

namespace HomoLudens.Data
{
    [Serializable]
    public class PlayerStats
    {
        public Action Changed;

        private float CurrentHP;
        private float MaxHP = 1;
        private float Speed;

        public void ResetHP()
        {
            CurrentHP = MaxHP;
        }

        public void UpgradeMaxHP(float value)
        {
            MaxHP += value;
            Changed?.Invoke();
        }
    }
}
