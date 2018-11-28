// Copyright 2016 Esri 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ArcGIS.Desktop.Core.Events;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Threading.Tasks;
using ArcGIS.Desktop.Internal.GeoProcessing;
using System.Linq;

namespace ProAppModuleMilitaryTools
{
    internal class MilitaryToolsModule : Module
    {
        private static MilitaryToolsModule _this = null;

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static MilitaryToolsModule Current
        {
            get
            {
                return _this ?? (_this = (MilitaryToolsModule)FrameworkApplication.FindModule("ProAppModuleMilitaryTools_Module"));
            }
        }

        #region Overrides
        /// <summary>
        /// Called by Framework when ArcGIS Pro is closing
        /// </summary>
        /// <returns>False to prevent Pro from closing, otherwise True</returns>
        protected override bool CanUnload()
        {
            //TODO - add your business logic
            //return false to ~cancel~ Application close
            return true;
        }

		protected override bool Initialize()
		{
			ProjectOpenedEvent.Subscribe(OnProjectOpened);
            //ProjectOpenedAsyncEvent.Subscribe(OnProjectOpenedAsync);
            return base.Initialize();
		}

		private void OnProjectOpened(ProjectEventArgs args)
		{
			AddIn.ToggleState();			
		}

		private async Task OnProjectOpenedAsync(ProjectEventArgs args)
		{
			var tools = await SearchTools.SearchAsync(Constants.RadialLineOfSightAndRange);
			string searchResult = tools.ElementAt(0).ToString();
			if (searchResult.Contains(Constants.RadialLineOfSightAndRange))
			{
				AddIn.SystemToolsAvailable = true;
			}
			else
			{
				AddIn.SystemToolsAvailable = false;
			}
		}

		protected override void Uninitialize()
		{
			base.Uninitialize();
			ProjectOpenedEvent.Unsubscribe(OnProjectOpened);
			return;
		}

		#endregion Overrides

	}
}
