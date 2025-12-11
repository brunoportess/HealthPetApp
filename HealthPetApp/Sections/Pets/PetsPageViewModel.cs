using CommunityToolkit.Mvvm.Input;
using HealthPetApp.Models;
using HealthPetApp.Sections.Pets.Detail;
using HealthPetApp.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace HealthPetApp.Sections.Pets
{
    internal partial class PetsPageViewModel : BasePageViewModel, ISoftNavigable
    {
        // Lista completa de todos os pets
        private ObservableCollection<Pet> AllPets { get; set; }

        private ObservableCollection<Pet> _filteredPets = [];
        public ObservableCollection<Pet> FilteredPets
        {
            get => _filteredPets;
            set { _filteredPets = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PetType> _petTypes = [];
        public ObservableCollection<PetType> PetTypes
        {
            get => _petTypes;
            set
            {
                _petTypes = value;
                OnPropertyChanged();
            }
        }

        // Propriedade para a SearchBar
        private CancellationTokenSource _searchCts;
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();

                _searchCts?.Cancel();
                _searchCts = new CancellationTokenSource();

                _ = Task.Delay(200, _searchCts.Token)
                        .ContinueWith(t =>
                        {
                            if (!t.IsCanceled)
                                MainThread.BeginInvokeOnMainThread(FilterPets);
                        });
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

        private PetType? _selectedPetType = null;
        public PetType? SelectedPetType
        {
            get => _selectedPetType;
            set
            {
                if (_selectedPetType == value) return;

                _selectedPetType?.IsSelected = false;
                value?.IsSelected = true;

                _selectedPetType = value;

                OnPropertyChanged(nameof(SelectedPetType));

                FilterPets();
            }
        }


        private Pet? _selectedPet = null;
        public Pet? SelectedPet
        {
            get => _selectedPet;
            set
            {
                _selectedPet = value;
                OnPropertyChanged();
            }
        }

        public PetsPageViewModel()
        {
            //LoadData();
            //FilteredPets = AllPets;
        }

        public override async Task Initialize(object args = null)
        {
            if (AllPets == null)
            {
                LoadData();
                FilterPets();
            }
        }


        public IAsyncRelayCommand PetDetailNavigateCommand => new AsyncRelayCommand(PetDetailNavigate);
        private async Task PetDetailNavigate()
        {
            if (SelectedPet == null) return;
            await NavigationHelper.Navigate<PetDetailPageViewModel>(SelectedPet);
        }

        private void LoadData()
        {
            // Dados de Exemplo
            AllPets =
            [
                new() { Name = "Bubble", Type = "Dog", Origin = "Mexico", Age = 3, Distance = 3.6, ImageUrl = "dogpuppy.png", Gender = "Male", CardBackgroundColor = "#F5B7B1" },
                new() { Name = "Minie", Type = "Cat", Origin = "China", Age = 2, Distance = 4.5, ImageUrl = "catpuppy.png", Gender = "Female", CardBackgroundColor = "#F7E0C0" },
            ];

            PetTypes =
            [
                new() { Name = "Dogs", Icon = "headdog.png", FilterValue = "Dog" },
                new() { Name = "Cats", Icon = "headcat.png", FilterValue = "Cat" },
                new() { Name = "Parrots", Icon = "headparrot.png", FilterValue = "Parrot" },
                new() { Name = "Bunnies", Icon = "headbunnie.png", FilterValue = "Bunny" }
            ];
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
            FilteredPets.Clear();
            foreach (var item in petsQuery)
                FilteredPets.Add(item);
        }

        public void OnNavigatedTo()
        {
            SelectedPet = null;
            SelectedPetType = null;
        }

        public void OnNavigatedFrom()
        {
            Debug.WriteLine("throw new NotImplementedException()");
        }
    }
}
