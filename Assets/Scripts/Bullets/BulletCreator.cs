using Pool;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletCreator : MonoBehaviour, IBulletReleaseCallback
    {
        [SerializeField] private GameObjectPool bulletPool;
        [SerializeField] private Transform worldTransform;

        public Bullet Create(BulletSpawnOptions options)
        {
            var bulletObject = this.bulletPool.Get();
            var bullet = bulletObject.GetComponent<Bullet>();
            
            bullet.SetParent(this.worldTransform);

            bullet.Construct(options, callback: this);
            
            return bullet;
        }

        public void OnBulletRelease(Bullet bullet)
        {
            this.bulletPool.Pool(bullet.gameObject);
        }
    }
}