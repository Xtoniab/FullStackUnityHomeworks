using Pool;
using ShootEmUp.Brain;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyCreator : MonoBehaviour, IEnemyReleaseCallback
    {
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private GameObjectPool enemyPool;
        [SerializeField] private GameObject character;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private Transform worldTransform;

        public bool TryCreate(out Ship enemy)
        {
            if (!this.enemyPositions.HasAvailableAttackPositions)
            {
                enemy = null;
                return false;
            }

            var attackPositionHandle = this.enemyPositions.AcquireRandomAttackPosition();
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();

            var enemyGameObject = this.enemyPool.Get();
            var enemyTransform = enemyGameObject.transform;
            var ship = enemyTransform.GetComponent<Ship>();
            var brain = enemyTransform.GetComponent<EnemyBrain>();

            enemyTransform.SetParent(this.worldTransform);
            enemyTransform.position = spawnPosition.position;

            ship.Construct(bulletSystem);
            brain.Construct(attackPositionHandle, this.character, this);
            
            enemy = ship;

            return true;
        }

        public void OnEnemyRelease(Ship enemy)
        {
            this.enemyPool.Pool(enemy.gameObject);
        }
    }
}