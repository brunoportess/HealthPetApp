using CommunityToolkit.Mvvm.Input;
using HealthPetApp.Models;
using HealthPetApp.Sections.Pets;
using HealthPetApp.Sections.Pets.Detail;
using HealthPetApp.Services.Navigation;
using System.Diagnostics;
using System.Windows.Input;

namespace HealthPetApp.Sections.Home
{
    internal partial class HomePageViewModel : BasePageViewModel
    {
        public ICommand NavigatePetsCommand => new Command(async () =>
        {
            await NavigationHelper.Navigate<PetsPageViewModel>();
        });

        public ICommand NavigatePetDetailCommand => new Command(async () =>
        {
            await NavigationHelper.Navigate<PetDetailPageViewModel>();
        });


    }
}
