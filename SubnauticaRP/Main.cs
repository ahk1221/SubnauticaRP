using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubnauticaRP
{
    public class Main
    {
        public static DiscordRPC.RichPresence Presence;

        public static void Patch()
        {
            // Initialize the client
            InitDiscord();

            // Load controller
            DiscordController.Load();
        }

        public static void InitDiscord()
        {
            // Handlers
            var eventHandlers = default(DiscordRPC.EventHandlers);
            eventHandlers.readyCallback = ReadyCallback;
            eventHandlers.disconnectedCallback = DisconnectedCallback;
            eventHandlers.errorCallback = ErrorCallback;

            // Initialize
            DiscordRPC.Initialize("448509316450877458", ref eventHandlers, true, "264710");

            // Presence
            Presence = new DiscordRPC.RichPresence
            {
                largeImageKey = "subnautica_main",
                details = "In Menu",
            };

            // Run the callbacks
            DiscordRPC.RunCallbacks();

            // Set the presence
            DiscordRPC.UpdatePresence(ref Presence);
        }

        private static void DisconnectedCallback(int errorCode, string message)
        {
            // Log errors
            Console.WriteLine("Discord ERR: ERROR CODE " + errorCode);
            Console.WriteLine(message);
        }

        private static void ErrorCallback(int errorCode, string message)
        {
            // Log errors
            Console.WriteLine("Discord ERR: ERROR CODE " + errorCode);
            Console.WriteLine(message);
        }

        private static void ReadyCallback()
        {
            // Log
            Console.WriteLine("Discord is READY!");
        }
    }

    public class Utility
    {
        private static Dictionary<string, string> biomeMap = new Dictionary<string, string>()
        {
            {
                "lostriver",
                "Lost River"
            },
            {
                "kelpforest",
                "Kelp Forest"
            },
            {
                "grassy",
                "Grassy Plateus"
            },
            {
                "underwaterislands",
                "Underwater Islands"
            },
            {
                "island",
                "Island"
            },
            {
                "lava",
                "Lava Zone"
            },
            {
                "ilz",
                "Inactive Lava Zone"
            },
            {
                "mushroom",
                "Mushroom Forest"
            },
            {
                "bloodkelp",
                "Blood Kelp"
            },
            {
                "dunes",
                "Sand Dunes"
            },
            {
                "grandReef",
                "Grand Reef"
            },
            {
                "koosh",
                "Bulb Zone"
            },
            {
                "mountains",
                "Mountains"
            },
            {
                "sparse",
                "Sparse Reef"
            },
            {
                "jellyshroom",
                "Jellyshroom"
            },
            {
                "safe",
                "Safe Shallows"
            },
            {
                "crash",
                "Crash Zone"
            },
            {
                "crag",
                "Crag Field"
            }
        };

        public static string GetBiomeDisplayName(string biomeStr)
        {
            foreach(var biome in biomeMap)
            {
                if(biomeStr.ToLower().Contains(biome.Key))
                {
                    return biome.Value;
                }
            }

            return biomeStr;
        }

        public static string GetBiomeStringName(string biomeDisplayName)
        {
            foreach(var biome in biomeMap)
            {
                if(biome.Value.Contains(biomeDisplayName))
                {
                    return biome.Key;
                }
            }

            return "";
        }
    }
}
