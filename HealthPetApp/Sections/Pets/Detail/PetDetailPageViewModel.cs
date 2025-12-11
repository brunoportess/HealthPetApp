using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HealthPetApp.Sections.Pets.Detail
{
    internal partial class PetDetailPageViewModel : BasePageViewModel
    {
        public string PageTitle { get; set; }

        public PetDetailPageViewModel()
        {
            PageTitle = "Detalhes do Pet";
        }

        public override async Task Initialize(object args = null)
        {
            var pet = args as Models.Pet;
            Debug.WriteLine("PetDetailPageViewModel initialized with args: " + args?.ToString());


            await base.Initialize(args);
        }
    }
}
