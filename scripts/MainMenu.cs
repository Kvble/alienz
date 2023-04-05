using alienz.scripts;
using Godot;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

public partial class MainMenu : PanelContainer
{
    
    private TextEdit _ipTextBox;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _ipTextBox = GetTree().Root.GetNode<TextEdit>("MainMenu/MenuContainer/ConnectContainer/IpTextBox");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void _OnHostButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes//HostMenu.tscn");
    }

    private void _OnConnectButtonPressed()
    {
        MultiplayerManager.InitializeClient(_ipTextBox.Text, 9999);
        GetTree().GetMultiplayer().MultiplayerPeer = MultiplayerManager.MultiplayerPeer;
        GetTree().ChangeSceneToFile("res://scenes//GameScene.tscn");
    }

    private void _OnExitButtonPressed()
    {
        GetTree().Quit();
    }
}
