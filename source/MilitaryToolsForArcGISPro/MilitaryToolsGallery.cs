using ArcGIS.Core.Licensing;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace ProAppModuleMilitaryTools
{
    internal class TaggedGalleryItem : GalleryItem
    {
        public TaggedGalleryItem(IPlugInWrapper command, string text, object icon = null, string tooltip = "", string group = "")
          : base(text, icon, tooltip, group) { Command = command; }

        public IPlugInWrapper Command { get; private set; }
    }
    internal class MilitaryToolsGallery : Gallery
    {
        private bool _isInitialized;

        protected override void OnDropDownOpened()
        {
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                if (_isInitialized)
                    return;
                var commandsConversion = new List<string>() {
                "ProAppModuleMilitaryTools_ConvertCoordinates_button",
                "ProAppModuleMilitaryTools_TableTo2_PointLine_button",
                "ProAppModuleMilitaryTools_TableToEllipse_button",
                "ProAppModuleMilitaryTools_TableToLineofBearing_button",
                "ProAppModuleMilitaryTools_TableToPoint_button",
                "ProAppModuleMilitaryTools_TabletoPolygon_button",
                "ProAppModuleMilitaryTools_TabletoPolyline_button"
            };
                foreach (var command in commandsConversion)
                {
                    var wrapper = FrameworkApplication.GetPlugInWrapper(command);
                    var headerName = FrameworkApplication.GetPlugInWrapper("ProAppMilitaryTools_Group0");
                    this.Add(new TaggedGalleryItem(wrapper, wrapper.Caption,
                      (ImageSource)wrapper.LargeImage ?? (ImageSource)wrapper.SmallImage,
                      wrapper.Tooltip.Split(new string[] { "\n" }, StringSplitOptions.None).First(), headerName.Caption));
                }

                var commandsDistanceDirection = new List<string>() {
                "ProAppModuleMilitaryTools_RangeRingsFromInterval_button",
                "ProAppModuleMilitaryTools_RangeRingsFromMinimumandMaximum_button",
                "ProAppModuleMilitaryTools_RangeRingsFromMinimumandMaximumTable_button"
            };

                foreach (var command in commandsDistanceDirection)
                {
                    var wrapper = FrameworkApplication.GetPlugInWrapper(command);
                    var headerName = FrameworkApplication.GetPlugInWrapper("ProAppMilitaryTools_Group1");
                    this.Add(new TaggedGalleryItem(wrapper, wrapper.Caption,
                      (ImageSource)wrapper.LargeImage ?? (ImageSource)wrapper.SmallImage,
                      wrapper.Tooltip.Split(new string[] { "\n" }, StringSplitOptions.None).First(), headerName.Caption));
                }

                var commandsGRG = new List<string>() {
                    "ProAppModuleMilitaryTools_CreateGRGFromArea_button",
                    "ProAppModuleMilitaryTools_CreateGRGFromPoint_button",
                    "ProAppModuleMilitaryTools_CreateReferenceSystemGRGFromArea_button",
                    "ProAppModuleMilitaryTools_NumberFeatures_button"
                };

                foreach (var command in commandsGRG)
                {
                    var wrapper = FrameworkApplication.GetPlugInWrapper(command);
                    var headerName = FrameworkApplication.GetPlugInWrapper("ProAppMilitaryTools_Group2");
                    this.Add(new TaggedGalleryItem(wrapper, wrapper.Caption,
                      (ImageSource)wrapper.LargeImage ?? (ImageSource)wrapper.SmallImage,
                      wrapper.Tooltip.Split(new string[] { "\n" }, StringSplitOptions.None).First(), headerName.Caption));
                }

                var commandsVisibility = new List<string>() {
                    "ProAppModuleMilitaryTools_FindLocalPeaks_button",
                    "ProAppModuleMilitaryTools_HighestPoints_button",
                    "ProAppModuleMilitaryTools_LinearLineofSight_button",
                    "ProAppModuleMilitaryTools_LowestPoints_button",
                    "ProAppModuleMilitaryTools_RadialLineofSight_button",
                    "ProAppModuleMilitaryTools_RadialLineofSightandRange_button"
                };

                foreach (var command in commandsVisibility)
                {
                    var wrapper = FrameworkApplication.GetPlugInWrapper(command);
                    var headerName = FrameworkApplication.GetPlugInWrapper("ProAppMilitaryTools_Group3");
                    this.Add(new TaggedGalleryItem(wrapper, wrapper.Caption,
                      (ImageSource)wrapper.LargeImage ?? (ImageSource)wrapper.SmallImage,
                      wrapper.Tooltip.Split(new string[] { "\n" }, StringSplitOptions.None).First(), headerName.Caption));
                }
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                var abc = ex.Message;
            }

        }

        protected override void OnClick(GalleryItem item)
        {
            var command = ((TaggedGalleryItem)item).Command as ICommand;
            if (command != null)
                command.Execute(null);
        }
    }

	internal static class AddIn
	{
		public static string AssemblyInstallPath => Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
		public static string MTToolboxAlias => "mt";
		public static string MTToolboxPath => Path.Combine(AssemblyInstallPath, "toolboxes", "Military Tools.pyt");
		public static void OpenCustomGPTool(string systoolboxalias, string systoolalias, IEnumerable<string> args)
		{
			if (AddIn.SystemToolsAvailable)
			{
				//Try Opening tools within system toolbox first
				Geoprocessing.OpenToolDialog(systoolboxalias + "." + systoolalias, args);
			}
			else
			{
				//if exception Open tools from AssemblyCache Instead
				if (systoolboxalias == MTToolboxAlias)
				{
					Geoprocessing.OpenToolDialog(MTToolboxPath + "\\" + systoolalias, args);
				}
			}
		}
		public static object TOCArgs()
		{
			Layer selectedLayer = null;
			StandaloneTable SATable = null;
			if (MapView.Active != null)
			{
				if (MapView.Active.GetSelectedLayers().Count() > 0)
				{
					selectedLayer = MapView.Active.GetSelectedLayers()[0];
				}
				if (MapView.Active.GetSelectedStandaloneTables().Count() > 0)
				{
					SATable = MapView.Active.GetSelectedStandaloneTables()[0];
				}
				if (SATable != null && selectedLayer != null)
				{
					return selectedLayer;
				}
				if (selectedLayer != null)
				{
					return selectedLayer;
				}
				if (SATable != null)
				{
					return SATable;
				}
			}
			return null;
		}

		public static Boolean SpatialAnalystEnabled => LicenseInformation.IsAvailable(LicenseCodes.SpatialAnalyst);

		public static Boolean Analyst3DEnabled => LicenseInformation.IsAvailable(LicenseCodes.Analyst3D);

		public static bool SystemToolsAvailable 
		{ 
                    get { return systemToolsAvailable; }
                    set { systemToolsAvailable = value; }			
		}

		private static Boolean systemToolsAvailable;

		public static void ToggleState()
		{
			if (SpatialAnalystEnabled&& Analyst3DEnabled)
			{
				FrameworkApplication.State.Activate("visibility_groupstate");
			}
			else
			{
				FrameworkApplication.State.Deactivate("visibility_groupstate");
			}
		}

	}

	internal class ConvertCoordinates_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.ConvertCoordinates, args);
		}		
	}
	internal class TableTo2_PointLine_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.TableTo2PointLine, args);
		}
	}
	internal class TableToEllipse_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.TableToEllipse, args);
		}
	}
	internal class TableToLineofBearing_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.TableToLineOfBearing, args);
		}
	}
	internal class TableToPoint_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.TableToPoint, args);
		}
	}
	internal class TabletoPolygon_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.TableToPolyline, args);
		}
	}
	internal class TabletoPolyline_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.TableToPolyline, args);
		}
	}

	internal class RangeRingsFromInterval_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.RangeRingsFromInterval, args);
		}
	}
	internal class RangeRingsFromMinimumandMaximum_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.RangeRingFromMinimumAndMaximum, args);
		}
	}
	internal class RangeRingsFromMinimumandMaximumTable_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.RangeRingsFromMinAndMaxTable, args);
		}
	}
	internal class CreateGRGFromArea_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.CreateGRGFromArea, args);
		}
	}
	internal class CreateGRGFromPoint_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.CreateGRGFromPoint, args);
		}
	}
	internal class CreateReferenceSystemGRGFromArea_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.CreateReferenceSystemGRGFromArea, args);
		}
	}
	internal class NumberFeatures_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.NumberFeatures, args);
		}
	}
	internal class FindLocalPeaks_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.FindLocalPeaks, args);
		}
	}
	internal class HighestPoints_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.HighestPoints, args);
		}
	}
	internal class LinearLineofSight_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.LinearLineOfSight, args);
		}
	}
	internal class LowestPoints_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.LowestPoints, args);
		}
	}
	internal class RadialLineofSight_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.RadialLineOfSight, args);
		}
	}
	internal class RadialLineofSightandRange_button : Button
	{
		protected override void OnClick()
		{
			var args = AddIn.TOCArgs() != null ? Geoprocessing.MakeValueArray(AddIn.TOCArgs()) : Geoprocessing.MakeValueArray();
			AddIn.OpenCustomGPTool(AddIn.MTToolboxAlias, Constants.RadialLineOfSightAndRange, args);
		}
	}
	public class Constants
	{
		public const string ConvertCoordinates = "ConvertCoordinates";
		public const string TableTo2PointLine = "TableTo2PointLine";
		public const string TableToEllipse = "TableToEllipse";
		public const string TableToLineOfBearing = "TableToLineOfBearing";
		public const string TableToPoint = "TableToPoint";
		public const string TableToPolyline = "TableToPolyline";
		public const string RangeRingsFromInterval = "RangeRingsFromInterval";
		public const string RangeRingFromMinimumAndMaximum = "RangeRingFromMinimumAndMaximum";
		public const string RangeRingsFromMinAndMaxTable = "RangeRingsFromMinAndMaxTable";
		public const string CreateGRGFromArea = "CreateGRGFromArea";
		public const string CreateGRGFromPoint = "CreateGRGFromPoint";
		public const string CreateReferenceSystemGRGFromArea = "CreateReferenceSystemGRGFromArea";
		public const string NumberFeatures = "NumberFeatures";
		public const string FindLocalPeaks = "FindLocalPeaks";
		public const string HighestPoints = "HighestPoints";
		public const string LinearLineOfSight = "LinearLineOfSight";
		public const string LowestPoints = "LowestPoints";
		public const string RadialLineOfSight = "RadialLineOfSight";
		public const string RadialLineOfSightAndRange = "RadialLineOfSightAndRange";
	}
}
