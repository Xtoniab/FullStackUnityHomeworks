using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float spawnDelay = 1f;
        [SerializeField] private EnemyCreator enemyCreator;
        [SerializeField] private GameManager gameManager;
        
        private Coroutine enemySpawnLoop;

        private void OnEnable()
        {
            gameManager.OnGameStart += StartSpawnLoop;
            gameManager.OnGameFinish += StopSpawnLoop;
        }
        
        private void OnDisable()
        {
            gameManager.OnGameStart -= StartSpawnLoop;
            gameManager.OnGameFinish -= StopSpawnLoop;
        }

        private void StartSpawnLoop()
        {
            StopSpawnLoop();
            enemySpawnLoop = StartCoroutine(EnemySpawnLoop());
        }

        private void StopSpawnLoop()
        {
            if (enemySpawnLoop != null)
            {
                StopCoroutine(enemySpawnLoop);
            }
        }
        
        private IEnumerator EnemySpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
                this.enemyCreator.TryCreate(out _);
            }
        }
    }
}