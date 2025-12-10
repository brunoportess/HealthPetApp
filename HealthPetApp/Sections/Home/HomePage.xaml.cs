using HealthPetApp.Sections.Pets;
using HealthPetApp.Services.Navigation;

namespace HealthPetApp.Sections.Home;

public partial class HomePage : ContentView
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
        await NavigationService.Current.Navigate<PetsPageViewModel>();
    }
}