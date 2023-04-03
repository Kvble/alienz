using Godot;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

public partial class GameScene : Node2D
{
    private Node _spawner;
    private PackedScene _playerScene;
    private MultiplayerApi _multiplayer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _multiplayer = GetTree().GetMultiplayer();
        _playerScene = ResourceLoader.Load<PackedScene>("res://scenes//Player.tscn");
        _spawner = GetTree().Root.GetNode<Node>("GameScene/Spawner");

        var peer = new ENetMultiplayerPeer();
        
        if (MainMenu.IsServer)
		{
            peer.PeerConnected += CreatePlayer;
            peer.PeerDisconnected += DestroyPlayer;
            if (peer.CreateServer(9999, 2) == Error.Ok)
            {
                Debug.WriteLine("Server created.");
                CreatePlayer(_multiplayer.GetUniqueId());
            }
        }
        else
        {
            if(peer.CreateClient(MainMenu.ServerIp, 9999) == Error.Ok)
            {

                Debug.WriteLine($"Connected to {MainMenu.ServerIp}:9999");
                //CreatePlayer(_multiplayer.GetUniqueId());
            }
        }
        _multiplayer.MultiplayerPeer = peer;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreatePlayer(long id)
	{
        var player = _playerScene.Instantiate();
        player.Name = id.ToString();

        if (player.GetMultiplayerAuthority() != (int)id)
        {
            player.SetMultiplayerAuthority((int)id);
        }
        Debug.WriteLine($"UniqueId: {id}");
        Debug.WriteLine($"Authority: {player.GetMultiplayerAuthority()}");
        _spawner.AddChild(player);
        Debug.WriteLine($"Player {player.Name} created.");
    }

    private void DestroyPlayer(long id)
    {
        _spawner.GetNode(id.ToString()).QueueFree();
        Debug.WriteLine($"Player {id} destroyed.");
    }
}
