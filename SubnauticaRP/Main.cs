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
                "floatingisland",
                "Floating Island"
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
                "grandreef",
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
                if(biomeDisplayName.Equals(biome.Value))
                {
                    return biome.Key;
                }
            }

            return "";
        }
        
        public static string GetPresenceDebug(DiscordRPC.RichPresence presence)
        {
            var returnString = 
                "state: " + presence.state + Environment.NewLine +
                "details: " + presence.details + Environment.NewLine +
                "startTimestamp: " + presence.startTimestamp + Environment.NewLine +
                "endTimestamp: " + presence.endTimestamp + Environment.NewLine +
                "largeImageKey: " + presence.largeImageKey + Environment.NewLine +
                "largeImageText: " + presence.largeImageText + Environment.NewLine +
                "smallImageKey: " + presence.smallImageKey + Environment.NewLine +
                "smallImageText: " + presence.smallImageText + Environment.NewLine +
                "partyId: " + presence.partyId + Environment.NewLine +
                "partySize: " + presence.partySize + Environment.NewLine +
                "partyMax: " + presence.partyMax + Environment.NewLine +
                "matchSecret: " + presence.matchSecret + Environment.NewLine +
                "joinSecret: " + presence.joinSecret + Environment.NewLine +
                "spectateSecret: " + presence.spectateSecret + Environment.NewLine +
                "instance: " + presence.instance;

            return returnString;
        }
    }
}
