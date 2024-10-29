using System;
using UnityEngine;

namespace ShootEmUp
{
    public class AttackPositionHandle
    {
        public Vector3 Value => attackPosition.position;
        
        private readonly Transform attackPosition;
        private readonly Action<Transform> releaseAction;
        
        private bool isReleased;
        
        public AttackPositionHandle(Transform attackPosition, Action<Transform> releaseAction)
        {
            this.attackPosition = attackPosition;
            this.releaseAction = releaseAction;
        }
        
        public void Release()
        {
            if (isReleased)
            {
                throw new Exception("Attack position is already released!");
            }
            
            releaseAction?.Invoke(attackPosition);
            isReleased = true;
        }
    }
}