using Rhino;
using Rhino.PlugIns;
using System;
using System.Collections.Generic;

namespace Corbel_design
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class CorbeldesignPlugIn : Rhino.PlugIns.PlugIn
    {

        public List<Tuple<double, double>> geometryPoints = new List<Tuple<double, double>>();
        public List<Tuple<double, double>> distToForcePoints = new List<Tuple<double, double>>();
        public List<Tuple<double, double>> forcePoints = new List<Tuple<double, double>>();
        public List<Tuple<double, double>> consoleHeigthPoints = new List<Tuple<double, double>>();



        public CorbeldesignPlugIn()
        {
            Instance = this;
            RhinoApp.Idle += OnIdle; // subcribe;
        }

        private static void OnIdle(object sender, EventArgs e)
        {
            RhinoApp.Idle -= OnIdle; // Unsubscribe
            openPanel();

        }


        private static void openPanel()
        {
            //Opens the main painel
            var type = typeof(FormMain);
            Rhino.UI.Panels.OpenPanel(type.GUID);
        }

        protected override Rhino.PlugIns.LoadReturnCode OnLoad(ref string errorMessage)
        {
            System.Type panelType = typeof(FormMain);
            Rhino.UI.Panels.RegisterPanel(this, panelType, "Corbel Design", System.Drawing.SystemIcons.WinLogo);
            return Rhino.PlugIns.LoadReturnCode.Success;

        }


        /// <summary>
        /// Returns the ID of this panel.
        /// </summary>
        public static System.Guid PanelId
        {
            get
            {
                return typeof(FormMain).GUID;
            }
        }


        ///<summary>Gets the only instance of the CorbeldesignPlugIn plug-in.</summary>
        public static CorbeldesignPlugIn Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// The tabbed dockbar user control
        /// </summary>
        public FormMain UserControl
        {
            get;
            set;
        }

        public override PlugInLoadTime LoadTime { get { return PlugInLoadTime.AtStartup; } }


        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and mantain plug-in wide options in a document.
    }
}