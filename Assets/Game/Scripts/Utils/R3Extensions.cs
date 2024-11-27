using System;
using R3;

namespace Game.Scripts.Utils
{
    public static class R3Extensions
    {
        public static IDisposable SubscribeAndCall<T>(this ReadOnlyReactiveProperty<T> source, Action<T> onNext)
        {
            onNext(source.CurrentValue);
            return source.Subscribe(onNext);
        }
    }
}