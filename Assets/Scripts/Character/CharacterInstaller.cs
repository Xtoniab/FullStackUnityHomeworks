using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class CharacterInstaller: MonoBehaviour
    {
        [SerializeField] private Ship ship;
        [SerializeField] private BulletSystem bulletSystem;
        
        private void Awake()
        {
            ship.Construct(bulletSystem);
        }
    }
}