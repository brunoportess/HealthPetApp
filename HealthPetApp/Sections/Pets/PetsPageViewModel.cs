using HealthPetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HealthPetApp.Sections.Pets
{
    internal partial class PetsPageViewModel : BasePageViewModel
    {
        // Lista completa de todos os pets
        private ObservableCollection<Pet> AllPets { get; set; }

        // Lista que o CollectionView exibe (a ser filtrada)
        private ObservableCollection<Pet> _filteredPets;
        public ObservableCollection<Pet> FilteredPets
        {
            get => _filteredPets;
            set { _filteredPets = value; OnPropertyChanged(); }
        }

        // Carrossel de tipos de pets
        public ObservableCollection<PetType> PetTypes { get; set; }

        // Propriedade para a SearchBar
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterPets(); // Chama a filtragem ao digitar
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }


        // Propriedade para o item selecionado no carrossel
        private PetType _selectedPetType;
        public PetType SelectedPetType
        {
            get => _selectedPetType;
            set
            {
                if (_selectedPetType != value)
                {
                    // Limpa o estado 'IsSelected' do anterior
                    if (_selectedPetType != null)
                    {
                        _selectedPetType.IsSelected = false;
                    }

                    _selectedPetType = value;

                    // Define o estado 'IsSelected' do novo
                    if (_selectedPetType != null)
                    {
                        _selectedPetType.IsSelected = true;
                    }

                    OnPropertyChanged();
                    FilterPets(); // Chama a filtragem ao selecionar
                }
            }
        }

        public PetsPageViewModel()
        {
            //SearchText = "aaaaaaa";
            // 1. Carregar dados (Exemplo)
            LoadData();

            // 2. Inicializa a lista filtrada com todos os pets
            FilteredPets = AllPets;

            // 3. Seleciona o primeiro filtro como ativo ("Dogs" na imagem)
            SelectedPetType = PetTypes.FirstOrDefault();
        }

        private void LoadData()
        {
            // Dados de Exemplo
            AllPets = new ObservableCollection<Pet>
            {
                new Pet { Name = "Chihuahua", Type = "Dog", Origin = "Mexico", Age = 3, Distance = 3.6, ImageUrl = "dogpuppy.png", Gender = "Male", CardBackgroundColor = "#F5B7B1" },
                new Pet { Name = "Pug", Type = "Dog", Origin = "China", Age = 2, Distance = 4.5, ImageUrl = "catpuppy.png", Gender = "Male", CardBackgroundColor = "#F7E0C0" },
                // Adicione mais pets
            };

            PetTypes = new ObservableCollection<PetType>
            {
                new() { Name = "Dogs", Icon = "headdog.png", FilterValue = "Dog" },
                new() { Name = "Cats", Icon = "headcat.png", FilterValue = "Cat" },
                new() { Name = "Parrots", Icon = "headparrot.png", FilterValue = "Parrot" },
                new() { Name = "Bunnies", Icon = "headbunnie.png", FilterValue = "Bunny" }
                // Adicione outros tipos de pets
            };
        }

        // Lógica de Filtragem Centralizada
        private void FilterPets()
        {
            var petsQuery = AllPets.AsEnumerable();

            // Filtro por Tipo (Carrossel)
            if (SelectedPetType != null)
            {
                petsQuery = petsQuery.Where(p => p.Type == SelectedPetType.FilterValue);
            }

            // Filtro por Nome (SearchBar)
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                petsQuery = petsQuery.Where(p => p.Name.ToLower().Contains(SearchText.ToLower()));
            }

            // Atualiza a lista exibida na CollectionView
            FilteredPets = new ObservableCollection<Pet>(petsQuery);
        }
    }
}
