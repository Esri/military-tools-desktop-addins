REM Must be run from Git shell/command prompt
REM Must be run from folder: military-tools-desktop-addins\source\Utilities

cd ../../..
pwd

REM Note: add "--single-branch" if you only want the single dev branch
git clone -b dev https://github.com/Esri/coordinate-conversion-addin-dotnet
git clone -b dev https://github.com/Esri/distance-direction-addin-dotnet.git
git clone -b dev https://github.com/Esri/military-symbol-editor-addin-wpf
git clone -b dev https://github.com/Esri/visibility-addin-dotnet
