using System;
using Rhino;
using Rhino.Commands;
using Rhino.Input;
using Rhino.DocObjects;
using Corbel_design.Display_classes;

namespace Corbel_design
{
    [System.Runtime.InteropServices.Guid("b15d678c-73f6-47d6-9383-79eb2e393a3d")]
    public class TakeBrep : Command
    {
        static TakeBrep _instance;
        public TakeBrep()
        {
            _instance = this;
        }

        ///<summary>The only instance of the TakeBrep command.</summary>
        public static TakeBrep Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "TakeBrep"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            ObjRef obj_ref;
            var rc = RhinoGet.GetOneObject("Select Brep", false, ObjectType.Brep, out obj_ref);
            if (rc != Result.Success)
                return rc;
            var brep = obj_ref.Brep();
            brep.Translate(new Rhino.Geometry.Vector3d(1000, 0, 0));


            CorbelConduit cd = new CorbelConduit();

            cd.Enabled = true;
            cd.corbel = brep;

            return Result.Success;
        }
    }
}
