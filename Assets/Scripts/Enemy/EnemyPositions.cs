using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace ShootEmUp
{
    /// <summary>
    /// Данный класс решает проблему одной и той же позиции атаки, которая может быть занята несколькими врагами одновременно.
    /// Для этого мы "занимаем" позицию врагом и в это время она не доступна для других врагов.
    /// </summary>
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPositions;
        [SerializeField] private Transform[] attackPositions;
        
        public bool HasAvailableAttackPositions => availableAttackPositions.Count > 0;
        
        private HashSet<Transform> availableAttackPositions;
        
        private void Awake()
        {
            availableAttackPositions = new HashSet<Transform>(attackPositions);
        }
        
        public Transform RandomSpawnPosition()
        {
            return this.spawnPositions.PickRandomElement();
        }

        public AttackPositionHandle AcquireRandomAttackPosition()
        {
            if (availableAttackPositions.Count == 0)
            {
                Debug.LogError("No available attack positions!");
                return null;
            }
            
            var attackPosition = availableAttackPositions.PickRandomElement();
            availableAttackPositions.Remove(attackPosition);

            return new AttackPositionHandle(attackPosition, AddAvailableAttackPosition);
        }
        
        public void AddAvailableAttackPosition(Transform attackPosition)
        {
            if (attackPosition != null)
            {
                availableAttackPositions.Add(attackPosition);
            }
        }
    }
}