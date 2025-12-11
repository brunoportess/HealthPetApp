using HealthPetApp.Services.Navigation;

namespace HealthPetApp.Sections.Template;

public partial class RootPage : ContentPage
{
    public static RootPage Instance { get; private set; }

    private readonly Stack<View> _softStack = new();
    public bool IsAtRoot => _softStack.Count == 0;



    public RootPage()
    {
        InitializeComponent();
        Instance = this;

        NavigationPage.SetHasNavigationBar(this, false);
    }

    public View BodyContent
    {
        get => ContentContainerLayout.Content;
        set => ContentContainerLayout.Content = value;
    }

    public void SetHeaderMode(HeaderMode mode)
    {
        HeaderMain.IsVisible = mode == HeaderMode.Main;
        HeaderSecondary.IsVisible = mode == HeaderMode.Secondary;
    }

    public void SetFooterVisible(bool visible)
    {
        Footer.IsVisible = visible;
    }

    // ============================================================
    // SOFT STACK
    // ============================================================

    public void PushSoftStack(View view)
    {
        _softStack.Push(view);
    }

    //public void SoftGoBack()
    //{
    //    if (_softStack.Count == 0)
    //        return;

    //    var previous = _softStack.Pop();
    //    BodyContent = previous;
    //}

    public async void SoftGoBack()
    {
        if (_softStack.Count == 0)
            return;

        var current = BodyContent;
        var previous = _softStack.Pop();

        // altera o conteúdo
        BodyContent = previous;

        // força o MAUI a redesenhar imediatamente
        //await Task.Yield();
        //this.ForceLayout();

        // lifecycle depois
        if (current is View currentView &&
            currentView.BindingContext is ISoftNavigable currentVm)
            currentVm.OnNavigatedFrom();

        if (previous is View previousView &&
            previousView.BindingContext is ISoftNavigable previousVm)
            previousVm.OnNavigatedTo();
    }



    public void ClearSoftStack()
    {
        _softStack.Clear();
    }


    public bool CanSoftGoBack => _softStack.Count > 0;

    protected override bool OnBackButtonPressed()
    {
        if (CanSoftGoBack)
        {
            SoftGoBack();
            return true;
        }

        return base.OnBackButtonPressed();
    }
}