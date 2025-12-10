using HealthPetApp.Sections.Template;
using HealthPetApp.Services.Navigation;

namespace HealthPetApp.Services.Navigation
{
    internal static class NavigationHelper
    {
        // ============================================================
        // NAVEGAÇÃO INTERNA (ContentView)
        // ============================================================

        public static async Task Navigate<TViewModel>(object? parameter = null)
            where TViewModel : BasePageViewModel
        {
            await NavigationService.Current.Navigate<TViewModel>(parameter);
        }

        // ============================================================
        // VOLTAR (Modal → Soft Stack → Nada)
        // ============================================================

        public static async Task GoBack()
        {
            await NavigationService.Current.GoBack();
        }

        public static bool CanGoBack =>
            RootPage.Instance?.CanSoftGoBack ?? false;

        // ============================================================
        // MODAL
        // ============================================================

        public static async Task NavigateModal<TViewModel>(object? parameter = null)
            where TViewModel : BaseModalPageViewModel
        {
            await NavigationService.Current.Navigate<TViewModel>(parameter);
        }

        // ============================================================
        // POPUPS
        // ============================================================

        public static async Task ShowPopup<TViewModel>(object? parameter = null, bool animate = true)
            where TViewModel : BasePageViewModel
        {
            await NavigationService.Current.NavigatePopupAsync<TViewModel>(parameter, animate);
        }
    }
}
