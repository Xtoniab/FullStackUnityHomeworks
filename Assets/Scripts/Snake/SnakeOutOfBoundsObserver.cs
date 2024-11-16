using System;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

public class SnakeOutOfBoundsObserver: IInitializable, IDisposable
{
    private readonly ISnake snake;
    private readonly IWorldBounds worldBounds;
    private readonly IGameCycle gameCycle;

    public SnakeOutOfBoundsObserver(ISnake snake, IWorldBounds worldBounds, IGameCycle gameCycle)
    {
        this.snake = snake;
        this.worldBounds = worldBounds;
        this.gameCycle = gameCycle;
    }

    private void OnMoved(Vector2Int newPos)
    {
        if (!worldBounds.IsInBounds(newPos))
        {
            gameCycle.GameOver(false);
        }
    }

    public void Initialize() 
        => snake.OnMoved += OnMoved;

    public void Dispose() 
        => snake.OnMoved -= OnMoved;
}