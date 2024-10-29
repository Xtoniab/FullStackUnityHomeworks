using Pool;
using UnityEngine;

namespace ShootEmUp
{
    // Этот класс нужен для того, чтобы отслеживать смерть врага и возвращать его в пул объектов, освобождая позицию аттаки.
    // 1. Напрямую подписываться в EnemyCreator плохо, ведь там негде делать отписку + совсем капельку нарушается SRP
    // 2. Решение без ивентов, а с делегатом OnDeath это не ООП
    // 3. Выносить логику с пулом итп в сам Enemy то же не хочется
    // 4. Есть ещё решение насильно резетать ивент. Например OnDeath = null. Но мне оно не нравится. 
    // Поэтому сделал такой класс
    public class EnemyDeathObserver : MonoBehaviour
    {
        [SerializeField] private Ship enemy;

        private AttackPositionHandle attackPositionHandle;
        private GameObjectPool enemyPool;

        public void Construct(AttackPositionHandle attackPositionHandle, GameObjectPool enemyPool)
        {
            this.attackPositionHandle = attackPositionHandle;
            this.enemyPool = enemyPool;
        }

        private void OnEnable()
        {
            this.enemy.OnDeath += OnEnemyDeath;
        }

        private void OnDisable()
        {
            this.enemy.OnDeath -= OnEnemyDeath;
        }

        private void OnEnemyDeath()
        {
            this.attackPositionHandle.Release();
            this.enemyPool.Pool(this.gameObject);
        }
    }
}