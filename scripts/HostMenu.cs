using Godot;
using System;
using System.Diagnostics;

public partial class HostMenu : PanelContainer
{
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
		try
		{
			var peer = new ENetMultiplayerPeer();
			peer.CreateServer(42069, 5);
			GetTree().GetMultiplayer().MultiplayerPeer = peer;
            GetTree().ChangeSceneToFile("res://scenes//GameScene.tscn");
        }
		catch (Exception e)
		{
            Debug.WriteLine(e);
		}
    }
}
