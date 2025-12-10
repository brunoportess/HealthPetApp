using HealthPetApp.Services.Navigation;

namespace HealthPetApp.Sections.Template;

public partial class HeaderBackComponent : ContentView
{
	public HeaderBackComponent()
	{
		InitializeComponent();
	}

    // Bindable Property para Título
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(HeaderBackComponent),
            string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    private async void OnBackTapped(object? sender, EventArgs e)
    {
        // usando seu NavigationService
        await NavigationService.Current.SoftGoBack();
    }
}