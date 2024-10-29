using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemyMovementBehaviour
    {
        private const float TargetPositionToleranceSqr = 0.25f * 0.25f;
        
        [SerializeField]
        private Transform transform;
        
        public bool IsReached { get; private set; }

        private Ship ship;
        private Vector2 destination;
        
        public void Construct(Ship ship, Vector2 endPoint)
        {
            this.ship = ship;
            this.destination = endPoint;
            this.IsReached = false;
        }
        
        public void FixedTick()
        {
            if (this.IsReached)
            {
                return;
            }
            
            this.ship.Move(GetMoveDirection());
        }
        
        private Vector2 GetMoveDirection()
        {
            if (this.IsReached)
            {
                return Vector2.zero;
            }
            
            var vector = this.destination - (Vector2) this.transform.position;
            if (vector.sqrMagnitude <= TargetPositionToleranceSqr)
            {
                this.IsReached = true;
                return Vector2.zero;
            }

            return vector.normalized;
        }
    }
}