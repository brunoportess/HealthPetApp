using HealthPetApp.Models;
using Mopups.Pages;
using Mopups.Services;

namespace HealthPetApp.Sections.Popups.UIComponents;

public partial class AlertPopupPage : PopupPage
{
    public AlertPopupPage(string tituloMensagem, string corpoMensagem, ImagensAlerta tipoImagem)
    {
        InitializeComponent();
        labelTituloMensagem.Text = tituloMensagem;
        labelCorpoMensagem.Text = corpoMensagem;

        if (tipoImagem == ImagensAlerta.Warning)
        {
            corFundoImagem.BackgroundColor = Color.FromArgb("#fad271");
            imagemPopup.TextColor = Color.FromArgb("#f29c1f");
            corFundoImagem.Stroke = Color.FromArgb("#f29c1f");
            imagemPopup.Text = "\uf071";
        }
        else if (tipoImagem == ImagensAlerta.Error)
        {
            corFundoImagem.BackgroundColor = Color.FromArgb("#ff674f");
            imagemPopup.TextColor = Color.FromArgb("#ad0e0e");
            corFundoImagem.Stroke = Color.FromArgb("#ad0e0e");
            imagemPopup.TranslationY = 5;
            imagemPopup.Text = "\u0058";
        }
        else if (tipoImagem == ImagensAlerta.Sucess)
        {
            corFundoImagem.BackgroundColor = Color.FromArgb("96c970");
            imagemPopup.TextColor = Color.FromArgb("#5eac24");
            corFundoImagem.Stroke = Color.FromArgb("#5eac24");
            imagemPopup.Text = "\uf00c";
        }

    }

    private async void FecharPopupClicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}