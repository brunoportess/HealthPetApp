using CommunityToolkit.Mvvm.Input;
using HealthPetApp;
using HealthPetApp.Sections.Home;

namespace AptiJobs.Sections.Login
{
    internal partial class LoginPageViewModel : BasePageViewModel
    {
        public LoginPageViewModel()
        {
            
        }

        [RelayCommand]
        public async Task Login()
        {
            await Navigation.Navigate<HomePageViewModel>();
        }
        
    }
}
