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

    private Page? GetCurrentPage()
    {
        // For single-window applications, use Windows[0].Page
        return Application.Current?.Windows.FirstOrDefault()?.Page;
    }

    public Task DisplayAlert(Exception ex)
    {
        var page = GetCurrentPage();
        if (page == null)
            return Task.CompletedTask;

        if (ex.Message.ToLower().Contains("no such host"))
            return page.DisplayAlertAsync("Ops..", "Parece que você não está conectado à internet, verifique sua conexão e/ou tente novamente mais tarde!", "Ok");
        else if (ex.Message.ToLower().Contains("sequence"))
            return page.DisplayAlertAsync("Ops..", "Ocorreu um erro ao processar a sua operação, verifique sua conexão com a internet e/ou tente novamente mais tarde, se o problema persistir, entre em contato com nossa central de atendimento.", "Ok");
        else
        {
            //DiagnosticsService.Current.TrackError(ex);

            return ex switch
            {
                InvalidOperationException invalidOperationException => page.DisplayAlertAsync("Ops..", invalidOperationException.Message, "Ok"),
                ArgumentException argumentException => page.DisplayAlertAsync("Atenção!", argumentException.Message, "Ok"),
                TaskCanceledException taskCanceledException => page.DisplayAlertAsync("Operação cancelada", "Operação cancelada pelo usuário", "Ok"),
                _ => page.DisplayAlertAsync("Atenção!", ex.Message, "Ok"),
            };
        }
    }

    public Task<bool> DisplayAlert(string title, string message, string okMessage, string cancelMessage)
    {
        var page = GetCurrentPage();
        if (page == null)
            return Task.FromResult(false);

        return page.DisplayAlertAsync(title, message, okMessage, cancelMessage);
    }

    public Task DisplayAlert(string title, string message, string okMessage = "OK")
    {
        var page = GetCurrentPage();
        if (page == null)
            return Task.CompletedTask;

        return page.DisplayAlertAsync(title, message, okMessage);
    }
}