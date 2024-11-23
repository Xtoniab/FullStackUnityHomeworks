using System.Collections.Generic;
using Modules;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;

public interface ICoinSpawner
{
    event Action OnEmpty;
    IReadOnlyList<ICoin> ActiveCoins { get; }
    void SpawnCoins(int count);
    void DespawnCoin(ICoin coin);
    bool HasCoinWithPosition(Vector2Int position, out ICoin coin);
}