using Modules;
using System.Collections.Generic;
using System.Linq;
using Coins;
using SnakeGame;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;

public class CoinSpawner : ICoinSpawner
{
    public event Action OnEmpty;
    
    private readonly CoinPool coinPool;
    private readonly IWorldBounds worldBounds;

    private readonly List<ICoin> activeCoins = new();
    public IReadOnlyList<ICoin> ActiveCoins => activeCoins;
    
    public CoinSpawner(CoinPool coinPool, IWorldBounds worldBounds)
    {
        this.coinPool = coinPool;
        this.worldBounds = worldBounds;
    }

    public void SpawnCoins(int count)
    {
        for (var i = 0; i < count; i++)
        {
            Vector2Int position;
            do
            {
                position = worldBounds.GetRandomPosition();
            } while (IsPositionOccupied(position));

            var coin = coinPool.Spawn(position);
            activeCoins.Add(coin);
        }
    }

    public void DespawnCoin(ICoin coin)
    {
        activeCoins.Remove(coin);
        coinPool.Despawn(coin);
        
        if (activeCoins.Count == 0)
        {
            OnEmpty?.Invoke();
        }
    }
    
    public bool HasCoinWithPosition(Vector2Int position, out ICoin coin)
    {
        coin = activeCoins.FirstOrDefault(c => c.Position == position);
        return coin != null;
    }

    private bool IsPositionOccupied(Vector2Int position) 
        => activeCoins.Any(coin => coin.Position == position);
}