using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Geometry;
using System.Drawing;
using Rhino.Display;

namespace Corbel_design.Display_classes
{
    public class CorbelConduit:Rhino.Display.DisplayConduit
    {
        public Brep corbel;
        public List<Brep> reinforcements = new List<Brep>();

        protected override void CalculateBoundingBox(Rhino.Display.CalculateBoundingBoxEventArgs e)
        {
            base.CalculateBoundingBox(e);
            if (corbel != null)
                e.IncludeBoundingBox(corbel.GetBoundingBox(true));

        }


        protected override void PostDrawObjects(Rhino.Display.DrawEventArgs e)
        {
            if (corbel != null)
            {
                e.Display.DrawBrepShaded(corbel, new DisplayMaterial(Color.Black, 0.5));
                e.Display.DrawBrepWires(corbel,Color.Black);
            }
            foreach (Brep brep in reinforcements)
            {
                e.Display.DrawBrepShaded(brep, new DisplayMaterial(Color.Black, 0.1));
            }       
        }



    }
}
