# HealthPetApp — Project README

## Overview

HealthPetApp is a **.NET MAUI** application (**C# 14 / .NET 10**) that follows an **MVVM** pattern with lightweight navigation and UI templates.  
The app shows **lists of pets**, **detail pages** and uses soft navigation inside a root layout.

---

## Quick start

1. Open the solution in **Visual Studio 2026**.
2. Restore **NuGet** packages.
3. Set the startup project to the `HealthPetApp` project.
4. Select a device/emulator and run:
   - `Debug > Start Debugging (F5)`  
   - or `Debug > Start Without Debugging (Ctrl+F5)`.

---

## Project structure (important files & folders)

### App.xaml

- Application resources and merged dictionaries.
- Registers the converter `SelectedToColorConverter` used across the UI.

### Models/

- `Pet.cs`  
  - Pet model and PetType model.  
  - Contains calculated properties (`Details`, `GenderIcon`).
- `BottomTab.cs`  
  - Enum for footer tabs used by the root layout.

### Sections/

#### Template/

- `CommomLayout.xaml(.cs)`  
  - Common root layout used to host the header, body content and footer.  
  - Exposes `SelectedTab` bindable property to control footer state.
- `HeaderComponent.xaml`  
  - Header UI (user info, location, icons).

#### Home/

- `HomePage.xaml`  
  - Main home content (body content for the app).

#### Pets/

- `PetsPage.xaml`  
  Pet list UI. Uses:
  - A `SearchBar` bound to `SearchText`
  - A horizontal `CollectionView` with `PetType` items (carousel filter)
  - A vertical `CollectionView` bound to `FilteredPets`

- `PetsPageViewModel.cs`  
  View model for `PetsPage`. Responsibilities:
  - Holds `AllPets`, `FilteredPets`, `PetTypes`
  - Implements centralized filtering in `FilterPets()` (by selected `PetType` and `SearchText`)
  - Initializes sample data in `LoadData()`

- `Detail/PetDetailPage.xaml`  
  - Pet details UI (scrollable page with image and metadata).

### Services/

- `Navigation/NavigationService.cs`  
  Navigation factory and logic:
  - Maps view models to views
  - Supports internal “soft” navigation (ContentView swapping inside `RootPage`)
  - Supports modal/popup navigation
  - Creates view/viewmodel instances with `Activator.CreateInstance` and sets `BindingContext`
  - Integrates Mopups (`MopupService`) for popup pages

- `DialogService.cs`  
  Centralized alert/dialog helpers:
  - Uses the current `Page` to show alerts
  - Maps common exceptions to user-friendly messages.

### Extensions/Converters/

- `SelectedToColorConverter.cs`  
  Converts `IsSelected` boolean to highlight/neutral `Color` used by the pet-type carousel.

### Resources/

- `Resources/Styles`
  - `Colors.xaml` — app style tokens (colors)
  - `Styles.xaml` — app styles
- `Resources/Raw`
  - Raw assets (see `AboutAssets.txt`)
  - Images referenced from XAML (e.g. `dogpuppy.png`, `headdog.png`) should be placed in platform-appropriate resources or in the MAUI `Resources` folder.

---

## Architecture notes & conventions

- **MVVM**
  - UI pages and content views are bound to view models.
  - Many pages are implemented as `ContentView` to be hosted inside `RootPage` for soft navigation.

- **Root layout**
  - `RootPage` (referenced by `NavigationService`) manages:
    - `HeaderMode` (`HeaderMode.Main` / `HeaderMode.Secondary`)
    - Soft navigation stack
    - Footer selected tab
  - Use `RootPage.Instance.BodyContent` to replace the displayed `ContentView`.

- **Navigation**
  - Use `NavigationService.Current.Navigate<TViewModel>(parameter)` to navigate to a page/viewmodel.
  - Use `NavigatePopupAsync<TViewModel>` for popups.

- **Dialogs**
  - Use `DialogService.Current.DisplayAlert(...)` to show consistent alerts and map errors.

- **Converters and resources**
  - Converters are registered in `App.xaml` (e.g. `SelectedToColorConverter`) and referenced by `StaticResource`.

- **Data & UI binding**
  - `PetsPageViewModel` shows an example for filtering — add sample data in `LoadData()` or hook to real data sources.

---

## Common modification points

- **Add a new page**
  - Create XAML + viewmodel.
  - Add mapping in `NavigationService.CreateViewModelMappings()` to map the viewmodel type to its view type.

- **Add new pet images/icons**
  - Place images in `Resources/Images` (or platform-specific resources).
  - Reference the filename in `Image.Source`.

- **Change colors/styles**
  - Edit `Resources/Styles/Colors.xaml`.
  - Edit `Resources/Styles/Styles.xaml`.

- **Add new pet types or sample data**
  - Edit `PetsPageViewModel.LoadData()` (modify `AllPets` and `PetTypes`).

---

## Tips

- View/viewmodel creation uses `Activator.CreateInstance` — constructors should be parameterless or the factory needs extension.
- The app uses Mopups for popup pages — ensure the Mopups package and initialization are correctly configured.
- When updating UI templates, prefer the `ContentView` approach to keep `RootPage` soft navigation behavior intact.
