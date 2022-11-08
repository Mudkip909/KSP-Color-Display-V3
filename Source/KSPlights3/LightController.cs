using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using KSP.Localization;
using UnityEngine;

namespace KSPlights2
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class LightController : PartModule
    {
        
        private float indX = 0;
        private float indY = 0;
        private int frameN = 1;
        private Texture2D texture = new Texture2D(16, 16);
        private Texture2D textureScaled = new Texture2D(10, 10);
        private string path;
        private MeshRenderer lightMeshRenderer;
        private int fps = 24;
        
     


        public void Start()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += sceneChanged;

            //Theres really no need to make a fancy cfg.
            var cfgpath = @Application.dataPath + "/../GameData/frames/fps.txt";

            string text = System.IO.File.ReadAllText(cfgpath);

            //Parse
            bool parsed = int.TryParse(text,out fps);
            print(parsed);

            InvokeRepeating("drawRepeat", 3f, (float)fps/1000);

        }

        
        private void sceneChanged(UnityEngine.SceneManagement.Scene a, UnityEngine.SceneManagement.Scene b)
        {
            //Stop playback
            CancelInvoke("drawRepeat");

        }

        public void drawRepeat()
        {
            //If more files exist, continue, otherwise restart playback
            if (!System.IO.File.Exists(@Application.dataPath + "/../GameData/frames/$file" + padFrameID(frameN + 1) + ".png"))
            {
                frameN = 1;
            }

            Vessel ves = FlightGlobals.ActiveVessel;

            var path = @Application.dataPath+"/../GameData/frames/$file" + padFrameID(frameN) + ".png";

            byte[] file = System.IO.File.ReadAllBytes(path);
            bool s = ImageConversion.LoadImage(texture, file);
            
            foreach (Part p in ves.Parts)
            {
                
                if (p.TryGetComponent(out LightIndexer light))
                {
                    indX = light.indX;
                    indY = light.indY;


                    float r = texture.GetPixel((int)indX, (int)indY).r / 1.2f;
                    float g = texture.GetPixel((int)indX, (int)indY).g / 1.2f;
                    float b = texture.GetPixel((int)indX, (int)indY).b / 1.2f;

                    //something
                    p.GetComponent<ModuleLight>().lightR = r;
                    p.GetComponent<ModuleLight>().lightG = g;
                    p.GetComponent<ModuleLight>().lightB = b;

                    //edits the color preview box on the left on the 2nd line of the light PAW window
                    p.GetComponent<ModuleLight>().UpdateLightColors();

                    //Goofy workaround
                    lightMeshRenderer = p.FindModelComponent<MeshRenderer>(p.GetComponent<ModuleLight>().lightMeshRendererName);
                    
                    lightMeshRenderer.material.SetColor("_EmissiveColor", new Color(r, g, b));
 
                    /*
                    if (texture.GetPixel((int)indX, (int)indY).r > 0.5f)
                    {
                        p.GetComponent<ModuleLight>().LightsOn();
                        //Debug.LogWarning(indX.ToString() + " " + indY.ToString() + "Turned ON");

                    }
                    else
                    {
                        p.GetComponent<ModuleLight>().LightsOff();
                        //Debug.LogWarning(indX.ToString() + " " + indY.ToString() + "Turned OFF");

                    }
                    */

                }
            }
            frameN++;
        }

        public static string padFrameID(int framenum)
        {
            if (framenum >= 100)
            {
                return framenum.ToString();
            }
            if (framenum >= 10)
            {
                return "0" + framenum.ToString();
            }
            if (framenum < 10)
            {
                return "00" + framenum.ToString();
            }
            
            return "000";
        }
    }
}
