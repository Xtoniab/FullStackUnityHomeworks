using Pool;
using ShootEmUp.Brain;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyCreator : MonoBehaviour
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
            var deathObserver = enemyTransform.GetComponent<ShipDeathObserver>();
            var brain = enemyTransform.GetComponent<EnemyBrain>();

            enemyTransform.SetParent(this.worldTransform);
            enemyTransform.position = spawnPosition.position;

            ship.Construct(bulletSystem);
            
            brain.Construct(
                attackPosition: attackPositionHandle.Value,
                attackTarget: this.character
            );

            deathObserver.Construct(_ =>
            {
                attackPositionHandle.Release();
                enemyPool.Pool(enemyGameObject);
            });

            enemy = ship;

            return true;
        }
    }
}