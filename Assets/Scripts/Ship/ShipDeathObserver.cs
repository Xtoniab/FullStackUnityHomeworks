using System;
using UnityEngine;

namespace ShootEmUp
{
    // Ну тут чуть заигрался скорее всего
    // Не хотел везде писать OnEnable и OnDisable
    public class ShipDeathObserver: MonoBehaviour
    {
        [SerializeField] private Ship ship;
        
        private Action<Ship> onDeathAction; 
        
        public void Construct(Action<Ship> action)
        {
            this.onDeathAction = action;
        }
        
        public void OnEnable()
        {
            this.ship.OnDeath += this.OnDeath;
        }
        
        public void OnDisable()
        {
            this.ship.OnDeath -= this.OnDeath;
        }
        
        public void Reset()
        {
            this.onDeathAction = null;
        }

        private void OnDeath()
        {
            this.onDeathAction?.Invoke(this.ship);
        }
    }
}