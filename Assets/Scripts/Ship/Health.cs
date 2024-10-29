using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class Health
    {
        public event Action OnDeath;

        [SerializeField] private int maxHitPoints;

        private int hitPoints;

        public void Construct()
        {
            this.hitPoints = this.maxHitPoints;
        }

        public bool IsAlive()
        {
            return this.hitPoints > 0;
        }

        public bool TakeDamage(int damage)
        {
            if (!this.IsAlive())
            {
                return false;
            }

            this.hitPoints -= damage;
            
            if (this.hitPoints <= 0)
            {
                this.OnDeath?.Invoke();
            }

            return true;
        }
    }
}