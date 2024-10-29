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

            bullet.transform.SetParent(this.worldTransform);

            bullet.Construct(
                options: options,
                onRelease: () => this.bulletPool.Pool(bulletObject)
            );
            
            return bullet;
        }
    }
}