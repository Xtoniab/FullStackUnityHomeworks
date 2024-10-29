using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private BulletCreator bulletCreator;
        
        private readonly HashSet<Bullet> activeBullets = new();

        private void FixedUpdate()
        {
            RemoveDeadBullets();
        }

        private void RemoveDeadBullets()
        {
            foreach (var bullet in activeBullets.ToList())
            {
                var alive = bullet.IsAlive;
                var inBounds = levelBounds.InBounds(bullet.transform.position);
                
                if (!alive || !inBounds)
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void SpawnBullet(BulletSpawnOptions options)
        {
            var bullet = this.bulletCreator.Create(options);
            this.activeBullets.Add(bullet);
        }

        public void RemoveBullet(Bullet bullet)
        {
            if (this.activeBullets.Remove(bullet))
            {
                bullet.Release();
            }
        }
    }
}