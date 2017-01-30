using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace MilitaryTools
{
    public class ddDockWindowButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ddDockWindowButton()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;

            UID dockWinID = new UIDClass();
            dockWinID.Value = ThisAddIn.IDs.ddDockWindow;

            IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
            dockWindow.Show(true);
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
