using AptiJobs.Sections.Login;
using HealthPetApp.Sections.Home;
using HealthPetApp.Sections.Pets;
using HealthPetApp.Sections.Pets.Detail;
using HealthPetApp.Sections.Template;
using HealthPetApp.Sections.Vaccine;
using Mopups.Pages;
using Mopups.Services;

namespace HealthPetApp.Services.Navigation
{
    internal class NavigationService
    {
        static readonly Lazy<NavigationService> _Lazy = new(() => new NavigationService());
        public static NavigationService Current => _Lazy.Value;

        readonly Dictionary<Type, Type> _Mappings;

        NavigationService()
        {
            _Mappings = new Dictionary<Type, Type>();
            CreateViewModelMappings();
        }

        INavigation Navigation =>
            ((CustomNavigationPage)Application.Current.Windows[0].Page!).Navigation;

        void CreateViewModelMappings()
        {
            // Home
            _Mappings.Add(typeof(HomePageViewModel), typeof(HomePage));
            _Mappings.Add(typeof(PetsPageViewModel), typeof(PetsPage));
            _Mappings.Add(typeof(VaccinePageViewModel), typeof(VaccinePage));
            _Mappings.Add(typeof(PetDetailPageViewModel), typeof(PetDetailPage));

            // Login
            _Mappings.Add(typeof(LoginPageViewModel), typeof(LoginPage));
        }

        // ============================================================
        // SOFT NAVIGATION (DENTRO DO ROOTPAGE)
        // ============================================================

        public async Task Navigate<TViewModel>(object? parameter = null)
            where TViewModel : BasePageViewModel
        {
            await InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        async Task InternalNavigateToAsync(Type viewModelType, object? parameter = null)
        {
            var viewObj = CreateAndBindPage(viewModelType);

            if (viewObj is Page modalPage)
            {
                var vm = (BasePageViewModel)modalPage.BindingContext;

                // CHAMA INITIALIZE AQUI
                await vm.Initialize(parameter);

                await Navigation.PushModalAsync(modalPage, true);
                return;
            }

            bool isMainPage =
                viewModelType == typeof(HomePageViewModel) ||
                viewModelType == typeof(PetsPageViewModel);

            if (viewObj is ContentView view)
            {
                var vm = (BasePageViewModel)view.BindingContext;

                if (isMainPage)
                {
                    RootPage.Instance.SetHeaderMode(HeaderMode.Main);
                    RootPage.Instance.ClearSoftStack();

                    //if (RootPage.Instance.BodyContent is ContentView oldView && oldView.BindingContext is ISoftNavigable oldVm)
                    //{
                    //    oldVm.OnNavigatedFrom();
                    //}

                    RootPage.Instance.BodyContent = view;

                    //if (view.BindingContext is ISoftNavigable newVm)
                    //{
                    //    newVm.OnNavigatedTo();
                    //}

                    // CHAMA INITIALIZE PARA PÁGINAS PRINCIPAIS
                    await vm.Initialize(parameter);
                }
                else
                {
                    RootPage.Instance.SetHeaderMode(HeaderMode.Secondary);

                    if (RootPage.Instance.BodyContent != null)
                        RootPage.Instance.PushSoftStack(RootPage.Instance.BodyContent);

                    RootPage.Instance.BodyContent = view;

                    // CHAMA INITIALIZE PARA PÁGINAS INTERNAS
                    await vm.Initialize(parameter);
                }
            }
        }


        async Task InternalNavigateToAsync2(Type viewModelType, object? parameter = null)
        {
            var viewObj = CreateAndBindPage(viewModelType);

            if (viewObj is Page modalPage)
            {
                await Navigation.PushModalAsync(modalPage, true);
                //await ((BasePageViewModel)modalPage.BindingContext).Initialize(parameter);
                return;
            }

            bool isMainPage =
                viewModelType == typeof(HomePageViewModel) ||
                viewModelType == typeof(PetsPageViewModel)
                // caso tenha mais páginas principais do footer, inclua aqui
                ;

            if (viewObj is ContentView view)
            {
                if (isMainPage)
                {
                    // Header principal
                    RootPage.Instance.SetHeaderMode(HeaderMode.Main);

                    // Zera soft stack
                    RootPage.Instance.ClearSoftStack();

                    // Troca o conteúdo
                    RootPage.Instance.BodyContent = view;
                    //await ((BasePageViewModel)view.BindingContext).Initialize(parameter);
                }
                else
                {
                    // Header secundário
                    RootPage.Instance.SetHeaderMode(HeaderMode.Secondary);

                    // Guarda tela anterior na soft stack
                    if (RootPage.Instance.BodyContent != null)
                        RootPage.Instance.PushSoftStack(RootPage.Instance.BodyContent);

                    // Troca conteúdo
                    RootPage.Instance.BodyContent = view;
                }
            }
        }


        // ============================================================
        // POPUP
        // ============================================================

        public async Task NavigatePopupAsync<TViewModel>(object? parameter = null, bool animate = false)
            where TViewModel : BasePageViewModel
        {
            var popup = CreateAndBindPage(typeof(TViewModel));

            if (popup is PopupPage p)
            {
                await ((BasePageViewModel)p.BindingContext).Initialize(parameter);
                await MopupService.Instance.PushAsync(p, animate);
                return;
            }

            throw new ArgumentException($"O tipo {typeof(TViewModel)} não é PopupPage");
        }

        // ============================================================
        // REAL PAGE BACK (MODAL OU PUSH)
        // ============================================================

        public async Task GoBack(bool toRoot = false, bool animated = true)
        {
            // Se estiver em modal → fecha
            //if (Navigation.ModalStack.Count > 0)
            //{
            //    await Navigation.PopModalAsync(animated);
            //    return;
            //}

            //// Se estiver em Soft Stack → volta tela
            //if (RootPage.Instance.CanSoftGoBack)
            //{
            //    RootPage.Instance.SoftGoBack();
            //    return;
            //}
            await BaseViewModel.Loading();
            try
            {
                if (toRoot)
                {
                    await Navigation.PopToRootAsync(animated);
                    return;
                }

                if (Navigation.ModalStack.Count > 0)
                {
                    await Navigation.PopModalAsync(animated);
                    return;
                }

                await Navigation.PopAsync(animated);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await BaseViewModel.RemoveAllPopupLoad();
            }
        }

        public async Task SoftGoBack()
        {
            // Remove a tela atual da soft stack
            RootPage.Instance.SoftGoBack();

            // Agora decidimos qual header mostrar
            if (RootPage.Instance.IsAtRoot)
            {
                RootPage.Instance.SetHeaderMode(HeaderMode.Main);
            }
            else
            {
                RootPage.Instance.SetHeaderMode(HeaderMode.Secondary);
            }

            await Task.CompletedTask;
        }

        // ============================================================
        // FACTORY
        // ============================================================

        Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_Mappings.ContainsKey(viewModelType))
                throw new KeyNotFoundException($"No map for {viewModelType} was found");

            return _Mappings[viewModelType];
        }

        object CreateAndBindPage(Type viewModelType)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            // MODAL
            if (viewModelType.BaseType == typeof(BaseModalPageViewModel))
            {
                var modal = Activator.CreateInstance(pageType) as Page;
                modal!.BindingContext = Activator.CreateInstance(viewModelType) as BasePageViewModel;
                return modal;
            }

            // CONTENTVIEW (navegação interna)
            var view = Activator.CreateInstance(pageType) as ContentView;
            view!.BindingContext = Activator.CreateInstance(viewModelType) as BasePageViewModel;
            return view;
        }
    }
}
