using System;
using UnityEngine;

namespace ShootEmUp.Brain
{
    public class EnemyBrain: MonoBehaviour
    {
        [SerializeField] private EnemyAttackBehaviour attackBehaviour;
        [SerializeField] private EnemyMovementBehaviour movementBehaviour;

        public event Action<GameObject> OnFire;

        public void Construct(Vector2 attackPosition, GameObject attackTarget)
        {
            this.movementBehaviour.SetDestination(attackPosition);
            this.attackBehaviour.SetTarget(attackTarget);
            this.attackBehaviour.ResetCooldown();

            this.attackBehaviour.OnFire += target => OnFire?.Invoke(target);
        }

        public Vector2 GetMoveDirection()
        {
            return this.movementBehaviour.GetMoveDirection();
        }

        public void FixedTick()
        {
            if (this.movementBehaviour.IsReached)
            {
                attackBehaviour.FixedTick();
            }
        }
    }
}