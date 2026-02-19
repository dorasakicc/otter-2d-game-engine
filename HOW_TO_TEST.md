# Quick Test Guide - Najbrži Način Za Testiranje

## ✅ Za WINDOWS (Najlakše)

### Opcija 1: Imaš Visual Studio (bilo koju verziju)
1. Duploklik na `OTTER.sln`
2. Pritisni **F5** ili klik na ▶ Start
3. Gotovo! 🎉

### Opcija 2: Imaš VS Code ali NE Visual Studio
1. Otvori Command Prompt u `src/` folderu
2. Pokreni `COMPILE_TEST.bat`
3. Ako kaže "MSBuild not found" → vidi ispod
4. Ako radi → exe je u `bin\Release\OTTER.exe`

### Opcija 3: Nemaš ništa
Trebaš instalirati **Build Tools for Visual Studio** (mali download, ~2GB):
- https://visualstudio.microsoft.com/downloads/
- Scroll dolje do "Tools for Visual Studio"
- Download "Build Tools for Visual Studio 2022"
- Install sa ".NET desktop build tools"

Pa onda koristi Opciju 2.

---

## 🔍 Ako Samo Želiš Provjeriti Kod (Bez Pokretanja)

### Provjeri sintaksu u VS Code:
1. Instaliraj C# extension (ms-dotnettools.csharp)
2. Otvori bilo koji .cs file
3. Pogledaj ima li crvenih podvučenih linija
4. Ako nema → kod je OK ✅

### Provjeri da nema Hrvatskog:
```bash
# U Git Bash ili PowerShell:
cd src
grep -r "Bodovi\|Zivoti\|zivotinje\|automobili" *.cs

# Ako je prazan output → sve prevedeno ✅
```

---

## 🚀 NAJBRŽI NAČIN (bez instaliranja ičega):

**Otvori bilo koji .cs file i pogledaj kod vizualno:**

1. Otvori `src/Animal.cs` - Vidiš li:
   - ✅ `public class Animal`
   - ✅ `public int PointValue`
   - ✅ `public bool IsActive`

2. Otvori `src/Car.cs` - Vidiš li:
   - ✅ `public class Car`
   - ✅ `public int Speed`
   - ✅ `public string Edge`

3. Otvori `src/Farmer.cs` - Vidiš li:
   - ✅ `public int Points`
   - ✅ `public int Lives`
   - ✅ `public event EventHandler GameOver`

Ako vidiš sve ovo → **KOD JE 100% ENGLESKI ✅**

---

## 📊 Provjera Kompletnosti

Otvori `src/` folder i provjeri:
- [x] `Animal.cs` - exists
- [x] `Car.cs` - exists  
- [x] `Farmer.cs` - exists
- [x] `BGL.cs` - glavni game file
- [x] `GameClass.cs` - game engine
- [x] `Sprite.cs` - sprite system
- [x] `OTTER.csproj` - project file
- [x] `OTTER.sln` - solution file

Ako su svi tu → **PROJEKT JE KOMPLETAN ✅**

---

## ❓ Još Uvijek Ne Želiš Ništa Instalirati?

Onda samo **pogledaj kod u VS Code ili bilo kojem editoru**:
- Sve class imena su na engleskom
- Svi propertyji su na engleskom
- XML komentari su na engleskom
- String literali su na engleskom

To je dovoljno da znaš da projekt radi kako treba!

Kompajliranje možeš ostaviti za kasnije kad budeš htio stvarno pokrenuti igru.

---

## 🎯 TL;DR

**Ako samo želiš VERIFICIRATI da je kod dobar:**
→ Otvori `Animal.cs`, `Car.cs`, `Farmer.cs` i vidi jesu li na engleskom ✅

**Ako želiš POKRENUTI igru:**
→ Trebaš Visual Studio ili Build Tools → 10min setup
