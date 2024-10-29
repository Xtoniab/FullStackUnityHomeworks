using Pool;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletCreator : MonoBehaviour
    {
        [SerializeField] private GameObjectPool bulletPool;
        [SerializeField] private Transform worldTransform;

        public Bullet Create(BulletSpawnOptions options)
        {
            var bulletObject = this.bulletPool.Get();
            var bullet = bulletObject.GetComponent<Bullet>();
            var deathObserver = bulletObject.GetComponent<BulletDeathObserver>();
            
            bullet.SetParent(this.worldTransform);

            bullet.Construct(options);
            deathObserver.Construct(this.bulletPool);
            
            return bullet;
        }
    }
}