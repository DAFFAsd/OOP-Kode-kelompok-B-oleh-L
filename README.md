# OOP-Kode-kelompok-B-oleh-L

A C# console-based RPG game project developed as part of the Object-Oriented Programming course. Battle against vegetable enemies in this unique turn-based RPG!

## Project Team Members

- Andi Muhammad Alvin Farhansyah (2306161933)
- Alexander Christhian (2306267025)
- Daffa Sayra Firdaus (2306267151)
- Fathan Yazid Satriani (2306250560)

## Project Structure

- `Core/` - Core game mechanics and state management
  - Combat system
  - Game state management
  - Save/Load functionality
  - Buff/Debuff system
  - Quest tracking
- `Models/` - Game object models and data structures
  - Character classes
  - Inventory system
  - Item implementations
  - Map areas
- `UI/` - User interface components
  - Battle menu
  - Main menu
  - Item management
- `Enums/` - Game enumerations and constants
  - Difficulty levels
  - Map types
  - Weapon types

## Game Features

### Combat System
- Turn-based battles with strategic elements
- Multiple enemy encounters
- Guard system for damage reduction
- Status effects (buffs/debuffs)
- Special abilities:
  - Critical Strike (Level 3)
  - Area Attack (Level 8)
  - Healing Aura (Level 5)

### Character Development
- Experience-based leveling system
- Stat progression
- Weapon upgrades:
  - Fists (Level 1)
  - Fry Gun (Level 3)
  - Soda Sprayer (Level 5)
  - Pizza Slicer (Level 7)
  - Sugar Rush Rifle (Level 10)
  - Holy Tabasco Sauce (Special Quest Reward)

### Areas & Progression
1. Leafy Lagoon (Starting Area)
   - Boss: Captain Cabbage
2. Veggie Valley
   - Boss: General Greenhorn
3. Fruit Field
   - Boss: Orchard Overlord
4. Mushroom Meadow
   - Boss: Fungal Fiend
5. The Salad Bar (Final Area)
   - Boss: Chef Supreme

### Item System
- Inventory management
- Various consumables:
  - Health Potions
  - Strength Potions
  - Weakening Potions
- Random item drops after battles

### Quest System
- Special missions from the Wise Duck
- Unique rewards
- Mini-boss encounters

### Save System
- Automatic save after significant events
- Manual save option
- State persistence between sessions

### Difficulty Settings
- Easy: Reduced enemy stats
- Normal: Balanced experience
- Hard: Enhanced enemy stats and AI
  
### Explanation Video Link
https://youtu.be/PJomwpzk1dg

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or Visual Studio Code

### Building and Running

1. Clone the repository
2. Open the solution in Visual Studio or VS Code
3. Build the project:
```sh
dotnet build

