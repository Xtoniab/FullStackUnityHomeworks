using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemyAttackBehaviour
    {
        [SerializeField]
        private float fireCooldown = 1.0f;

        public event Action<GameObject> OnFire;
        
        private GameObject target;
        private float currentTime;
        
        public void SetTarget(GameObject target)
        {
            this.target = target;
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
                OnFire?.Invoke(this.target);
                this.currentTime += this.fireCooldown;
            }
        }
    }
}