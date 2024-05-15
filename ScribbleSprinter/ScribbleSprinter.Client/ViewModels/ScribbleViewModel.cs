
using Blazored.LocalStorage;
using MudBlazor.Interfaces;
using MudBlazor.Utilities;
using ScribbleSprinter.Client.Pages;
using System.Text.RegularExpressions;

namespace ScribbleSprinter.Client.ViewModels
{

    public class ScribbleViewModel : ViewModelBase
    {
        private readonly ILocalStorageService localStorageService;

        public string SavedTexts { get; set; } = string.Empty;

        public States GameState { get; private set; }
        public ScribbleViewModel(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }
        public async Task SprintCompleted(string sprintText)
        {
            SavedTexts += sprintText;

            try
            {
                await localStorageService.SetItemAsStringAsync("Scribble", SavedTexts);
            }
            catch (Exception)
            {
            }

            SetState(States.SprintCompleted);
        }

        public async Task Clear()
        {
            SavedTexts = string.Empty;
            try
            {
                await localStorageService.SetItemAsStringAsync("Scribble", SavedTexts);
            }
            catch (Exception)
            {
            }
            StateHasChanged();
        }

        public override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    SavedTexts = await localStorageService.GetItemAsStringAsync("Scribble") ?? string.Empty;
                    StateHasChanged();
                }
                catch (Exception)
                {
                }
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        public void SetState(States state)
        {
            GameState = state;
            StateHasChanged();
        }
    }
}
