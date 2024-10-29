using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Ship : MonoBehaviour
    {
        public event Action OnDeath;

        [SerializeField] private Team team;
        [SerializeField] private Weapon weapon;
        [SerializeField] private Health health;
        [SerializeField] private RigidbodyMovement movement;
        public TeamTag TeamTag => this.team.TeamTag;

        public void Construct(BulletSystem bulletSystem)
        {
            this.weapon.Construct(bulletSystem, this.team.TeamTag, fireCondition: () => this.health.IsAlive());
            this.movement.Construct(moveCondition: () => this.health.IsAlive());
            this.health.Construct();
        }

        private void OnEnable()
            => this.health.OnDeath += this.OnShipDeath;

        private void OnDisable()
            => this.health.OnDeath -= this.OnShipDeath;

        public void Move(Vector2 direction)
            => this.movement.Move(direction * Time.fixedDeltaTime);

        public bool TakeDamage(int damage)
            => this.health.TakeDamage(damage);

        public void FireForward()
            => this.weapon.Fire();

        public void FireTarget(GameObject target)
            => this.weapon.Fire(target);

        private void OnShipDeath() =>
            this.OnDeath?.Invoke();
    }
}