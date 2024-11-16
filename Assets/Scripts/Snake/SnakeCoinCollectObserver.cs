using System;
using ModestTree;
using Modules;
using UnityEngine;
using Zenject;

namespace Coins
{
    public class SnakeCoinCollectObserver : IInitializable, IDisposable
    {
        private readonly ISnake snake;
        private readonly IScore score;
        private readonly IDifficulty difficulty;
        private readonly IGameCycle gameCycle;
        private readonly ICoinSpawner coinSpawner;

        public SnakeCoinCollectObserver(
            ISnake snake,
            IScore score,
            IGameCycle gameCycle,
            ICoinSpawner coinSpawner,
            IDifficulty difficulty)
        {
            this.snake = snake;
            this.score = score;
            this.gameCycle = gameCycle;
            this.coinSpawner = coinSpawner;
            this.difficulty = difficulty;
        }

        private void OnMoved(Vector2Int newPos)
        {
            if (!coinSpawner.HasCoinWithPosition(newPos, out var coin))
            {
                return;
            }
            
            coinSpawner.DespawnCoin(coin);
            
            score.Add(coin.Score);
            snake.Expand(coin.Bones);

            if (coinSpawner.ActiveCoins.IsEmpty() && !difficulty.Next(out _))
            {
                gameCycle.GameOver(win: true);
            }
        }
        
        public void Initialize() 
            => snake.OnMoved += OnMoved;

        public void Dispose() 
            => snake.OnMoved -= OnMoved;
    }
}