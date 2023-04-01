using Godot;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

public partial class GameScene : Node2D
{
    private Node2D _spawner;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _spawner = GetTree().Root.GetNode<Node2D>("GameScene/Spawner");

        var peer = new ENetMultiplayerPeer();
        if (MainMenu.IsServer)
		{
            GetTree().GetMultiplayer().MultiplayerPeer.PeerConnected += CreatePlayer;
            GetTree().GetMultiplayer().MultiplayerPeer.PeerDisconnected += DestroyPlayer;
            if (peer.CreateServer(42069, 2) == Error.Ok)
            {
                Debug.WriteLine("Server created.");
            }
        }
        else
        {
            peer.CreateClient(MainMenu.ServerIp, 42069);
        }
        GetTree().GetMultiplayer().MultiplayerPeer = peer;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreatePlayer(long id)
	{
        var player = new Player()
        {
            Id = id.ToString(),
        };
        _spawner.AddChild(player);
        Debug.WriteLine("Connected");
    }

    private void DestroyPlayer(long id)
    {
        _spawner.GetNode(id.ToString()).QueueFree();
        Debug.WriteLine("Disconnected");
    }
}
