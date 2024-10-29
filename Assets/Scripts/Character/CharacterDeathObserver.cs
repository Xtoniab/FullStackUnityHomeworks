using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver: MonoBehaviour
    {
        [SerializeField] private ShipDeathObserver deathObserver;
        [SerializeField] private GameManager gameManager;

        private void Awake()
        {
            deathObserver.Construct(_ => gameManager.FinishGame());
        }
    }
}