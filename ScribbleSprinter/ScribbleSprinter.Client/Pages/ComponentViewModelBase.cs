using Microsoft.AspNetCore.Components;
using ScribbleSprinter.Client.ViewModels;

namespace ScribbleSprinter.Client.Pages
{
    public class ComponentViewModelBase<TVm> : ComponentBase, IDisposable where TVm : ViewModelBase
    {
        [Inject]
        public TVm Vm { get; set; } = null!;
        protected override Task OnInitializedAsync()
        {
            Vm.StateHasChanged = StateHasChanged;
            Vm.InvokeAsync = InvokeAsync;
            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Vm.OnAfterRenderAsync(firstRender);
            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            Vm.Dispose();
        }
    }
}
