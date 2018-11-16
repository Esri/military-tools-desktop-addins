REM Must be run from Git shell/command prompt
REM Must be run from folder: military-tools-desktop-addins\source\Utilities

cd ../../..
pwd

REM Note: add "--single-branch" if you only want the single dev branch
git clone -b dev https://github.com/Esri/coordinate-conversion-addin-dotnet
git clone -b dev https://github.com/Esri/distance-direction-addin-dotnet.git
git clone -b dev https://github.com/Esri/military-symbology.git
git clone -b dev https://github.com/Esri/military-tools-geoprocessing-toolbox
git clone -b dev https://github.com/Esri/visibility-addin-dotnet
git clone -b dev https://github.com/Esri/military-tools-geoprocessing-toolbox.git
