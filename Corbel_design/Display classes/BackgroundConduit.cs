using System;
using System.Collections.Generic;
using System.Drawing;
using Rhino;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.DocObjects.Custom;
using Rhino.Geometry;

namespace Corbel_design.Display_classes
{
    /*
    public class BackGroundConduit : Rhino.Display.DisplayConduit
    {

        enum Material
        {
            Steel = 0,
            Concrete = 1
        }

        private readonly Dictionary<Material,Color> _colors = new Dictionary<Material, Color>()
        {
            {Material.Concrete,Color.Gray},
            {Material.Steel,Color.Black }
        };



        public List<Brep> GetReinforcementBreps()
        {
            List<Brep> temp = new List<Brep>();
            RhinoObject[] objs = RhinoDoc.ActiveDoc.Objects.FindByUserString("infType", "Reinforcement", true);
            foreach (RhinoObject rhinoObject in objs)
            {
                UserDataList list = rhinoObject.Attributes.UserData;
                if (list.Find(typeof(Reinforcement)) is Reinforcement tempReinf)
                    temp.Add(tempReinf.BrepGeometry);
            }

            return temp;
        }

        //This method will return all the breps in the geometry larges saved in the geometrys of the model. 
        // the return type is tuple where first value is the list of concrete breps and the second value
        // is a list of steel breps
        public Tuple<List<Brep>, List<Brep>> GetGeometryLarges()
        {
            List<Brep> concreteBreps = new List<Brep>();
            List<Brep> steelBreps = new List<Brep>();
            RhinoObject[] objs = RhinoDoc.ActiveDoc.Objects.FindByUserString("infType", "GeometryLarge", true);
            foreach (RhinoObject rhinoObject in objs)
            {
                UserDataList list = rhinoObject.Attributes.UserData;

                if (!(list.Find(typeof(GeometryLarge)) is GeometryLarge temp))
                    temp = list.Find(typeof(RectangleGeometryLarge)) as GeometryLarge;

                
                if (temp != null && temp.Material.GetType() == typeof(ConcreteMaterial))
                    concreteBreps.Add(temp.BaseBrep);
                else if (temp != null && temp.Material.GetType() == typeof(SteelMaterial))
                    steelBreps.Add(temp.BaseBrep);
                
                    
                    
            }

            return new Tuple<List<Brep>, List<Brep>>(concreteBreps,steelBreps);
        }




        protected override void CalculateBoundingBox(CalculateBoundingBoxEventArgs e)
        {
           // base.CalculateBoundingBox(e);
           // BoundingBox box = CreateBoundingBox();
           // e.IncludeBoundingBox(box);
    }

        protected override void PostDrawObjects(DrawEventArgs e)
        {
            base.PostDrawObjects(e);

            List<Brep> concreteBreps = new List<Brep>();
            List<Brep> steelBreps = new List<Brep>();

            Tuple<List<Brep>, List<Brep>> temp1 = GetGeometryLarges();
            concreteBreps.AddRange(temp1.Item1);
            steelBreps.AddRange(temp1.Item2);
            steelBreps.AddRange(GetReinforcementBreps());

            foreach (Brep brep in concreteBreps)
            {
                e.Display.DrawBrepShaded(brep, new DisplayMaterial(_colors[Material.Concrete]));
            }

            foreach (Brep brep in steelBreps)
            {
                e.Display.DrawBrepShaded(brep, new DisplayMaterial(_colors[Material.Steel]));
            }

        }


        protected override void DrawForeground(DrawEventArgs e)
        {

            base.DrawForeground(e);

        }

    }
*/
}