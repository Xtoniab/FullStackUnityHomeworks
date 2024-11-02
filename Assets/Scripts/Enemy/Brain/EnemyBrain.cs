using UnityEngine;

namespace ShootEmUp.Brain
{
    public class EnemyBrain: MonoBehaviour
    {
        [SerializeField] private Ship ship;
        [SerializeField] private EnemyAttackBehaviour attackBehaviour;
        [SerializeField] private EnemyMovementBehaviour movementBehaviour;
        
        private AttackPositionHandle attackPositionHandle;
        private IEnemyReleaseCallback callback;
        
        public void Construct(AttackPositionHandle attackPosition, GameObject attackTarget, IEnemyReleaseCallback callback)
        {
            this.attackPositionHandle = attackPosition;
            this.callback = callback;
            
            this.movementBehaviour.Construct(ship, attackPosition.Value);
            this.attackBehaviour.Construct(ship, attackTarget);
        }
        
        private void FixedUpdate()
        {
            if (this.movementBehaviour.IsReached)
            {
                attackBehaviour.FixedTick();
            }
            else
            {
                this.movementBehaviour.FixedTick();
            }
        }
        
        private void OnEnable() => this.ship.OnDeath += ReleaseEnemy;
        private void OnDisable() => this.ship.OnDeath -= ReleaseEnemy;
        private void ReleaseEnemy()
        {
            this.attackPositionHandle.Release();
            this.callback.OnEnemyRelease(this.ship);
        }
    }
}