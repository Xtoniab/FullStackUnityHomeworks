using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class Weapon
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private BulletConfig bulletConfig;
        
        private TeamTag team;
        private Func<bool> canFire;
        private BulletSystem bulletSystem;
        
        public void Construct(BulletSystem bulletSystem, TeamTag team, Func<bool> fireCondition)
        {
            this.team = team;
            this.canFire = fireCondition;
            this.bulletSystem = bulletSystem;
        }
        
        public void Fire(GameObject target = null)
        {
            if (!this.canFire())
            {
                return;
            }
            
            var direction = target != null 
                ? (target.transform.position - this.firePoint.position).normalized 
                : firePoint.rotation * Vector3.up;
            
            bulletSystem.SpawnBullet(new BulletSpawnOptions
            {
                teamTag = team,
                physicsLayer = (int) this.bulletConfig.physicsLayer,
                color = this.bulletConfig.color,
                damage = this.bulletConfig.damage,
                position = firePoint.position,
                velocity = direction * this.bulletConfig.speed
            });
        }
    }
}