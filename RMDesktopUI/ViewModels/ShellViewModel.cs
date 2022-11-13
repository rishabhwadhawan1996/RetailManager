using System.Threading;
using System.Threading.Tasks;

using Caliburn.Micro;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel:Conductor<object>,IHandle<LogOnEventModel>
    {
        private readonly SimpleContainer container;
        private readonly SalesViewModel salesVm;
        private readonly IEventAggregator eventAgregator;

        public ShellViewModel(IEventAggregator events,SalesViewModel salesViewModel,SimpleContainer simpleContainer)
        {
            container = simpleContainer;
            salesVm = salesViewModel;
            eventAgregator = events;
            eventAgregator.Subscribe(this);
            ActivateItemAsync(container.GetInstance<LoginViewModel>()).Wait();
        }

        public async Task HandleAsync(LogOnEventModel message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(salesVm);           
        }
    }
}
