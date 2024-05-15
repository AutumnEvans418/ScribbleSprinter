using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace ScribbleSprinter.Client.ViewModels
{
    public class SprintViewModel : ViewModelBase
    {
        public int Value { get; set; }
        public int TimeLimit { get; set; } = 5;
        public int TypingSpeed { get; set; } = 1000;

        private string text = "";

        System.Timers.Timer sprintTimer { get; } = new();
        System.Timers.Timer typingTimer { get; } = new();

        public EventCallback<string> OnSprintCompleted { get; set; }
        public Action? OnGameOver { get; set; }
        public string Text
        {
            get => text;
            set
            {
                if (value.Length > text.Length)
                {
                    typingTimer.Stop();
                    if (Value < TimeLimit)
                    {
                        typingTimer.Start();
                    }

                    if (!sprintTimer.Enabled)
                    {
                        sprintTimer.Start();
                    }
                }

                text = value;
            }
        }

        public override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                typingTimer.Interval = TypingSpeed;
                typingTimer.Elapsed += (s,e) => InvokeAsync(TypingTimer_Elapsed);

                sprintTimer.AutoReset = true;
                sprintTimer.Interval = 1000;
                sprintTimer.Elapsed += (s, e) => InvokeAsync(Countdown);
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        public async Task Countdown()
        {
            Value++;
            if (Value >= TimeLimit)
            {
                typingTimer.Stop();
                sprintTimer.Stop();
               
                await OnSprintCompleted.InvokeAsync(Text);
            }
            StateHasChanged();
        }

        private Task TypingTimer_Elapsed()
        {
            if (Value < TimeLimit)
            {
                typingTimer.Stop();
                sprintTimer.Stop();
                OnGameOver?.Invoke();
                StateHasChanged();
            }
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            typingTimer?.Dispose();
            sprintTimer?.Dispose();
            base.Dispose();
        }
    }
}
