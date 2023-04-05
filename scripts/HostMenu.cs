using alienz.scripts;
using Godot;
using System;
using System.Diagnostics;

public partial class HostMenu : PanelContainer
{
    private MultiplayerApi _multiplayer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _OnPlayButtonPressed()
	{
        MultiplayerManager.InitializeServer(9999);
        GetTree().GetMultiplayer().MultiplayerPeer = MultiplayerManager.MultiplayerPeer;
        GetTree().Root.Title += " HOST";
        GetTree().ChangeSceneToFile("res://scenes//GameScene.tscn");
    }
}
