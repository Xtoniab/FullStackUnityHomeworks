using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    [Inject] 
    public IGameCycle gameCycle;

    private void Start()
    {
        gameCycle.StartGame();
    }
}