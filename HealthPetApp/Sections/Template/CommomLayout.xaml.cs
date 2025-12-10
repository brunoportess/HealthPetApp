using HealthPetApp.Models;

namespace HealthPetApp.Sections.Template;

public partial class CommomLayout : ContentView
{
	public CommomLayout()
	{
		InitializeComponent();
        //header.IconSize = "30";
    }

    public View ContentLayout
    {
        get => ContentContainerLayout.Content;
        set => ContentContainerLayout.Content = value;
    }

    #region [ Title Header ]

    //public static readonly BindableProperty TitleProperty =
    //   BindableProperty.Create(
    //       propertyName: nameof(Title),
    //       returnType: typeof(string),
    //       declaringType: typeof(CommomLayout),
    //       defaultValue: string.Empty,
    //       defaultBindingMode: BindingMode.OneWay,
    //       propertyChanged: TitlePropertyChanged);

    //private static void TitlePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
    //{
    //    var control = (CommomLayout)bindable;
    //    control.txtTitleLabel.Text = newValue.ToString();
    //    //header.Title = newValue.ToString();
    //}

    //public string Title
    //{
    //    get => (string)GetValue(TitleProperty);
    //    set => SetValue(TitleProperty, value);
    //}

    #endregion

    #region [ Icon FontAwesome Header ]

    //public static readonly BindableProperty IconProperty =
    //   BindableProperty.Create(
    //       propertyName: nameof(Icon),
    //       returnType: typeof(string),
    //       declaringType: typeof(CommomLayout),
    //       defaultValue: string.Empty,
    //       defaultBindingMode: BindingMode.OneWay,
    //       propertyChanged: IconPropertyChanged);

    //private static void IconPropertyChanged(BindableObject bindable, object oldvalue, object newValue)
    //{
    //    var control = (CommomLayout)bindable;
    //    control.txtFontIcon.Text = newValue.ToString();
    //    //header.Icon = newValue.ToString();
    //}

    //public string Icon
    //{
    //    get => (string)GetValue(TitleProperty);
    //    set => SetValue(TitleProperty, value);
    //}

    #endregion

    #region [ Size Icon FontAwesome Header ]

    //public static readonly BindableProperty IconSizeProperty =
    //   BindableProperty.Create(
    //       propertyName: nameof(IconSize),
    //       returnType: typeof(string),
    //       declaringType: typeof(CommomLayout),
    //       defaultValue: string.Empty,
    //       defaultBindingMode: BindingMode.OneWay,
    //       propertyChanged: IconSizePropertyChanged);

    //private static void IconSizePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
    //{
    //    var control = (CommomLayout)bindable;
    //    var teste = Convert.ToDouble(newValue);
    //    control.txtFontIcon.FontSize = teste;
    //    //header.Icon = newValue.ToString();
    //}

    //public string IconSize
    //{
    //    get => (string)GetValue(TitleProperty);
    //    set => SetValue(TitleProperty, value);
    //}

    #endregion



    #region [ Selected Tab Footer ]

    public static readonly BindableProperty SelectedTabProperty =
        BindableProperty.Create(
            nameof(SelectedTab),
            typeof(BottomTab),
            typeof(CommomLayout),
            BottomTab.Home,
            propertyChanged: SelectedTabChanged);

    // Update the SelectedTabChanged method to use the instance field and simplify the null check (IDE0031)
    private static void SelectedTabChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CommomLayout)bindable;
        if (control.MenuFooterPageControl is not null)
            control.MenuFooterPageControl.SelectedTab = (BottomTab)newValue;
    }

    public BottomTab SelectedTab
    {
        get => (BottomTab)GetValue(SelectedTabProperty);
        set => SetValue(SelectedTabProperty, value);
    }

    #endregion
}