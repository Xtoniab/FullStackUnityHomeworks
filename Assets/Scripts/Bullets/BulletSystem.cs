using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
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
            RemoveBullets(bullet => !bullet.IsAlive);
            RemoveBullets(bullet => !levelBounds.InBounds(bullet.transform.position));
        }

        private void RemoveBullets(Func<Bullet, bool> removePredicate)
        {
            activeBullets.ToList().Where(removePredicate).ForEach(RemoveBullet);
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