using System;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class SnakeSelfCollideObserver: IInitializable, IDisposable
    {
        private readonly ISnake snake;
        private readonly IGameCycle gameCycle;

        public SnakeSelfCollideObserver(ISnake snake, IGameCycle gameCycle)
        {
            this.snake = snake;
            this.gameCycle = gameCycle;
        }

        private void OnSelfCollided()
        {
            gameCycle.GameOver(false);
        }

        public void Initialize() 
            => snake.OnSelfCollided += OnSelfCollided;

        public void Dispose() 
            => snake.OnSelfCollided -= OnSelfCollided;
    }
}