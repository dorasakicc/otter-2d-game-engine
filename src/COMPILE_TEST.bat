@echo off
echo ========================================
echo OTTER Game - Quick Compile Test
echo ========================================
echo.

REM Try to find MSBuild
set MSBUILD=""
if exist "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
)
if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD="C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
)

if %MSBUILD%=="" (
    echo ❌ MSBuild not found!
    echo.
    echo You need ONE of these:
    echo   1. Visual Studio 2019/2022 installed
    echo   2. Build Tools for Visual Studio
    echo   3. .NET Framework SDK
    echo.
    echo Download from: https://visualstudio.microsoft.com/downloads/
    echo   - Visual Studio Community (free)
    echo   - Or just "Build Tools for Visual Studio"
    pause
    exit /b 1
)

echo Found MSBuild: %MSBUILD%
echo.
echo Compiling project...
%MSBUILD% OTTER.sln /p:Configuration=Release /nologo /verbosity:minimal

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo ✅ BUILD SUCCESSFUL!
    echo ========================================
    echo.
    echo Game compiled to: bin\Release\OTTER.exe
    echo.
    echo Run the game:
    echo   cd bin\Release
    echo   OTTER.exe
    echo.
) else (
    echo.
    echo ========================================
    echo ❌ BUILD FAILED!
    echo ========================================
    echo.
    echo Check the errors above.
)

pause
