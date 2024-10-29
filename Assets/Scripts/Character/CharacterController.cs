using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private Ship ship;
        [SerializeField] private PlayerInput playerInput;
        
        private void OnEnable()
        {
            this.playerInput.OnFire += ship.FireForward;
        }

        private void FixedUpdate()
        {
            this.ship.Move(this.playerInput.MoveDirection);
        }

        private void OnDisable()
        {
            this.playerInput.OnFire -= ship.FireForward;
        }
    }
}