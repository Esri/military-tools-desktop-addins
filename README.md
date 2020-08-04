# military-tools-desktop-addins
___Beginning at ArcGIS Pro 2.6 this functionality is installed with ArcGIS Pro. This repository will only be updated with critical fixes to ArcGIS Desktop.___


Military Tools for ArcGIS is a collection of mission-focused enhancements to simplify defense and intelligence workflows in ArcGIS. The Military Tools for ArcGIS Desktop add-ins repository combines several related mission-focused ArcGIS add-ins for defense and intelligence (see [Resources](#resources) section) as a single installable add-in toolbar for ArcMap and ArcGIS Pro.

![screenshot of toolbar](militarytools.png)

## Features

* ArcMap toolbar and ArcGIS Pro tab for the set of Military Tools for ArcGIS
	* [Coordinate Conversion](https://github.com/Esri/coordinate-conversion-addin-dotnet)
	* [Distance and Direction](https://github.com/Esri/distance-direction-addin-dotnet)
	* [Military Symbol Editor](https://github.com/Esri/military-symbology) (ArcGIS Pro Only)
	* [Visibility](https://github.com/Esri/visibility-addin-dotnet)
* Addin for ArcMap 10.4.1+
* Addin for ArcGIS Pro 2.2+ 

## Sections

* [Requirements](#requirements)
* [Instructions](#instructions)
* [Resources](#resources)
* [Issues](#issues)
* [Contributing](#contributing)
* [Licensing](#licensing)

## Requirements

### Build Requirements 

* Visual Studio 2015
    * Important Note: Visual Studio 2013 [may also be required if building on ArcGIS 10.4.1-10.5.1](https://support.esri.com/en/technical-article/000012659)
    * If you wish to load and view the Python project file `military-tools-geoprocessing-toolbox.pyproj` you should install the Python Tools for Visual Studio (PTVS) Extension when prompted. This project or extension is not required to build the addins.
* ArcGIS for Desktop 
	* ArcMap 10.4.1+
	* ArcGIS Pro 2.2+
* ArcGIS Desktop SDK for .NET 10.4.1+
	* [ArcGIS Desktop for .NET Requirements](https://desktop.arcgis.com/en/desktop/latest/get-started/system-requirements/arcobjects-sdk-system-requirements.htm)
* [ArcGIS Pro SDK](http://pro.arcgis.com/en/pro-app/sdk/) 2.2+

### Run Requirements

* ArcGIS for Desktop 
	* ArcMap 10.4.1+
	* ArcGIS Pro 2.2+

## Instructions

### Build Instructions

#### Obtain the Dependent Repositories

* Clone this repository
* Clone each of the following component repositories *to the same folder location* 
	* [coordinate-conversion-addin-dotnet](https://github.com/Esri/coordinate-conversion-addin-dotnet)
	* [distance-direction-addin-dotnet](https://github.com/Esri/distance-direction-addin-dotnet)
	* [military-symbology](https://github.com/Esri/military-symbology)
	* [military-tools-geoprocessing-toolbox](https://github.com/Esri/military-tools-geoprocessing-toolbox)
	* [visibility-addin-dotnet](https://github.com/Esri/visibility-addin-dotnet)

* Your local folder structure should now look like:

```
{Github Clone Location}
+---military-tools-desktop-addins
+---coordinate-conversion-addin-dotnet
+---distance-direction-addin-dotnet
+---military-symbology
+---military-tools-geoprocessing-toolbox
+---visibility-addin-dotnet
```

**IMPORTANT NOTE: Because of file name length limitations in Visual Studio, the length of the folder name of {Github Clone Location} should not exceed 80 characters**

* A clone [script has been provided](./source/Utilities/CloneDependentRepos.bat) to automate the cloning of these repos. To use:
	* Open a Git shell/command prompt 
	* `cd military-tools-desktop-addins\source\Utilities`
	* `CloneDependentRepos.bat`

#### Building

* To Build Using Visual Studio
	* Open and build solution files:
	* `{Github Clone Location}\military-tools-desktop-addins\source\MilitaryToolsForArcGISPro\MilitaryToolsForArcGISPro.sln`
	* `{Github Clone Location}\military-tools-desktop-addins\source\MilitaryToolsForArcMap\MilitaryToolsForArcMap.sln`
* To use MSBuild to build the solution
	* Open a Visual Studio Command Prompt: Start Menu | Visual Studio | Visual Studio Tools | Developer Command Prompt
	* ArcGIS Pro Add-in
		* ``` cd military-tools-desktop-addins\source\MilitaryToolsForArcGISPro ```
		* ``` msbuild MilitaryToolsForArcGISPro.sln /property:Configuration=Release ```
	* ArcMap Add-in
		* ``` cd military-tools-desktop-addins\source\MilitaryToolsForArcMap ```
		* ``` msbuild MilitaryToolsForArcMap.sln /property:Configuration=Release ```

#### Running

* ArcGIS Pro add-in
	* Double-click to install the build add-in file: 
	* `military-tools-desktop-addins\source\MilitaryToolsForArcGISPro\bin\Release\MilitaryToolsforArcGISPro.esriAddinX`
	* Run ArcGIS Pro and use the tools on the Military Tools tab
* ArcMap add-in
	* Double-click to install the build add-in file: 
	* `military-tools-desktop-addins\source\MilitaryToolsForArcMap\bin\Release\MilitaryToolsforArcMap.esriAddIn`
	* Run ArcMap and enable the Military Tools toolbar

#### More Information

For more information, please consult the [wiki](https://github.com/Esri/military-tools-desktop-addins/wiki) or [landing page](https://esri.github.io/military-tools-desktop-addins).

## Resources

* [Military Tools for ArcGIS](http://solutions.arcgis.com/defense/help/military-tools/)
* [ArcGIS for Defense Downloads](http://appsforms.esri.com/products/download/#ArcGIS_for_Defense)
* [ArcGIS Solutions Website](http://solutions.arcgis.com/#Defense)
* [ArcGIS 10.X Help](http://resources.arcgis.com/en/help/)
* [ArcGIS Blog](http://blogs.esri.com/esri/arcgis/)
* [ArcGIS Pro Help](http://pro.arcgis.com/en/pro-app/)

### Related repositories:

* [coordinate-conversion-addin-dotnet](https://github.com/Esri/coordinate-conversion-addin-dotnet)
* [distance-direction-addin-dotnet](https://github.com/Esri/distance-direction-addin-dotnet)
* [military-symbology](https://github.com/Esri/military-symbology)
* [military-tools-geoprocessing-toolbox](https://github.com/Esri/military-tools-geoprocessing-toolbox)
* [visibility-addin-dotnet](https://github.com/Esri/visibility-addin-dotnet)

## Issues

Find a bug or want to request a new feature?  Please let us know by submitting an [issue](https://github.com/Esri/military-tools-desktop-addins/issues).

## Contributing

Esri welcomes contributions from anyone and everyone. Please see our [guidelines for contributing](https://github.com/esri/contributing).


## Repository Points of Contact 
Contact the [Military Tools team](mailto:defensesolutions@esri.com)

## Licensing
Copyright 2018 Esri

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at:

   http://www.apache.org/licenses/LICENSE-2.0.

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

A copy of the license is available in the repository's [license.txt](./License.txt) file.

