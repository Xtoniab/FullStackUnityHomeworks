using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class Weapon
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private BulletConfig bulletConfig;

        public bool CanFire { get; set; } 

        private TeamTag team;
        private BulletSystem bulletSystem;
        
        public void Construct(BulletSystem bulletSystem, TeamTag team)
        {
            this.team = team;
            this.bulletSystem = bulletSystem;
        }
        
        public void Fire(GameObject target = null)
        {
            if (!this.CanFire)
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