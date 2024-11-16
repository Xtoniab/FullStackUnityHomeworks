using System;
using Modules;
using Zenject;

public class GameStartObserver: IInitializable, IDisposable
{
    private readonly IGameCycle gameCycle;
    private readonly IDifficulty difficulty;

    public GameStartObserver(IGameCycle gameCycle, IDifficulty difficulty)
    {
        this.gameCycle = gameCycle;
        this.difficulty = difficulty;
    }

    private void OnGameStart()
    {
        if (!difficulty.Next(out _))
        {
            gameCycle.GameOver(true);
        }
    }

    public void Initialize() 
        => gameCycle.OnGameStart += OnGameStart;

    public void Dispose() 
        => gameCycle.OnGameStart -= OnGameStart;
}