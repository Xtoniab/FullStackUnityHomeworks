using Pool;
using ShootEmUp.Brain;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    // Не стал уже делать мега абстракцию IShipInput, с какими-то универсальными контроллерами
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Ship ship;
        
        [SerializeField]
        private EnemyBrain brain;

        private void FixedUpdate()
        {
            this.ship.Move(this.brain.GetMoveDirection());
            this.brain.FixedTick();
        }

        private void OnEnable()
        {
            this.brain.OnFire += this.ship.FireTarget;
        }

        private void OnDisable()
        {
            this.brain.OnFire -= this.ship.FireTarget;
        }
    }
}