using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemyAttackBehaviour
    {
        [SerializeField]
        private float fireCooldown = 1.0f;
        
        private Ship ship;
        private GameObject target;
        private float currentTime;
        
        public void Construct(Ship ship, GameObject target)
        {
            this.ship = ship;
            this.target = target;
            
            ResetCooldown();
        }

        public void ResetCooldown()
        {
            this.currentTime = this.fireCooldown;
        }

        public void FixedTick()
        {
            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                this.ship.FireTarget(this.target);
                this.currentTime += this.fireCooldown;
            }
        }
    }
}