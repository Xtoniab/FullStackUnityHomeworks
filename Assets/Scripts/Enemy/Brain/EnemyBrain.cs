using UnityEngine;

namespace ShootEmUp.Brain
{
    public class EnemyBrain: MonoBehaviour
    {
        [SerializeField] private Ship ship;
        [SerializeField] private EnemyAttackBehaviour attackBehaviour;
        [SerializeField] private EnemyMovementBehaviour movementBehaviour;
        
        public void Construct(Vector2 attackPosition, GameObject attackTarget)
        {
            this.movementBehaviour.Construct(ship, attackPosition);
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
    }
}