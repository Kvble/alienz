using Godot;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

public partial class MainMenu : PanelContainer
{
    public static bool IsServer { get; set; }
    public static string ServerIp { get; set; }
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
        ServerIp = _ipTextBox.Text;
        IsServer = false;
        GetTree().ChangeSceneToFile("res://scenes//GameScene.tscn");
    }

    private void _OnExitButtonPressed()
    {
        GetTree().Quit();
    }
}
