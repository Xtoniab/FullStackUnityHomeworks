using System;
using Modules;
using Zenject;

public class DifficultyController: IInitializable, IDisposable
{
    private readonly ICoinSpawner coinSpawner;
    private readonly IDifficulty difficulty;
    private readonly IGameCycle gameCycle;

    public DifficultyController(ICoinSpawner coinSpawner, IDifficulty difficulty, IGameCycle gameCycle)
    {
        this.coinSpawner = coinSpawner;
        this.difficulty = difficulty;
        this.gameCycle = gameCycle;
    }

    private void DifficultyUp()
    {
        if (!difficulty.Next(out _))
        {
            gameCycle.GameOver(win: true);
        }
    }

    public void Initialize()
    {
        gameCycle.OnGameStart += DifficultyUp;
        coinSpawner.OnEmpty += DifficultyUp;
    }

    public void Dispose()
    {
        gameCycle.OnGameStart -= DifficultyUp;
        coinSpawner.OnEmpty -= DifficultyUp;
    }
}