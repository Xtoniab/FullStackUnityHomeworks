using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver: MonoBehaviour
    {
        [SerializeField] private Ship character;
        [SerializeField] private GameManager gameManager;

        private void OnEnable()
        {
            this.character.OnDeath += this.gameManager.FinishGame;
        }
        
        private void OnDisable()
        {
            this.character.OnDeath -= this.gameManager.FinishGame;
        }
    }
}