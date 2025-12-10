using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPetApp.Models
{
    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; } // Ex: "Dog", "Cat", "Parrot", "Bunny"
        public string Origin { get; set; }
        public int Age { get; set; }
        public double Distance { get; set; }
        public string ImageUrl { get; set; } // URL ou nome do arquivo de imagem
        public string Gender { get; set; } // Ex: "Male", "Female"
        public string CardBackgroundColor { get; set; } // Cor de fundo do cartão (Ex: "#F5B7B1" para o Chihuahua)

        // Propriedades calculadas
        public string Details => $"{Age} anos, {Type.ToLower()}";
        public string GenderIcon => Gender == "Male" ? "♂" : "♀";
    }

    public class PetType
    {
        public string Name { get; set; } // Ex: "Dogs"
        public string Icon { get; set; } // Nome do arquivo de ícone
        public string FilterValue { get; set; } // Ex: "Dog"
        public bool IsSelected { get; set; } = false;
    }
}
