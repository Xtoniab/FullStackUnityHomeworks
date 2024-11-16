using System;
using Modules;
using SnakeGame;
using Zenject;

namespace UI
{
    public class GameUIPresenter: IInitializable, IDisposable
    {
        private readonly IDifficulty difficulty;
        private readonly IScore score;
        private readonly IGameCycle gameCycle;
        private readonly IGameUI gameUI;

        public GameUIPresenter(IGameUI gameUI, IDifficulty difficulty, IScore score, IGameCycle gameCycle)
        {
            this.difficulty = difficulty;
            this.score = score;
            this.gameCycle = gameCycle;
            this.gameUI = gameUI;
        }

        public void Initialize()
        {
            difficulty.OnStateChanged += SetDifficulty;
            score.OnStateChanged += SetScore;
            gameCycle.OnGameOver += gameUI.GameOver;
            
            SetDifficulty();
            SetScore(score.Current);
        }

        private void SetScore(int newScore)
            => gameUI.SetScore(newScore.ToString());

        private void SetDifficulty()
            => gameUI.SetDifficulty(difficulty.Current, difficulty.Max);

        public void Dispose()
        {
            difficulty.OnStateChanged -= SetDifficulty;
            score.OnStateChanged -= SetScore;
            gameCycle.OnGameOver -= gameUI.GameOver;
        }
    }
}