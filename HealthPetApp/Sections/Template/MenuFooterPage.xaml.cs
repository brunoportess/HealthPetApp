using HealthPetApp.Models;
using HealthPetApp.Sections.Home;
using HealthPetApp.Sections.Pets;
using HealthPetApp.Services.Navigation;

namespace HealthPetApp.Sections.Template;

public partial class MenuFooterPage : ContentView
{
    public static readonly BindableProperty SelectedTabProperty =
        BindableProperty.Create(
            nameof(SelectedTab),
            typeof(BottomTab),
            typeof(MenuFooterPage),
            BottomTab.Home,
            propertyChanged: OnSelectedTabChanged);

    //public static readonly BindableProperty SelectedTabProperty =
    //    BindableProperty.Create(
    //        nameof(SelectedTab),
    //        typeof(BottomTab),
    //        typeof(MenuFooterPage),
    //        BottomTab.Home,
    //        propertyChanged: OnSelectedTabChanged);

    public BottomTab SelectedTab
    {
        get => (BottomTab)GetValue(SelectedTabProperty);
        set => SetValue(SelectedTabProperty, value);
    }

    private static void OnSelectedTabChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MenuFooterPage)bindable;
        control.UpdateTabColors();
    }

    public MenuFooterPage()
    {
        InitializeComponent();
        UpdateTabColors();
    }

    private void UpdateTabColors()
    {
        var activeColor = Color.FromArgb("#188544");
        var inactiveColor = Color.FromArgb("#909090");

        // Home
        ((FontImageSource)HomeIcon.Source).Color =
            SelectedTab == BottomTab.Home ? activeColor : inactiveColor;

        // Pets
        ((FontImageSource)PetIcon.Source).Color =
            SelectedTab == BottomTab.Students ? activeColor : inactiveColor;

        // Search
        ((FontImageSource)SearchIcon.Source).Color =
            SelectedTab == BottomTab.Formulas ? activeColor : inactiveColor;

        // Profile
        ((FontImageSource)ProfileIcon.Source).Color =
            SelectedTab == BottomTab.Config ? activeColor : inactiveColor;

        // Textos
        HomeText.TextColor = ((FontImageSource)HomeIcon.Source).Color;
        PetText.TextColor = ((FontImageSource)PetIcon.Source).Color;
        SearchText.TextColor = ((FontImageSource)SearchIcon.Source).Color;
        ProfileText.TextColor = ((FontImageSource)ProfileIcon.Source).Color;
    }

    // Navegação
    private async void GoToHomePage(object sender, TappedEventArgs e)
    {
        SelectedTab = BottomTab.Home;
        //await NavigationService.Current.Navigate<HomePageViewModel>();
        //RootPage.Instance.BodyContent = new HomePage()
        //{
        //    BindingContext = new HomePageViewModel()
        //};
        await NavigationService.Current.Navigate<HomePageViewModel>();

        //UpdateTabColors();
    }

    private async void GoToStudentsPage(object sender, TappedEventArgs e)
    {
        SelectedTab = BottomTab.Students;
        //await NavigationService.Current.Navigate<PetsPageViewModel>();
        //RootPage.Instance.BodyContent = new PetsPage()
        //{
        //    BindingContext = new PetsPageViewModel()
        //};
        await NavigationService.Current.Navigate<PetsPageViewModel>();

        //UpdateTabColors();
    }

    private async void GoToFormulasPage(object sender, TappedEventArgs e)
    {
        SelectedTab = BottomTab.Formulas;
        // Navegar depois
    }

    private async void GoToConfigPage(object sender, TappedEventArgs e)
    {
        SelectedTab = BottomTab.Config;
        // Navegar depois
    }
}
