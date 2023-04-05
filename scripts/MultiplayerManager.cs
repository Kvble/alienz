using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alienz.scripts
{
    internal static class MultiplayerManager
    {
        public static ENetMultiplayerPeer MultiplayerPeer { get; private set; }
        public static bool IsServer { get; private set; }
        public static string ServerIp { get; private set; }

        public static event OnServerDelegate ServerCreatePlayer;
        public static event OnServerDelegate ServerDestroyPlayer;

        public static void InitializeServer(int port)
        {
            MultiplayerPeer = new ENetMultiplayerPeer();
            if (MultiplayerPeer.CreateServer(9999, 2) == Error.Ok)
            {
                Debug.WriteLine("Server created.");
            }
            MultiplayerPeer.PeerConnected += OnServerCreatePlayer;
            MultiplayerPeer.PeerDisconnected += OnServerDestroyPlayer;
            IsServer = true;
        }

        public static void InitializeClient(string ip, int port)
        {
            MultiplayerPeer = new ENetMultiplayerPeer();
            if (MultiplayerPeer.CreateClient(ip, port) == Error.Ok)
            {
                Debug.WriteLine($"Connected to {ip}:{port}");
                ServerIp = ip;
            }
        }

        private static void OnServerCreatePlayer(long id) => ServerCreatePlayer?.Invoke(id);

        private static void OnServerDestroyPlayer(long id) => ServerDestroyPlayer?.Invoke(id);

    }

    public delegate void OnServerDelegate(long id);
}
