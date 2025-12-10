using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthPetApp.Services;

sealed class DialogService
{
    private static readonly Lazy<DialogService> _Lazy = new Lazy<DialogService>(() => new DialogService());

    public static DialogService Current => _Lazy.Value;

    private DialogService() { }

    public Task DisplayAlert(Exception ex)
    {
        if (ex.Message.ToLower().Contains("no such host"))
            return Application.Current!.MainPage!.DisplayAlert("Ops..", "Parece que você não está conectado à internet, verifique sua conexão e/ou tente novamente mais tarde!", "Ok");
        else if (ex.Message.ToLower().Contains("sequence"))
            return Application.Current!.MainPage!.DisplayAlert("Ops..", "Ocorreu um erro ao processar a sua operação, verifique sua conexão com a internet e/ou tente novamente mais tarde, se o problema persistir, entre em contato com nossa central de atendimento.", "Ok");
        else
        {
            //DiagnosticsService.Current.TrackError(ex);

            return ex switch
            {
                InvalidOperationException invalidOperationException => Application.Current!.MainPage!.DisplayAlert("Ops..", invalidOperationException.Message, "Ok"),
                ArgumentException argumentException => App.Current!.MainPage!.DisplayAlert("Atenção!", argumentException.Message, "Ok"),
                TaskCanceledException taskCanceledException => App.Current!.MainPage!.DisplayAlert("Operação cancelada", "Operação cancelada pelo usuário", "Ok"),
                _ => App.Current!.MainPage!.DisplayAlert("Atenção!", ex.Message, "Ok"),
            };

        }
    }

    public Task<bool> DisplayAlert(string title, string message, string okMessage, string cancelMessage)
    {
        return App.Current!.MainPage!.DisplayAlert(title, message, okMessage, cancelMessage);
    }

    public Task DisplayAlert(string title, string message, string okMessage = "OK")
    {
        return App.Current!.MainPage!.DisplayAlert(title, message, okMessage);
    }
}