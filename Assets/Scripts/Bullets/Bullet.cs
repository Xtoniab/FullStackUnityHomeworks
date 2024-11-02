using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public bool IsAlive { get; private set; }

        private TeamTag teamTag;
        private int damage;
        private IBulletReleaseCallback callback;

        public void Construct(BulletSpawnOptions options, IBulletReleaseCallback callback)
        {
            this.IsAlive = true;
            this.callback = callback;
            
            SetPosition(options.position);
            SetColor(options.color);
            SetPhysicsLayer(options.physicsLayer);
            SetDamage(options.damage);
            SetTeamTag(options.teamTag);
            SetVelocity(options.velocity);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Unity будет обработает все коллизии на тике, даже если объект стал неактивен после обработки первой
            // Поэтому такой флажок маст хев
            if (!this.IsAlive)
            {
                return;
            }
            
            TryDealDamage(collision.gameObject);
        }

        private bool TryDealDamage(GameObject target)
        {
            if (!target.TryGetComponent(out Ship ship))
            {
                return false;
            }

            if (this.teamTag == ship.TeamTag)
            {
                return false;
            }

            if (!ship.TakeDamage(this.damage))
            {
                return false;
            }
            
            IsAlive = false;
            return true;
        }
        
        public void SetDamage(int damage)
        {
            if (damage >= 0)
            {
                this.damage = damage;
            }
            else
            {
                throw new ArgumentException("Damage can't be negative");
            }
        }
        
        public void Release()
        {
            callback.OnBulletRelease(this);
        }
        
        public void SetTeamTag(TeamTag teamTag) => this.teamTag = teamTag;
        public void SetVelocity(Vector2 velocity) => this.rigidbody2D.velocity = velocity;
        public void SetPhysicsLayer(int physicsLayer) => this.gameObject.layer = physicsLayer;
        public void SetPosition(Vector3 position) => this.transform.position = position;
        public void SetColor(Color color) => this.spriteRenderer.color = color;
        public void SetParent(Transform parent) => this.transform.SetParent(parent);
    }
}