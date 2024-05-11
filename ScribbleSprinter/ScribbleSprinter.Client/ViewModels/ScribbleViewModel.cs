
namespace ScribbleSprinter.Client.ViewModels
{
    public class ScribbleViewModel : ViewModelBase
    {
        public string Text
        {
            get => text; set
            {
                if (value.Length > text.Length)
                {
                    typingTimer.Stop();
                    if (Value < TimeLimit)
                    {
                        typingTimer.Start();
                    }
                }

                
                text = value;
            }
        }
        public int Value { get; set; }
        public int TimeLimit = 60;
        private string text = string.Empty;

        System.Timers.Timer sprintTimer { get; } = new();
        System.Timers.Timer typingTimer { get; } = new();
        public bool GameOver { get; set; }
        public override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                typingTimer.Interval = 1000;
                typingTimer.Elapsed += TypingTimer_Elapsed;

                sprintTimer.AutoReset = true;
                sprintTimer.Interval = 1000;
                sprintTimer.Elapsed += (s, e) => InvokeAsync(Countdown);
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        private void TypingTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (Value < TimeLimit)
            {
                GameOver = true;
            }
        }

        public Task Countdown()
        {
            Value++;
            StateHasChanged();
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            sprintTimer?.Dispose();
            typingTimer?.Dispose();
            base.Dispose();
        }
    }
}
