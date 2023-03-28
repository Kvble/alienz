using Godot;
using System.Diagnostics;

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

    public void _OnHostButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes//HostMenu.tscn");
    }

    public void _OnConnectButtonPressed()
    {
        var connectionIp = _ipTextBox.Text;
        Debug.WriteLine(connectionIp);
    }

    public void _OnExitButtonPressed()
    {
        GetTree().Quit();
    }
}
