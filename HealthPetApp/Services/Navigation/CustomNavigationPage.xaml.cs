namespace HealthPetApp.Services.Navigation;

public partial class CustomNavigationPage : NavigationPage
{
    public CustomNavigationPage() : base()
    {
        InitializeComponent();
    }

    public CustomNavigationPage(Page root) : base(root)
    {
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasNavigationBar(root, false);
        InitializeComponent();
    }
}