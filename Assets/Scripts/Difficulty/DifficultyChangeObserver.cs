using System;
using Modules;
using Zenject;

public class DifficultyChangeObserver : IInitializable, IDisposable
{
    private readonly ISnake snake;
    private readonly IDifficulty difficulty;
    private readonly ICoinSpawner coinSpawner;

    public DifficultyChangeObserver(ISnake snake, IDifficulty difficulty, ICoinSpawner coinSpawner)
    {
        this.snake = snake;
        this.difficulty = difficulty;
        this.coinSpawner = coinSpawner;
    }

    private void OnDifficultyChanged()
    {
        snake.SetSpeed(difficulty.Current);
        coinSpawner.SpawnCoins(difficulty.Current);
    }

    public void Initialize() 
        => difficulty.OnStateChanged += OnDifficultyChanged;
    
    public void Dispose() 
        => difficulty.OnStateChanged -= OnDifficultyChanged;
}