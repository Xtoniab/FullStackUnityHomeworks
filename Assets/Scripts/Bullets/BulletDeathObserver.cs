using Pool;
using UnityEngine;

namespace ShootEmUp
{
    // Комментарий такой же как в EnemyDeathObserver.cs
    // Коротко: в Creator негде делать отписку от смерти, а на слои ниже уже не хочется делать зависимости на Pool
    public class BulletDeathObserver: MonoBehaviour
    {
        [SerializeField] private Bullet bullet;
        
        private GameObjectPool bulletPool;

        public void Construct(GameObjectPool bulletPool)
        {
            this.bulletPool = bulletPool;
        }
        
        private void OnEnable()
        {
            this.bullet.OnRelease += OnBulletDeath;
        }

        private void OnDisable()
        {
            this.bullet.OnRelease -= OnBulletDeath;
        }

        private void OnBulletDeath(Bullet bullet)
        {
            this.bulletPool.Pool(bullet.gameObject);
        }
    }
}