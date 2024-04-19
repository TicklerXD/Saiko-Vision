using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// *********************** INFO FOR MOD DEVELOPERS ********************
//
//        If you decide to use the code of my mod as a template for your mods, then you need to copy and paste the following lines in Properties/AssemblyInfo.cs:

//        [assembly: MelonInfo(typeof(*Your plugin class (SaikoCameraMod.SaikoCamera in my case)*), "*Name of your mode*", "*Any version*", "*Your nickname*")]
//        [assembly: MelonGame("*Game Developer*", "*Name of game*")] -----> You can see this information from the MelonLoader console when you start the game
//
//*********************************************************************

namespace SaikoCameraMod
{
    public class SaikoCamera : MelonMod
    {
        static string playText = string.Empty; // Set the "Play" text for Nightmare Mode
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);

            if (!sceneName.Contains("LevelNew")) return;

            //Set player's camera
            var mainCam = Camera.main;
            mainCam.rect = new Rect(.75f, 0.75f, .35f, .35f);
            mainCam.depth = 5;

            if (GameObject.Find("bunny_saiko") || GameObject.Find("sane saiko") || GameObject.Find("nightmare"))
            {
                var cam = new GameObject("Camera");
                Transform head;
                if (GameObject.Find("bunny_saiko")) head = GetHead("bunny_saiko");
                else if (GameObject.Find("nightmare")) head = GetHead("nightmare");
                else head = GetHead("sane saiko");

                // Creating Saiko's camera
                cam.AddComponent<Camera>();
                cam.transform.position = head.position;
                cam.transform.rotation = head.rotation;  
                cam.transform.SetParent(head);
                cam.SetActive(true);

                return;
            }

            var cam2 = GetHead("yandere 2").FindChild("Camera");
            cam2.gameObject.SetActive(true);
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            // Just unlocks the Nightmare Mode
            if (SceneManager.GetActiveScene().name.Contains("MainMenu"))
            {
                var n = GameObject.Find("MainMenu").transform.FindChild("Canvas").FindChild("ChooseDifficulty").FindChild("Practice (1)").gameObject;
                var textComp = n.transform.FindChild("Text").GetComponent<Text>();
                if (playText.Equals(string.Empty)) playText = textComp.text;

                textComp.text = playText;
                n.GetComponent<Button>().interactable = true;
            }
        }
        Transform GetHead(string name) =>
            GameObject.Find(name).transform.FindChild("Armature" + (name.Contains("bunny") ? ".002" : "")).FindChild("mixamorig:Hips").FindChild("mixamorig:Spine").FindChild("mixamorig:Spine1")
                .FindChild("mixamorig:Spine2").FindChild("mixamorig:Neck").FindChild("mixamorig:Head");
    }  
}
