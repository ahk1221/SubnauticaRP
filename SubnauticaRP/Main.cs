using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubnauticaRP
{
    public class Main
    {
        private static DiscordRPC.RichPresence Presence;

        public static void Patch()
        {
            // Initialize the client
            InitDiscord();

            // Set the presence
            DiscordRPC.UpdatePresence(ref Presence);
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
                state = "Test state",
                details = "Test details",
                largeImageText = "Test largeImageText"
            };
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
}
