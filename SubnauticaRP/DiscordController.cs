using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SubnauticaRP
{
    public enum PlayerState
    {
        Menu,
        Loading,
        Playing
    }

    public class DiscordController : MonoBehaviour
    {
        private static PlayerState state;

        private static string currentSceneName;

        public static void Load()
        {
            var go = new GameObject("DiscordController").AddComponent<DiscordController>().gameObject;
            DontDestroyOnLoad(go);

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private static void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            currentSceneName = scene.name;

            Console.WriteLine("Loaded Scene: " + currentSceneName);

            if (currentSceneName.ToLower().Contains("menu"))
                state = PlayerState.Menu;
        }

        private void Update()
        {
            UpdateState();
            UpdatePresence();
        }

        private void UpdateState()
        {
            if (currentSceneName.ToLower().Contains("menu"))
                state = PlayerState.Menu;
            else if (!uGUI_SceneLoading.IsLoadingScreenFinished || uGUI.main.loading.IsLoading || !uGUI.main)
                state = PlayerState.Loading;
            else
                state = PlayerState.Playing;
        }

        private void UpdatePresence()
        {
            if(state != PlayerState.Playing)
            {
                Main.Presence.details = (state == PlayerState.Menu) ? "In Menu" : "Loading";
                Main.Presence.state = "";
                Main.Presence.largeImageKey = "subnautica_main";
                Main.Presence.smallImageKey = "";
                DiscordRPC.UpdatePresence(ref Main.Presence);

                return;
            }

            var biome = Utility.GetBiomeDisplayName(Player.main.GetBiomeString());
            Main.Presence.details = "At " + biome;
            Main.Presence.largeImageKey = Utility.GetBiomeStringName(biome);

            var subRoot = Player.main.GetCurrentSub();
            var vehicle = Player.main.GetVehicle();
            var depth = Player.main.GetDepth();
            if (subRoot)
            {
                var type = subRoot.GetType().Equals(typeof(BaseRoot)) ? "Base" : "Cyclops";
                Main.Presence.state = "In " + type;
                Main.Presence.smallImageKey = type.ToLower();
            }
            else if (vehicle)
            {
                var type = vehicle.GetType().Equals(typeof(SeaMoth)) ? "Seamoth" : "Prawn";
                Main.Presence.state = "In " + type;
                Main.Presence.smallImageKey = type.ToLower();
            }
            else if(Player.main.IsUnderwaterForSwimming())
            {
                Main.Presence.state = "Swimming";
                Main.Presence.smallImageKey = "swimming"; 
            }
            else if(!Player.main.IsSwimming())
            {
                Main.Presence.state = "On Foot";
                Main.Presence.smallImageKey = "";
            }

            if (Main.Presence.state != "")
                Main.Presence.state += " (Depth: " + depth + ")";

            DiscordRPC.UpdatePresence(ref Main.Presence);
        }

        //private IEnumerator Start()
        //{
        //    yield return new WaitForSeconds(1f);
        //    while ((!uGUI_SceneLoading.IsLoadingScreenFinished || uGUI.main.loading.IsLoading) && currentSceneName == "Main")
        //    {
        //        state = PlayerState.Loading;
        //        yield return null;
        //    }

        //    state = PlayerState.Playing;
        //}
    }
}
