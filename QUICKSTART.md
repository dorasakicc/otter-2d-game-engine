# Quick Start Guide

## Installation

### Option 1: Visual Studio (Recommended)
1. Clone this repository
   ```bash
   git clone https://github.com/yourusername/otter-game.git
   cd otter-game
   ```

2. Open `src/OTTER.sln` in Visual Studio

3. Press F5 to build and run

### Option 2: Command Line
```bash
# Navigate to src folder
cd src

# Build (requires MSBuild)
msbuild OTTER.sln /p:Configuration=Release

# Run
cd bin/Release
OTTER.exe
```

## How to Play

### Controls
- **Arrow Keys** or **WASD** - Move the farmer
- **ESC** - (Implement pause/quit)

### Objective
1. Collect farm animals (cats, chickens, cows, pigs)
2. Avoid getting hit by cars
3. Reach 800 points to win

### Scoring
- Each animal collected: **+100 points**
- Hit by car: **-1 life** (3 lives total)
- Game over when lives = 0

## Troubleshooting

### "Could not load file or assembly..."
- Make sure you have .NET Framework 4.8 installed
- Try cleaning and rebuilding: `Build > Clean Solution`, then `Build > Build Solution`

### Sprites not showing
- Ensure assets folder is in the same directory as the executable
- Check that `Copy to Output Directory` is set to `Copy if newer` for asset files

### Game runs too fast/slow
- This is a known limitation of the current implementation
- See CONTRIBUTING.md for improvement suggestions

## Game Assets

All sprites are included in `assets/sprites/`:
- `farmerr.png` - Player character
- `cat.png`, `chicken.png`, `cow.png`, `pig.png` - Collectible animals
- `auto*.png` - Car obstacles

Background:
- `cestaa.jpg` - Road background

## Next Steps

After getting the game running:
1. Read `README.md` for technical details
2. Check `CONTRIBUTING.md` for improvement ideas
3. Experiment with changing values (speed, points, etc.)
4. Try adding new features!

## Common First Modifications

**Make game easier:**
```csharp
// In farmer.cs, line 42
Zivoti = 5;  // Start with 5 lives instead of 3
```

**Change win condition:**
```csharp
// In farmer.cs, line 21
if (bodovi == 500)  // Win at 500 points instead of 800
```

**Adjust game speed:**
```csharp
// In BGL.cs, find the timer interval
timer1.Interval = 50;  // Smaller = faster game
```

Have fun and happy coding! 🎮
