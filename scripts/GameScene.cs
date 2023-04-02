using Godot;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

public partial class GameScene : Node2D
{
    private Node _spawner;
    private PackedScene _playerScene;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _playerScene = ResourceLoader.Load<PackedScene>("res://scenes//Player.tscn");
        _spawner = GetTree().Root.GetNode<Node>("GameScene/Spawner");

        var peer = new ENetMultiplayerPeer();
        if (MainMenu.IsServer)
		{
            GetTree().GetMultiplayer().MultiplayerPeer.PeerConnected += CreatePlayer;
            GetTree().GetMultiplayer().MultiplayerPeer.PeerDisconnected += DestroyPlayer;
            if (peer.CreateServer(9999, 2) == Error.Ok)
            {
                Debug.WriteLine("Server created.");
                CreatePlayer(GetTree().GetMultiplayer().GetUniqueId());
            }
        }
        else
        {
            
            if(peer.CreateClient(MainMenu.ServerIp, 9999) == Error.Ok)
            {
                Debug.WriteLine("Client created.");
                CreatePlayer(GetTree().GetMultiplayer().GetUniqueId());
            }
        }
        GetTree().GetMultiplayer().MultiplayerPeer = peer;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreatePlayer(long id)
	{
        var player = _playerScene.Instantiate();
        player.Name = id.ToString();
        _spawner.AddChild(player);
        Debug.WriteLine("Connected");
    }

    private void DestroyPlayer(long id)
    {
        _spawner.GetNode(id.ToString()).QueueFree();
        Debug.WriteLine("Disconnected");
    }
}
