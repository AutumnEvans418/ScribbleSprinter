
using MudBlazor.Interfaces;
using MudBlazor.Utilities;
using ScribbleSprinter.Client.Pages;
using System.Text.RegularExpressions;

namespace ScribbleSprinter.Client.ViewModels
{

    public class ScribbleViewModel : ViewModelBase
    {
        public List<string> SavedTexts { get; } = [];

        public States GameState { get; private set; }

        public void SetState(States state)
        {
            GameState = state;
            StateHasChanged();
        }
    }
}
