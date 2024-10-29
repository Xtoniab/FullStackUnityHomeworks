using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class RigidbodyMovement
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 5.0f;

        public bool CanMove { get; set; }

        public void Move(Vector2 vector)
        {
            if (CanMove == false)
            {
                return;
            }

            var nextPosition = this.rigidbody2D.position + vector * this.speed;
            this.rigidbody2D.MovePosition(nextPosition);
        }
    }
}