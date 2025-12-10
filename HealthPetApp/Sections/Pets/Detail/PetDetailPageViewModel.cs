using System;
using System.Collections.Generic;
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
    }
}
