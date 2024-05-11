namespace ScribbleSprinter.Client.ViewModels
{
    public class ViewModelBase : IDisposable
    {
        public Action StateHasChanged { get; set; } = null!;
        public Func<Func<Task>, Task> InvokeAsync { get; set; } = null!;

        public virtual void Dispose()
        {
        }

        public virtual Task OnAfterRenderAsync(bool firstRender)
        {
            return Task.CompletedTask;
        }
    }
}
