using alienz.scripts;
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
        
        if (MultiplayerManager.IsServer)
		{
            MultiplayerManager.ServerCreatePlayer += CreatePlayer;
            MultiplayerManager.ServerDestroyPlayer += DestroyPlayer;
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreatePlayer(long id)
	{
        var player = (Player)_playerScene.Instantiate();
        player.Name = id.ToString();
        _spawner.AddChild(player);
        Debug.WriteLine($"Player {player.Name} created.");
        Debug.WriteLine($"UniqueId: {id}");
        Debug.WriteLine($"Authority: {player.GetMultiplayerAuthority()}");
    }

    private void DestroyPlayer(long id)
    {
        _spawner.GetNode(id.ToString()).QueueFree();
        Debug.WriteLine($"Player {id} destroyed.");
    }
}
