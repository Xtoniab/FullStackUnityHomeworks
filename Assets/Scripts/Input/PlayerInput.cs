using System;
using UnityEngine;

namespace ShootEmUp
{
    public class PlayerInput: MonoBehaviour
    {
        public event Action OnFire;
        public Vector2 MoveDirection { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFire?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.MoveDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.MoveDirection = Vector2.right;
            }
            else
            {
                this.MoveDirection = Vector2.zero;
            }
        }
    }
}