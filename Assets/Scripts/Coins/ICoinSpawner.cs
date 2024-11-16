using System.Collections.Generic;
using Modules;
using UnityEngine;

public interface ICoinSpawner
{
    IReadOnlyList<ICoin> ActiveCoins { get; }
    void SpawnCoins(int count);
    void DespawnCoin(ICoin coin);
    bool HasCoinWithPosition(Vector2Int position, out ICoin coin);
}