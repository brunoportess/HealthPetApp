using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPetApp.Services.Navigation
{
    public interface ISoftNavigable
    {
        void OnNavigatedTo();
        void OnNavigatedFrom();
    }
}
