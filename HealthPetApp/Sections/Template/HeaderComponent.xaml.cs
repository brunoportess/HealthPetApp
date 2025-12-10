namespace HealthPetApp.Sections.Template;

public partial class HeaderComponent : ContentView
{
	public HeaderComponent()
	{
		InitializeComponent();
	}

    // Nome do usuário
    public static readonly BindableProperty UserNameProperty =
        BindableProperty.Create(nameof(UserName), typeof(string), typeof(HeaderComponent), default(string),
            propertyChanged: (b, o, n) =>
            {
                ((HeaderComponent)b).UserNameLabel.Text = (string)n;
            });

    public string UserName
    {
        get => (string)GetValue(UserNameProperty);
        set => SetValue(UserNameProperty, value);
    }

    // Localização
    public static readonly BindableProperty LocationProperty =
        BindableProperty.Create(nameof(Location), typeof(string), typeof(HeaderComponent), default(string),
            propertyChanged: (b, o, n) =>
            {
                ((HeaderComponent)b).LocationLabel.Text = (string)n;
            });

    public string Location
    {
        get => (string)GetValue(LocationProperty);
        set => SetValue(LocationProperty, value);
    }

    // Avatar
    public static readonly BindableProperty AvatarProperty =
        BindableProperty.Create(nameof(Avatar), typeof(ImageSource), typeof(HeaderComponent), default(ImageSource),
            propertyChanged: (b, o, n) =>
            {
                ((HeaderComponent)b).AvatarImage.Source = (ImageSource)n;
            });

    public ImageSource Avatar
    {
        get => (ImageSource)GetValue(AvatarProperty);
        set => SetValue(AvatarProperty, value);
    }
}