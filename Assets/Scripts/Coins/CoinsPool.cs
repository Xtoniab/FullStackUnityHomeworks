using Modules;
using Zenject;

namespace Coins
{
    public class CoinPool : MonoMemoryPool<Coin>
    {
        public void Despawn(ICoin coin)
        {
            base.Despawn(coin as Coin);
        }
    }
}