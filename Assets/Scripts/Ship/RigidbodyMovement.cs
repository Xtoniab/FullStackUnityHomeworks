using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class RigidbodyMovement
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 5.0f;

        private Func<bool> canMove;
        
        public void Construct(Func<bool> moveCondition)
        {
            this.canMove = moveCondition;
        }
        
        public void Move(Vector2 vector)
        {
            if (!this.canMove())
            {
                return;
            }
            
            var nextPosition = this.rigidbody2D.position + vector * this.speed;
            this.rigidbody2D.MovePosition(nextPosition);
        }
    }
}