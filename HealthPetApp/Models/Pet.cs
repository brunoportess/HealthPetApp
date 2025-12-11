using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPetApp.Models
{
    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; } 
        public string Origin { get; set; }
        public int Age { get; set; }
        public double Distance { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
        public string CardBackgroundColor { get; set; }

        // Propriedades calculadas
        public string Details => $"{Age} anos, {Type.ToLower()}";
        public string GenderIcon => Gender == "Male" ? "♂" : "♀";
    }

    public class PetType
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string FilterValue { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
