using Zenject;

namespace Game.Scripts.UI.Money
{
    public class MoneyUIInstaller: Installer<IMoneyView, MoneyUIInstaller>
    {
        [Inject]
        private readonly IMoneyView moneyView;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IMoneyView>()
                .FromInstance(moneyView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<MoneyPresenter>()
                .AsCached()
                .NonLazy();
        }
        
    }
}