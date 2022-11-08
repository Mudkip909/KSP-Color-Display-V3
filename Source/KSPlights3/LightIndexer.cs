using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KSP.Localization;
using UnityEngine;

namespace KSPlights2
{
    [KSPAddon(KSPAddon.Startup.FlightAndEditor, false)]
    public class LightIndexer : PartModule
    {

        [KSPField(isPersistant = true, guiActiveEditor = true, guiName = "Index X", guiFormat = "F1"),
        UI_FloatRange(minValue = 1f, maxValue = 64f, stepIncrement = 1f, scene = UI_Scene.All)]
        [UI_ScaleEdit(scene = UI_Scene.Editor)]
        public float indX = 1;

        [KSPField(isPersistant = true, guiActiveEditor = true, guiName = "Index Y", guiFormat = "F1"),
        UI_FloatRange(minValue = 1f, maxValue = 64f, stepIncrement = 1f, scene = UI_Scene.All)]
        [UI_ScaleEdit(scene = UI_Scene.Editor)]
        public float indY = 1;

        public void Update()
        {
            
            //Vessel ves = FlightGlobals.ActiveVessel;


            /*
            foreach (Part p in ves.Parts)
            {
                if (p.TryGetComponent(out LightIndexer solarPanel))
                {
                        
                }
            }
            */

        }
    }
}
