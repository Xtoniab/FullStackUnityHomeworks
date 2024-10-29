using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        public event Action OnGameStart;
        public event Action OnGameFinish;

        private void Start()
        {
            OnGameStart?.Invoke();
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;

            OnGameFinish?.Invoke();
        }
    }
}