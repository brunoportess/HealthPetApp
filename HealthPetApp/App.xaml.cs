using AptiJobs.Sections.Login;
using HealthPetApp.Sections.Home;
using HealthPetApp.Sections.Template;
using HealthPetApp.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace HealthPetApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //return new Window(new CustomNavigationPage(new HomePage() { BindingContext = new HomePageViewModel() }));
            var root = new RootPage();

            // injeta conteúdo inicial (Home)
            root.BodyContent = new HomePage()
            {
                BindingContext = new HomePageViewModel()
            };

            return new Window(new CustomNavigationPage(root)
            {
                BarBackgroundColor = Colors.Transparent,
                BarTextColor = Colors.Transparent
            });
        }
    }
}